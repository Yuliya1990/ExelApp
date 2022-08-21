using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExelApp
{
    public partial class Excel : Form
    {
        private Grid GR = new Grid();
        private _26BasedSystem sys26 = new _26BasedSystem();
        public Excel()
        {
            InitializeComponent();
            InitTable(GR.RowCount, GR.ColCount);
            this.KeyPreview = true;
        }
        private void InitTable(int rowCount, int colCount)
        {
            for (int i = 0; i < colCount; i++)
            {
                string colName = sys26.To26Sys(i);
                dataGridView1.Columns.Add(colName, colName);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                dataGridView1.Rows[i].Height = 28;
            }
        }
        private void Exel_Load(object sender, EventArgs e)
        {

        }
        private void bjToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
        private void rowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello!");
        }
        private void EnterBtn_Click(object sender, EventArgs e)
        {
  
                string expression = textBox.Text;
                if (expression == "")
                    return;

                int col = dataGridView1.SelectedCells[0].ColumnIndex;
                int row = dataGridView1.SelectedCells[0].RowIndex;

                GR.ChangeCell(row, col, expression, dataGridView1);

                dataGridView1[col, row].Value = GR.grid[row][col].Value;  
                textBox.Text = GR.grid[row][col].Expression;
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int col = dataGridView1.SelectedCells[0].ColumnIndex;
                int row = dataGridView1.SelectedCells[0].RowIndex;

                string expr = GR.grid[row][col].Expression;
                string value = GR.grid[row][col].Value;

                textBox.Text = expr;
                textBox.Focus();
            }
            catch { }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void FormClick(object sender, EventArgs e)
        {

        }
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    EnterBtn_Click(null, null);
            }
            catch { }
        }
        private void addRowBtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow { Height = 28};
            dataGridView1.Rows.Add(row);
            GR.AddRow(dataGridView1);
        }

       private void deleteRowBtn_Click(object sender, EventArgs e)
        {
           int curRow = GR.RowCount - 1;
            if (!GR.DeleteRow(dataGridView1, this))
                return;
            dataGridView1.Rows.RemoveAt(curRow);
        }

        private void addColumnBtn_Click(object sender, EventArgs e)
        {
            string colname = sys26.To26Sys(GR.ColCount);
            dataGridView1.Columns.Add(colname, colname);
            GR.AddColumn(dataGridView1);
        }

        private void deleteColumnBtn_Click(object sender, EventArgs e)
        {
            int curCol = GR.ColCount - 1;
            if (!GR.DeleteCol(dataGridView1, this))
                return;

            dataGridView1.Columns.RemoveAt(curCol);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GridFile|*.txt";
            saveFileDialog.Title = "Save Grid File";
            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                GR.Save(sw);
                sw.Close();
                fs.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DGridFile|*.txt";
            openFileDialog.Title = "Select Grid File";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            StreamReader sr = new StreamReader(openFileDialog.FileName);
            GR.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            Int32.TryParse(sr.ReadLine(), out int row);
            Int32.TryParse(sr.ReadLine(), out int col);

            InitTable(row, col);
            GR.Open(row, col, sr, dataGridView1);
            sr.Close();
        }
    }
}
