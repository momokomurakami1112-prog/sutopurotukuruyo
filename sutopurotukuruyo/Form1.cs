using System.Text;
using System.Text.RegularExpressions;

namespace sutopurotukuruyo
{
    public partial class Form1 : Form
    {
        MethodGenerater _methodGenerater;

        public Form1()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Initialize();
        }

        #region 初期設定

        private void Initialize()
        {
            // 画面設定
            SubRadioButton.Checked = true;
            NotUseTransactionRadioButton.Checked = true;
            SqlConnectionNameTextBox.Text = Properties.Settings.Default.ConnectionName;
            SqlCommandNameTextBox.Text = Properties.Settings.Default.CommandName;

            // イベント登録
            FilePathTextBox.DragEnter += FilePathTextBox_DragEnter;
            FilePathTextBox.DragDrop += FilePathTextBox_DragDrop;
        }


        // FilePathTextBoxイベントハンドラ
        private void FilePathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void FilePathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                FilePathTextBox.Text = files[0];
            }
        }

        #endregion

        #region ファイル読込ボタン

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "ストアドプロシージャーを選択";
            dialog.Filter = "sqlファイル (*.sql)|*.sql|すべてのファイル (*.*)|*.*";
            dialog.Multiselect = false;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.LastFolderPath))
            {
                dialog.InitialDirectory = Properties.Settings.Default.LastFolderPath;
            }

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Properties.Settings.Default.LastFolderPath = Path.GetDirectoryName(dialog.FileName);
            FilePathTextBox.Text = dialog.FileName;
        }

        #endregion

        #region メソッド作成ボタン

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

            // インスタンス作成
            _methodGenerater = new MethodGenerater();
            // ユーザーの選択部分を取得
            GetUserSelectedOptions();

            if (string.IsNullOrWhiteSpace(SqlConnectionNameTextBox.Text))
            {
                string title = "確認";
                string message = "SQLConnectionの変数名が指定されていません。\n操作を続けますか？";
                DialogResult dialogResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.No) return;
            }
            if (string.IsNullOrWhiteSpace(SqlCommandNameTextBox.Text))
            {
                string title = "確認";
                string message = "SQLCommandの変数名が指定されていません。\n操作を続けますか？";
                DialogResult dialogResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.No) return;
            }

            // Input/Outputパラメータのみを取得
            //List<string> parameterLines = ExtractParameterLines(File.ReadAllLines(FilePathTextBox.Text, Encoding.GetEncoding("shift_jis")));
            List<string> parameterLines = _methodGenerater.ExtractParameterLines(File.ReadAllLines(FilePathTextBox.Text));
            if (parameterLines.Count == 0)
            {
                MessageBox.Show("パラメータが存在しません。");
                return;
            }

            // パラメータをデータクラスのリストとして取得
            List<StoredProcedureParameter> parameterList = _methodGenerater.GetParameterDataList(parameterLines);

            // テキストボックスをクリア
            MethodTextBox.Clear();

            // ヘッダー組立
            StringBuilder Header = _methodGenerater.GenerateMethodHeader();
            // ボディ組立
            StringBuilder parameter = _methodGenerater.GenerateMethodParameter(parameterList);
            // フッター組立
            StringBuilder footer = _methodGenerater.GenerateMethodFooter();

            // TextBox に表示
            MethodTextBox.Text = Header.ToString() + parameter.ToString() + footer.ToString();
        }

        // ユーザーの選択部分を取得
        private void GetUserSelectedOptions()
        {
            _methodGenerater._isSub = SubRadioButton.Checked;
            _methodGenerater._isTransaction = UseTransactionRadioButton.Checked;
            _methodGenerater._sqlConnectionName = SqlConnectionNameTextBox.Text ?? "";
            _methodGenerater._sqlCommandName = SqlCommandNameTextBox.Text ?? "";
            _methodGenerater._fileName = Path.GetFileNameWithoutExtension(FilePathTextBox.Text);

            Properties.Settings.Default.ConnectionName = SqlConnectionNameTextBox.Text;
            Properties.Settings.Default.CommandName = SqlCommandNameTextBox.Text;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region コードをコピーボタン

        private async void SelectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MethodTextBox.Text))
            {
                MessageBox.Show("メソッドが作成されていません。");
                return;
            }
            MethodTextBox.Focus();
            MethodTextBox.SelectAll();
            MethodTextBox.Copy();

            CopyStatusLabel.Visible = true;
            await Task.Delay(1500);
            CopyStatusLabel.Visible = false;
        }

        #endregion
    }
}
