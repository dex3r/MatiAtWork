using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Game2048
{
    public partial class View : Form
    {
        private ITheGame game;
        private Label[,] labels;
        private int boardWidth;
        private int boardHeight;

        private Label originalLabel;

        internal static readonly int GWL_EXSTYLE = -20;
        internal static readonly int WS_EX_COMPOSITED = 0x02000000;

        [DllImport("user32")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public View()
        {
            InitializeComponent();

            originalLabel = lCellValue;

            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            style |= WS_EX_COMPOSITED;
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }

        private void View_Load(object sender, EventArgs e)
        {
            Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            this.SuspendLayout();
            tlpGameBoard.SuspendLayout();

            boardWidth = (int)nudWidth.Value;
            boardHeight = (int)nudHeight.Value;

            game = new TheGame();
            game.Initialize(boardWidth, boardHeight);

            tlpGameBoard.ColumnCount = boardWidth;
            tlpGameBoard.RowCount = boardHeight;

            tlpGameBoard.ColumnStyles.Clear();

            for (int i = 0; i < tlpGameBoard.ColumnCount; i++)
            {
                var columnStyle = new ColumnStyle(SizeType.Percent, 100f);

                tlpGameBoard.ColumnStyles.Add(columnStyle);
            }

            tlpGameBoard.RowStyles.Clear();

            for (int i = 0; i < tlpGameBoard.RowCount; i++)
            {
                var rowStyle = new RowStyle(SizeType.Percent, 100f);

                tlpGameBoard.RowStyles.Add(rowStyle);
            }

            labels = new Label[boardWidth, boardHeight];
            tlpGameBoard.Controls.Clear();

            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    var label = new Label();
                    labels[x, y] = label;

                    label.AutoSize = true;
                    label.Text = "2";
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    tlpGameBoard.Controls.Add(label, x, y);
                }
            }

            tlpGameBoard.ResumeLayout();
            this.ResumeLayout();

            RefreshView();
        }

        private void RefreshView()
        {
            this.SuspendLayout();
            tlpGameBoard.SuspendLayout();

            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    Label label = labels[x, y];
                    int value = game[x, y];
                    string finalText = "";

                    if (value > 0)
                    {
                        value = (int)Math.Pow(2, value);
                        finalText = value.ToString();
                    }

                    label.Text = finalText;
                }
            }

            lBiggestField.Text = game.GetBiggestFieldPow2().ToString();
            lFieldsSum.Text = game.GetFieldsSumPow2().ToString();

            tlpGameBoard.ResumeLayout();
            this.ResumeLayout();
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    Move(MoveDirection.Up);
                    break;
                case Keys.Down:
                case Keys.S:
                    Move(MoveDirection.Down);
                    break;
                case Keys.Left:
                case Keys.A:
                    Move(MoveDirection.Left);
                    break;
                case Keys.Right:
                case Keys.D:
                    Move(MoveDirection.Right);
                    break;
            }
        }

        private void Move(MoveDirection direction)
        {
            if (game.MakeMove(direction))
            {
                lLastMoveResult.Text = "Successfull";
            }
            else
            {
                lLastMoveResult.Text = "Failed";
            }

            RefreshView();
        }

        private void View_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void View_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout();
        }
    }
}
