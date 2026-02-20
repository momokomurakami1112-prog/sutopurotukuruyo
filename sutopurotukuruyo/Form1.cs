using System.Text;
using System.Text.RegularExpressions;
using sutopurotukuruyo.Templates;

namespace sutopurotukuruyo
{
    public partial class Form1 : Form
    {
        bool _isSub;
        bool _isTransaction;
        string _sqlConnectionName;
        string _sqlCommandName;

        public Form1()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            SubRadioButton.Checked = true;
            NotUseTransactionRadioButton.Checked = true;
            SqlConnectionNameTextBox.Text = "_sqlConnection";
            SqlCommandNameTextBox.Text = "_sqlCommand";
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

            // ユーザーの選択部分を取得
            GetUserSelectedOptions();

            // Input/Outputパラメータのみを取得
            List<string> parameterLines = ExtractParameterLines(File.ReadAllLines(FilePathTextBox.Text, Encoding.GetEncoding("shift_jis")));

            // パラメータをデータクラスのリストとして取得
            List<StoredProcedureParameter> parameterList = GetParameterDataList(parameterLines);

            // テキストボックスをクリア
            MethodTextBox.Clear();

            // ヘッダー組立
            //StringBuilder header = new StringBuilder();
            // ボディ組立
            StringBuilder parameter = GenerateMethodParameter(parameterList);
            // フッター組立
            StringBuilder footer = new StringBuilder();

            // TextBox に表示
            MethodTextBox.Text = parameter.ToString();
        }

        // ユーザーの選択部分を取得
        private void GetUserSelectedOptions()
        {
            _isSub = SubRadioButton.Checked;
            _isTransaction = UseTransactionRadioButton.Checked;
            _sqlConnectionName = SqlConnectionNameTextBox.Text ?? "";
            _sqlCommandName = SqlCommandNameTextBox.Text ?? "";
        }

        // パラメータ部分を取得
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

        // パラメータをデータクラスに変換
        private List<StoredProcedureParameter> GetParameterDataList(List<string> parameterLines)
        {
            List<StoredProcedureParameter> parameterList = new List<StoredProcedureParameter>();

            foreach (string line in parameterLines)
            {
                StoredProcedureParameter parameter = ParseParameter(line);

                if (parameter != null)
                {
                    parameterList.Add(parameter);
                }
            }

            return parameterList;
        }

        // パラメータ解析
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

                // Decimalの場合は Precision / Scale に分ける
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

        // パラメータのTypeを変換
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

        // メソッドのパラメータ部分を作成
        private StringBuilder GenerateMethodParameter(List<StoredProcedureParameter> parameterList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var parameter in parameterList)
            {
                // コメント行
                if (!string.IsNullOrWhiteSpace(parameter.Comment))
                {
                    stringBuilder.AppendLine($"' {parameter.Comment}");
                }

                string sqlDbType = ConvertToSqlDbType(parameter.SqlType);

                // Decimal系は特別扱い
                if (sqlDbType.Equals("Decimal", StringComparison.OrdinalIgnoreCase))
                {
                    // Add部分（Decimalはlengthなし）
                    stringBuilder.AppendLine(string.Format(CodeTemplates.AddParameterTemplate, parameter.Name, sqlDbType, ""));
                    // Precision
                    if (!string.IsNullOrWhiteSpace(parameter.PrecisionText))
                    {
                        stringBuilder.AppendLine(
                            $"sqlcmd.Parameters(\"@{parameter.Name}\").Precision = {parameter.PrecisionText}");
                    }

                    // Scale
                    if (!string.IsNullOrWhiteSpace(parameter.ScaleText))
                    {
                        stringBuilder.AppendLine(
                            $"sqlcmd.Parameters(\"@{parameter.Name}\").Scale = {parameter.ScaleText}");
                    }

                    // Outputの場合はDirection上書き
                    if (parameter.IsOutput)
                    {
                        stringBuilder.AppendLine(string.Format(CodeTemplates.AddOutputParameterTemplate, parameter.Name, sqlDbType, ""));
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
                        if (!string.IsNullOrWhiteSpace(parameter.LengthText))
                        {
                            lengthText = $", {parameter.LengthText}";
                        }
                    }

                    string line = parameter.IsOutput
                        ? string.Format(CodeTemplates.AddOutputParameterTemplate, parameter.Name, sqlDbType, lengthText)
                        : string.Format(CodeTemplates.AddParameterTemplate, parameter.Name, sqlDbType, lengthText);

                    stringBuilder.AppendLine(line);
                }
            }

            return stringBuilder;
        }


    }
}
