using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sutopurotukuruyo
{
    public class MethodGenerater
    {
        public bool _isTransaction;
        public string _fileName;
        public string _sqlConnectionName;
        public string _sqlCommandName;

        public enum MethodType
        {
            Sub,
            Function,
            Parameter
        }
        public MethodType _methodType;

        // パラメータ部分を取得
        public List<string> ExtractParameterLines(string[] allLines)
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
        public List<StoredProcedureParameter> GetParameterDataList(List<string> parameterLines)
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
        public StoredProcedureParameter ParseParameter(string line)
        {
            string pattern =
                @"@(?<name>\w+)\s+" +
                @"(?<type>\w+)" +
                @"(\((?<length>[^)]+)\))?\s*" +
                @"(=\s*[^ \t]+)?\s*" +
                @"(?<output>OUTPUT)?\s*" +
                @"(--\s*<(?<comment>[^>]+)>)?";

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
        public string ConvertToSqlDbType(string sqlType)
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

        // メソッドのヘッダー部分を作成
        public StringBuilder GenerateMethodHeader()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string methodType = _methodType.ToString();
            stringBuilder.Append(string.Format(CodeTemplates.MethodTitle, methodType, _fileName));
            if (_isTransaction)
            {
                stringBuilder.Append(string.Format(CodeTemplates.TransactionSet, _sqlConnectionName));
            }
            stringBuilder.Append(string.Format(CodeTemplates.MethodHeader, _sqlCommandName, _sqlConnectionName, _fileName));
            if (_isTransaction)
            {
                stringBuilder.Append(string.Format(CodeTemplates.TransactionStart, _sqlConnectionName));
            }
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder;
        }

        // メソッドのパラメータ部分を作成
        public StringBuilder GenerateMethodParameter(List<StoredProcedureParameter> parameterList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(CodeTemplates.ParameterHeader);
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
                    stringBuilder.AppendLine(string.Format(CodeTemplates.AddParameterTemplate, _sqlCommandName, parameter.Name, sqlDbType, ""));
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
                        stringBuilder.AppendLine(string.Format(CodeTemplates.AddOutputParameterTemplate, _sqlCommandName, parameter.Name, sqlDbType, ""));
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
                        ? string.Format(CodeTemplates.AddOutputParameterTemplate, _sqlCommandName, parameter.Name, sqlDbType, lengthText)
                        : string.Format(CodeTemplates.AddParameterTemplate, _sqlCommandName, parameter.Name, sqlDbType, lengthText);

                    stringBuilder.AppendLine(line);
                }
            }
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(string.Format(CodeTemplates.ReturnValue, _sqlCommandName));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder;
        }

        // メソッドのフッター部分を作成
        public StringBuilder GenerateMethodFooter()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format(CodeTemplates.ExecuteNonQuery, _sqlCommandName));
            stringBuilder.Append(string.Format(CodeTemplates.ReturnValueIf, _sqlCommandName));
            if (_isTransaction)
            {
                stringBuilder.Append(string.Format(CodeTemplates.TransactionCommit));
            }
            stringBuilder.Append(string.Format(CodeTemplates.ReturnValueIfEnd));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(string.Format(CodeTemplates.MethodFooter, _fileName));
            if (_isTransaction)
            {
                stringBuilder.Append(string.Format(CodeTemplates.TransactionRollback));
            }
            if (_methodType == MethodType.Function)
            {
                stringBuilder.Append(CodeTemplates.RetrunFalse);
            }
            stringBuilder.Append(CodeTemplates.TryEnd);
            if (_methodType == MethodType.Function)
            {
                stringBuilder.Append(CodeTemplates.RetrunTrue);
            }
            string methodType = _methodType.ToString();
            stringBuilder.Append(string.Format(CodeTemplates.MethodEnd, methodType));

            return stringBuilder;
        }
    }
}
