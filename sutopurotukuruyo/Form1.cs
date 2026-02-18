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

        }

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
                @"(\((?<length>[^\)]+)\))?\s*" +
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
            parameter.LengthText = match.Groups["length"].Success
                ? match.Groups["length"].Value
                : null;

            parameter.IsOutput = match.Groups["output"].Success;

            parameter.Comment = match.Groups["comment"].Success
                ? match.Groups["comment"].Value
                : null;

            return parameter;
        }

    }

    public class StoredProcedureParameter
    {
        public string? Name { get; set; }
        public string? SqlType { get; set; }
        public string? LengthText { get; set; }
        public bool? IsOutput { get; set; }
        public string? Comment { get; set; }
    }
}
