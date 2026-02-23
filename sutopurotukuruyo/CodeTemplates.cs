using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace sutopurotukuruyo
{
    public static class CodeTemplates
    {
        // タイトル
        public static readonly string MethodTitle = "Private {0} {1}()" + Environment.NewLine;
        // トランザクション設定
        public static readonly string TransactionSet = "Dim transaction As SqlTransaction = {0}.BeginTransaction()" + Environment.NewLine;
        // ヘッダー
        public static readonly string MethodHeader = "Try" + Environment.NewLine +
                                                     "Dim {0} As New SqlCommand" + Environment.NewLine +
                                                     "{0}.Connection = {1}" + Environment.NewLine +
                                                     "{0}.CommandType = CommandType.StoredProcedure" + Environment.NewLine +
                                                     "{0}.CommandText = \"[{2}]\"" + Environment.NewLine +
                                                     "{0}.Parameters.Clear()" + Environment.NewLine;
        // トランザクション開始
        public static readonly string TransactionStart = "{0}.Transaction = transaction" + Environment.NewLine;

        // 区切りラベル
        public static readonly string ParameterHeader = "★パラメータ" + Environment.NewLine;
        // パラメータ部分
        public const string AddParameterTemplate = "{0}.Parameters.Add(\"@{1}\", SqlDbType.{2}{3}).Value = ";
        public const string AddOutputParameterTemplate = "{0}.Parameters.Add(\"@{1}\", SqlDbType.{2}{3}).Direction = ParameterDirection.Output";

        // ReturnValue
        public static readonly string ReturnValue = "★ReturnValue" + Environment.NewLine +
                                                    "{0}.Parameters.Add(\"ReturnValue\", SqlDbType.Int).Direction = ParameterDirection.ReturnValue";

        // 実行
        public static readonly string ExecuteNonQuery = "{0}.ExecuteNonQuery()" + Environment.NewLine + Environment.NewLine;

        // ReturnValueIf
        public static readonly string ReturnValueIf = "If {0}.Parameters(\"ReturnValue\").Value = 0 Then" + Environment.NewLine;
        // ReturnValueIfEnd
        public static readonly string ReturnValueIfEnd = "End If" + Environment.NewLine;
        // トランザクションをコミット
        public static readonly string TransactionCommit = "transaction.Commit()" + Environment.NewLine;
        // フッター
        public static readonly string MethodFooter = "Catch ex As SqlException" + Environment.NewLine +
                                             "Dim msgOutForm As New MsgOut" + Environment.NewLine +
                                             "msgOutForm.PubF_SetMsgInf(\"DB\", \"0001\", \"DBエラー\", \"OK\", \"FWOZ\")" + Environment.NewLine +
                                             "msgOutForm.PubF_SetSQLErrInf(\"{0}\", Pub_MsgOut_Act_SEL, ex.Number.ToString(), ex.Class, ex.LineNumber, ex.ErrorCode, ex.State, ex.Message)" + Environment.NewLine +
                                             Environment.NewLine +
                                             "' 画面有り" + Environment.NewLine +
                                             "msgOutForm.PubF_Exec()" + Environment.NewLine +
                                             Environment.NewLine;
        // トランザクションをロールバック
        public static readonly string TransactionRollback = "Try" + Environment.NewLine +
                                                            "transaction.Rollback()" + Environment.NewLine +
                                                            "Me.Close()" + Environment.NewLine +
                                                            "Catch rollbackEx As Exception" + Environment.NewLine +
                                                            "Console.WriteLine(\"ロールバック中にエラー: \" & rollbackEx.Message)" + Environment.NewLine +
                                                            "End Try" + Environment.NewLine;

        // Try終了
        public static readonly string TryEnd = "End Try" + Environment.NewLine;
        // メソッド終了
        public static readonly string MethodEnd = "End {0}" + Environment.NewLine;

        // return
        public static readonly string RetrunFalse = "Return False" + Environment.NewLine;
        public static readonly string RetrunTrue = "Return True" + Environment.NewLine;

    }
}
