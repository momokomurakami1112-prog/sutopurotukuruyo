using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sutopurotukuruyo.Templates
{
    public static class CodeTemplates
    {
        // タイトル
        public static readonly string MethodTitle = "Private {0} {1}()\" + Environment.NewLine" + Environment.NewLine;
        // トランザクション設定
        public static readonly string TransactionSet = "Dim transaction As SqlTransaction = {0}.BeginTransaction()" + Environment.NewLine;
        // ヘッダー
        public static readonly string MethodHeader = "Try" + Environment.NewLine +
                                                     "Dim {0} As New SqlCommand" + Environment.NewLine +
                                                     "{0}.Connection = sqlcnn" + Environment.NewLine +
                                                     "{0}.CommandType = CommandType.StoredProcedure" + Environment.NewLine +
                                                     "{0}.CommandText = \"[{1}]\"" + Environment.NewLine +
                                                     "{0}.Parameters.Clear()" + Environment.NewLine;
        // トランザクション開始
        public static readonly string TransactionStart = "{0}.Transaction = transaction" + Environment.NewLine;

        // 区切りラベル
        public const string InputParameterHeader = "★入力パラメータ";
        public const string OutputParameterHeader = "★出力パラメータ";
        // パラメータ部分
        public const string AddParameterTemplate = "sqlcmd.Parameters.Add(\"@{0}\", SqlDbType.{1}{2}).Value = ";
        public const string AddOutputParameterTemplate = "sqlcmd.Parameters.Add(\"@{0}\", SqlDbType.{1}{2}).Direction = ParameterDirection.Output";

        // ReturnValue
        public static readonly string ReturnValue = "{0}.Parameters.Add(\"ReturnValue\", SqlDbType.Int).Direction = ParameterDirection.ReturnValue";

        // 実行
        public static readonly string ExecuteNonQuery = "{0}.ExecuteNonQuery()" + Environment.NewLine + Environment.NewLine +
                                                        "If {0}.Parameters(\"ReturnValue\").Value = 0 Then" + Environment.NewLine +
                                                        // トランザクションある場合は表示する
                                                        "{1}" + Environment.NewLine +
                                                        "{2}" + Environment.NewLine +
                                                        "" + Environment.NewLine +
                                                        "End If" + Environment.NewLine;
        // トランザクションをコミット
        public static readonly string TransactionCommit = "transaction.Commit()";
        // フッター
        public static readonly string MethodFooter = "Catch ex As SqlException" + Environment.NewLine +
                                             "Dim msgOutForm As New MsgOut" + Environment.NewLine +
                                             "msgOutForm.PubF_SetMsgInf(\"DB\", \"0001\", \"DBエラー\", \"OK\", \"FWOZ\")" + Environment.NewLine +
                                             "msgOutForm.PubF_SetSQLErrInf(\"{0}\", Pub_MsgOut_Act_SEL, ex.Number.ToString(), ex.Class, ex.LineNumber, ex.ErrorCode, ex.State, ex.Message)" + Environment.NewLine +
                                             Environment.NewLine +
                                             "' 画面有り" + Environment.NewLine +
                                             "msgOutForm.PubF_Exec()" + Environment.NewLine +
                                             Environment.NewLine + 
                                            // トランザクションがある場合ロールバックの記述
                                             "{1}";

        // トランザクションをロールバック
        public static readonly string TransactionRollback = "Try" + Environment.NewLine +
                                                            "transaction.Rollback()" + Environment.NewLine +
                                                            "Me.Close()" + Environment.NewLine +
                                                            "Catch rollbackEx As Exception" + Environment.NewLine +
                                                            "Console.WriteLine(\"ロールバック中にエラー: \" & rollbackEx.Message)" + Environment.NewLine +
                                                            "End Try" + Environment.NewLine;
        // 
        public static readonly string MethodEnd = "End Try" + Environment.NewLine +
                                                  "End {0}" + Environment.NewLine;

    }
}
