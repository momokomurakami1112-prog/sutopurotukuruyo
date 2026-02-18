using System.Text;
using System.Text.RegularExpressions;

namespace sutopurotukuruyo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        // ファイル読込ボタン
        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "ストアドプロシージャーを選択";
            dialog.Filter = "sqlファイル (*.sql)|*.sql|すべてのファイル (*.*)|*.*";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FilePathTextBox.Text = dialog.FileName;
        }

        // メソッド作成ボタン
        private void MethodGenerateButton_Click(object sender, EventArgs e)
        {
            // ファイルが指定されていない場合エラー
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text))
            {
                MessageBox.Show("ファイルを選択してください。");
                return;
            }
            // ファイルが存在しない場合エラー
            if (!File.Exists(FilePathTextBox.Text))
            {
                MessageBox.Show("ファイルが存在しません。");
                return;
            }

            string[] allLines = File.ReadAllLines(FilePathTextBox.Text, Encoding.GetEncoding("shift_jis"));
            List<string> parameterLines = ExtractParameterLines(allLines);


            List<StoredProcedureParameter> parameterList = new List<StoredProcedureParameter>();
            foreach (string line in parameterLines)
            {
                StoredProcedureParameter parameter = ParseParameter(line);

                if (parameter != null)
                {
                    parameterList.Add(parameter);
                }
            }
            MethodTextBox.Clear();
            StringBuilder sb = new StringBuilder();

            foreach (var p in parameterList)
            {
                string sqlDbType = ConvertToSqlDbType(p.SqlType);

                // コメント行
                if (!string.IsNullOrWhiteSpace(p.Comment))
                {
                    sb.AppendLine($"' {p.Comment}");
                }

                // Decimal系は特別扱い
                if (sqlDbType.Equals("Decimal", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine($"sqlcmd.Parameters.Add(\"@{p.Name}\", SqlDbType.{sqlDbType})");

                    // Precision / Scale があれば設定
                    if (!string.IsNullOrWhiteSpace(p.PrecisionText))
                    {
                        sb.AppendLine($"sqlcmd.Parameters(\"@{p.Name}\").Precision = {p.PrecisionText}");
                    }
                    if (!string.IsNullOrWhiteSpace(p.ScaleText))
                    {
                        sb.AppendLine($"sqlcmd.Parameters(\"@{p.Name}\").Scale = {p.ScaleText}");
                    }

                    if (!p.IsOutput)
                    {
                        sb.AppendLine($"sqlcmd.Parameters(\"@{p.Name}\").Value = ");
                    }
                    else
                    {
                        sb.AppendLine($"sqlcmd.Parameters(\"@{p.Name}\").Direction = ParameterDirection.Output");
                    }
                }
                else
                {
                    // VarChar / Char 系
                    string lengthText = "";
                    if (sqlDbType.Equals("Char", StringComparison.OrdinalIgnoreCase) ||
                        sqlDbType.Equals("VarChar", StringComparison.OrdinalIgnoreCase) ||
                        sqlDbType.Equals("NChar", StringComparison.OrdinalIgnoreCase) ||
                        sqlDbType.Equals("NVarChar", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrWhiteSpace(p.LengthText))
                        {
                            lengthText = $", {p.LengthText}";
                        }
                    }

                    string line;
                    if (p.IsOutput)
                    {
                        line = $"sqlcmd.Parameters.Add(\"@{p.Name}\", SqlDbType.{sqlDbType}{lengthText}).Direction = ParameterDirection.Output";
                    }
                    else
                    {
                        line = $"sqlcmd.Parameters.Add(\"@{p.Name}\", SqlDbType.{sqlDbType}{lengthText}).Value = ";
                    }
                    sb.AppendLine(line);
                }
            }

            // TextBox に表示
            MethodTextBox.Text = sb.ToString();
        }

        private string ConvertToSqlDbType(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "tinyint": return "TinyInt";
                case "smallint": return "SmallInt";
                case "int": return "Int";
                case "decimal": return "Decimal";
                case "char": return "Char";
                case "varchar": return "VarChar";
                case "nvarchar": return "NVarChar";
                case "bit": return "Bit";
                case "datetime": return "DateTime";
                default: return sqlType;
            }
        }

        // パラメータ解析
        private List<string> ExtractParameterLines(string[] allLines)
        {
            List<string> parameterLines = new List<string>();

            bool isInsideParameterArea = false;

            foreach (string rawLine in allLines)
            {
                string line = rawLine.Trim();

                if (line.StartsWith("CREATE", StringComparison.OrdinalIgnoreCase))
                {
                    isInsideParameterArea = true;
                    continue;
                }

                if (!isInsideParameterArea)
                {
                    continue;
                }

                if (line.StartsWith("WITH", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("AS", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                int atIndex = line.IndexOf("@");

                if (atIndex >= 0)
                {
                    string parameterPart = line.Substring(atIndex).TrimEnd(',');
                    parameterLines.Add(parameterPart);
                }
            }

            return parameterLines;
        }

        private StoredProcedureParameter ParseParameter(string line)
        {
            string pattern =
                @"@(?<name>\w+)\s+" +
                @"(?<type>\w+)" +
                @"(\((?<length>[^)]+)\))?\s*" +
                @"(?<output>OUTPUT)?\s*" +
                @"(--<(?<comment>.+?)>)?";

            Match match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return null;
            }

            StoredProcedureParameter parameter = new StoredProcedureParameter();

            parameter.Name = match.Groups["name"].Value;
            parameter.SqlType = match.Groups["type"].Value;

            if (match.Groups["length"].Success)
            {
                string length = match.Groups["length"].Value.Trim();

                // Decimal(7,2) の場合は Precision / Scale に分ける
                if (parameter.SqlType.Equals("decimal", StringComparison.OrdinalIgnoreCase) &&
                    length.Contains(","))
                {
                    var parts = length.Split(',');
                    parameter.PrecisionText = parts[0].Trim();
                    parameter.ScaleText = parts[1].Trim();
                }
                else
                {
                    parameter.LengthText = length;
                }
            }

            parameter.IsOutput = match.Groups["output"].Success;
            parameter.Comment = match.Groups["comment"].Success ? match.Groups["comment"].Value : null;

            return parameter;
        }


        public class StoredProcedureParameter
        {
            public string? Name { get; set; }
            public string? SqlType { get; set; }
            public string? LengthText { get; set; }
            public bool IsOutput { get; set; }
            public string? Comment { get; set; }
            public string? PrecisionText { get; set; }
            public string? ScaleText { get; set; }
        }
    }
}
