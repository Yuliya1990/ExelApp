using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace ExelApp
{
    public class Grid
    {
        private const int _initColCount = 10;
        private const int _initRowCount = 10;
        public int ColCount;
        public int RowCount;
        private _26BasedSystem sys26 = new _26BasedSystem();
        public Dictionary<string, string> dictionary = new Dictionary<string, string>(); //Name - value
        public List<List<Cell>> grid = new List<List<Cell>>();
        public Grid()
        {
            SetGrid(_initRowCount, _initColCount);
        }
        public void SetGrid(int row, int col)
        {
            Clear();
            ColCount = col;
            RowCount = row;
            for (int i = 0; i < RowCount; i++)
            {
                List<Cell> newRow = new List<Cell>();
                for (int j = 0; j < ColCount; j++)
                {
                    string name = sys26.To26Sys(j) + i.ToString();
                    newRow.Add(new Cell(name, i, j));
                    dictionary.Add(name, "");
                }
                grid.Add(newRow);
            }
        }
        public void AddRow(DataGridView dataGridView)
        {
            List<Cell> newRow = new List<Cell>();
            for (int j = 0; j < ColCount; j++)
            {
                string name = sys26.To26Sys(j) + RowCount.ToString();
                newRow.Add(new Cell(name, RowCount, j));
                dictionary.Add(name, "");
            }
            grid.Add(newRow);
            RowCount++;
            dataGridView.Rows[RowCount - 1].HeaderCell.Value = (RowCount - 1).ToString();
        }
        public void AddColumn(DataGridView dataGridView)
        {
            for (int i = 0; i < RowCount; i++)
            {
                string name = sys26.To26Sys(ColCount) + i.ToString();
                grid[i].Add(new Cell(name, i, ColCount));
                dictionary.Add(name, "");
            }
            ColCount++;
        }
        public bool DeleteRow(DataGridView dataGridView, Excel form)
        {
            if (RowCount == 0)
                return false;

            DialogResult result = new DialogResult();
            bool isResult = false;

            foreach (Cell cell in grid[RowCount - 1])
            {
                if (cell.DependfromMeCells.Count != 0)
                {
                    result = MessageBox.Show("Do you really want to delete this row? All cells that depend on cells in this row will be cleared.", "Confirmation", MessageBoxButtons.YesNo);
                    isResult = true;
                    break;
                }
            }

            if (isResult && result == DialogResult.No) //если не хотим удалять и нажали НЕТ
                return false;

            else //если нажали ДА (хотим удалить) или зависимых клеток не было
            {
                foreach (Cell cell in grid[RowCount - 1])
                {
                    dictionary.Remove(cell.Name);
                    foreach (Cell point in cell.DependfromMeCells)
                    {
                        RefreshCellAndPointers(point, dataGridView);
                    }
                }
                grid.Remove(grid[RowCount - 1]);
            }
            RowCount--;
            return true;
        }
        public bool DeleteCol(DataGridView dataGridView, Excel form)
        {
            if (ColCount == 1)
                return false;

            DialogResult result = new DialogResult();
            bool isResult = false;

            for (int i = 0; i < RowCount; i++)
            {
                if (grid[i][ColCount - 1].DependfromMeCells.Count != 0)
                {
                    result = MessageBox.Show("Do you really want to delete this column? All cells that depend on cells in this column will be cleared.", "Confirmation", MessageBoxButtons.YesNo);
                    isResult = true;
                    break;
                }
            }
            if (isResult && result == DialogResult.No) //если не хотим удалять и нажали НЕТ
                return false;

            else //если нажали ДА (хотим удалить) или зависимых клеток не было
            {
                for (int i = 0; i < RowCount; i++)
                {
                    dictionary.Remove(grid[i][ColCount - 1].Name);
                    foreach (Cell point in grid[i][ColCount - 1].DependfromMeCells)
                    {
                        RefreshCellAndPointers(point, dataGridView);
                    }
                    grid[i].RemoveAt(ColCount - 1);
                }
            }
            ColCount--;
            return true;
        }
        public void Clear()
        {
            foreach (List<Cell> list in grid)
            {
                list.Clear();
            }
            grid.Clear();
            dictionary.Clear();
            RowCount = 0;
            ColCount = 0;
        }
        public void ChangeCell(int row, int col, string expr, DataGridView dataGridView)
        {
            grid[row][col].DelIDependCells();
            grid[row][col].Expression = expr;
            grid[row][col].New_IDependCells.Clear();
            string value = expr;

            if (!string.IsNullOrEmpty(expr))
            {
                if (expr[0] != '=')
                {
                    grid[row][col].Value = expr;
                    dictionary[grid[row][col].Name] = expr;

                    foreach (Cell cell in grid[row][col].DependfromMeCells)
                        RefreshCellAndPointers(cell, dataGridView);
                }
                else
                {
                    try
                    {
                        string new_expression = ConvertReferences(row, col, expr);

                        if (new_expression != "")
                            new_expression = new_expression.Remove(0, 1);
                        else
                        {
                            MessageBox.Show("Contains non-existent cell!");
                            grid[row][col].Expression = "#Error";
                            grid[row][col].Value = "#Error";
                            dictionary[grid[row][col].Name] = "#Error";

                            foreach (Cell cell in grid[row][col].DependfromMeCells)
                                RefreshCellAndPointers(cell, dataGridView);
                            throw new Exception("Contain  non-existent cell!");
                        }


                        if (!grid[row][col].CheckForLoop(grid[row][col].New_IDependCells))
                        {
                            MessageBox.Show("There is a loop! Change the expression.");
                            grid[row][col].Value = "#Loop";
                            throw new Exception("There is a loop! Change the expression.");
                        }

                        grid[row][col].AddPointersAndReferences(); //если все ОК - обновляем IDependCells
                        value = Calculate(new_expression);
                        if (value == "Error")
                        {
                            MessageBox.Show("Error in cell " + grid[row][col].Name + "!");
                            throw new Exception("Error in cell " + grid[row][col].Name + "!");
                        }

                        grid[row][col].Value = value;
                        dictionary[grid[row][col].Name] = value;

                        foreach (Cell cell in grid[row][col].DependfromMeCells)
                            RefreshCellAndPointers(cell, dataGridView);

                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
            }
        }
        private bool RefreshCellAndPointers(Cell cell, DataGridView dataGridView)
        {
            cell.New_IDependCells.Clear();
            string new_expr = ConvertReferences(cell.RowIndex, cell.ColIndex, cell.Expression);

            if (new_expr == "")
            {
                cell.Expression = "#Error";
                cell.Value = "#Error";
                dictionary[cell.Name] = "#Error";

                dataGridView[cell.ColIndex, cell.RowIndex].Value = cell.Value;

                foreach (Cell point in cell.DependfromMeCells)
                    RefreshCellAndPointers(point, dataGridView);
            }
            else
            {
                new_expr = new_expr.Remove(0, 1);
                string value = "";
                try
                {
                    value = Calculate(new_expr);
                }
                catch (DivideByZeroException ex)
                {
                    value = ex.Message;
                }
                catch (Exception ex)
                {
                    value = "Error";
                }
                if (value == "Division by zero error")
                {
                    MessageBox.Show("Division by zero error");
                    return false;
                }
                if (value == "error")
                {
                    MessageBox.Show("Error in cell" + cell.Name + '!');
                    return false;
                }

                cell.Value = value;
                dictionary[cell.Name] = value;
                dataGridView[cell.ColIndex, cell.RowIndex].Value = value;

                foreach (Cell point in cell.DependfromMeCells)
                {
                    bool isOk = RefreshCellAndPointers(point, dataGridView);
                    if (!isOk)
                        return false;
                }
            }

            return true;
        }


        public MatchCollection FindAllCells(string expr)
        {
            string cellPattern = @"[A-Z]+[0-9]+";
            Regex regex = new Regex(cellPattern, RegexOptions.IgnoreCase); //передается регулярное выражение CellPattern для поиска в expr
            MatchCollection matches = regex.Matches(expr); // принимает строку expr, в которой надо найти имена клеток, и возвращает коллекцию найденных клеток
            return matches;
        }

        private string ConvertReferences(int row, int col, string expr)
        {
            var matches = FindAllCells(expr);
            int[] nums;
            foreach (Match match in matches)// перебираем все клетки, что нашли
                if (dictionary.ContainsKey(match.Value)) //если имя клетки есть в dictionary
                {
                    nums = sys26.From26Sys(match.Value); //конвертируем имя клетки в индексы (колонка, ряд)
                    grid[row][col].New_IDependCells.Add(grid[nums[1]][nums[0]]);// наполняем буферный массив клеток от кот зависима текущая клетка
                }
                else
                {
                    return "";
                }
            MatchEvaluator myEvaluator = new MatchEvaluator(RefToValue);
            string cellPattern = @"[A-Z]+[0-9]+";
            Regex regex = new Regex(cellPattern, RegexOptions.IgnoreCase);
            string new_expression = regex.Replace(expr, myEvaluator);
            return new_expression;
        }
        private string RefToValue(Match m)
        {
            if (dictionary[m.Value] == "" || dictionary[m.Value] == "#Error")
                return "0";
            else
                return dictionary[m.Value];
        }
        public string Calculate(string expr)
        {
            string res = null;

            res = Convert.ToString(Calculator.Evaluate(expr));
            if (res == "∞" || res=="-2147483648")
                {
                    throw new DivideByZeroException("Division by zero error");
                }
                return res;
            
           
        }
        public void Save(StreamWriter sw)
        {
            sw.WriteLine(RowCount);
            sw.WriteLine(ColCount);
            foreach (List<Cell> list in grid)
            {
                foreach(Cell cell in list)
                {
                    sw.WriteLine(cell.Name);
                    sw.WriteLine(cell.Expression);
                    sw.WriteLine(cell.Value);

                    if (cell.IDependCells == null)
                        sw.WriteLine(0);
                    else
                    {
                        sw.WriteLine(cell.IDependCells.Count);
                        foreach (Cell point in cell.IDependCells)
                            sw.WriteLine(point.Name);
                    }
                    if(cell.DependfromMeCells==null)
                        sw.WriteLine(0);
                    else
                    {
                        sw.WriteLine(cell.DependfromMeCells.Count);
                        foreach (Cell point in cell.DependfromMeCells)
                            sw.WriteLine(point.Name);
                    }
                }
            }
        }
        public void Open(int row, int col, StreamReader sr, DataGridView dataGridView)
        {
            for (int r = 0; r < row; r++)
            {
                for(int c = 0; c < col; c++)
                {
                    string name = sr.ReadLine();
                    string expr = sr.ReadLine();
                    string value = sr.ReadLine();
                    if (expr != "")
                        dictionary[name] = value;
                    else
                        dictionary[name] = "";

                    int iDepCount = Convert.ToInt32(sr.ReadLine());
                    List<Cell> newIDep = new List<Cell>();
                    string iDep;
                    for (int i=0; i<iDepCount; i++)
                    {
                        iDep = sr.ReadLine();
                        newIDep.Add(grid[sys26.From26Sys(iDep)[1]][sys26.From26Sys(iDep)[0]]);
                    }

                    int depFromMeCount = Convert.ToInt32(sr.ReadLine());
                    List<Cell> newDepFromMe = new List<Cell>();
                    string depFromMe;
                    for (int i=0; i<depFromMeCount; i++)
                    {
                        depFromMe = sr.ReadLine();
                        newDepFromMe.Add(grid[sys26.From26Sys(depFromMe)[1]][sys26.From26Sys(depFromMe)[0]]);
                    }
                    grid[r][c].SetCell(value, expr, newIDep, newDepFromMe);
                    int icol = grid[r][c].ColIndex;
                    int irow = grid[r][c].RowIndex;
                    dataGridView[icol, irow].Value = dictionary[name];
                }
            }
        }
    }
}
