namespace Game2048
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpGameBoard = new System.Windows.Forms.TableLayoutPanel();
            this.lCellValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lBiggestField = new System.Windows.Forms.Label();
            this.lFieldsSum = new System.Windows.Forms.Label();
            this.lLastMoveResult = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.bClickMove = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpGameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGameBoard
            // 
            this.tlpGameBoard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpGameBoard.ColumnCount = 2;
            this.tlpGameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameBoard.Controls.Add(this.lCellValue, 0, 0);
            this.tlpGameBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGameBoard.Location = new System.Drawing.Point(0, 0);
            this.tlpGameBoard.Name = "tlpGameBoard";
            this.tlpGameBoard.RowCount = 2;
            this.tlpGameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGameBoard.Size = new System.Drawing.Size(428, 336);
            this.tlpGameBoard.TabIndex = 0;
            // 
            // lCellValue
            // 
            this.lCellValue.AutoSize = true;
            this.lCellValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lCellValue.Location = new System.Drawing.Point(4, 1);
            this.lCellValue.Name = "lCellValue";
            this.lCellValue.Size = new System.Drawing.Size(206, 166);
            this.lCellValue.TabIndex = 0;
            this.lCellValue.Text = "2";
            this.lCellValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Biggest field:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fields sum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Last move:";
            // 
            // lBiggestField
            // 
            this.lBiggestField.AutoSize = true;
            this.lBiggestField.Location = new System.Drawing.Point(76, 13);
            this.lBiggestField.Name = "lBiggestField";
            this.lBiggestField.Size = new System.Drawing.Size(13, 13);
            this.lBiggestField.TabIndex = 4;
            this.lBiggestField.Text = "0";
            // 
            // lFieldsSum
            // 
            this.lFieldsSum.AutoSize = true;
            this.lFieldsSum.Location = new System.Drawing.Point(76, 26);
            this.lFieldsSum.Name = "lFieldsSum";
            this.lFieldsSum.Size = new System.Drawing.Size(13, 13);
            this.lFieldsSum.TabIndex = 5;
            this.lFieldsSum.Text = "0";
            // 
            // lLastMoveResult
            // 
            this.lLastMoveResult.AutoSize = true;
            this.lLastMoveResult.Location = new System.Drawing.Point(76, 39);
            this.lLastMoveResult.Name = "lLastMoveResult";
            this.lLastMoveResult.Size = new System.Drawing.Size(0, 13);
            this.lLastMoveResult.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Restart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(52, 67);
            this.nudWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(63, 20);
            this.nudWidth.TabIndex = 8;
            this.nudWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Width:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Height:";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(52, 93);
            this.nudHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(63, 20);
            this.nudHeight.TabIndex = 11;
            this.nudHeight.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // bClickMove
            // 
            this.bClickMove.Location = new System.Drawing.Point(197, 8);
            this.bClickMove.Name = "bClickMove";
            this.bClickMove.Size = new System.Drawing.Size(105, 23);
            this.bClickMove.TabIndex = 12;
            this.bClickMove.Text = "Click to move";
            this.bClickMove.UseVisualStyleBackColor = true;
            this.bClickMove.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlpGameBoard);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.bClickMove);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.nudHeight);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.lBiggestField);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.lFieldsSum);
            this.splitContainer1.Panel2.Controls.Add(this.nudWidth);
            this.splitContainer1.Panel2.Controls.Add(this.lLastMoveResult);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(50);
            this.splitContainer1.Size = new System.Drawing.Size(428, 462);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(452, 486);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 486);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View";
            this.Load += new System.EventHandler(this.View_Load);
            this.ResizeBegin += new System.EventHandler(this.View_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.View_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_KeyDown);
            this.tlpGameBoard.ResumeLayout(false);
            this.tlpGameBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpGameBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lBiggestField;
        private System.Windows.Forms.Label lFieldsSum;
        private System.Windows.Forms.Label lLastMoveResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label lCellValue;
        private System.Windows.Forms.Button bClickMove;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}