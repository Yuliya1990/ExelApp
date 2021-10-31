using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ExelApp
{
    public class Grid
    {
        private const int _initColCount = 10;
        private const int _initRowCount = 10;
        public int ColCount;
        public int RowCount;
        private _26BasedSystem sys26 = new _26BasedSystem();
        public Dictionary<string, string> dictionary = new Dictionary<string, string>();
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
        public void Clear()
        {
            foreach(List<Cell> list in grid)
            {
                list.Clear();
            }
            grid.Clear();
            dictionary.Clear();
            RowCount = 0;
            ColCount = 0;
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

            RowCount += 1;
            dataGridView.Rows[RowCount-1].HeaderCell.Value = (RowCount-1).ToString();
            //  for (int i = 0; i < RowCount; i++)
            // {
            //   dataGridView.Rows[i].HeaderCell.Value = i.ToString();
            //}

            RefreshReferences();

            foreach(List<Cell> list in grid)
            {
                foreach(Cell cell in list)
                {
                    if (cell.IDependCells != null)
                        foreach (Cell cell_in_ref in cell.IDependCells)// перебираю ячейки от кот я зависима
                            if (cell_in_ref.RowIndex == RowCount - 1)// если ячейка от кот я зависима из посл строки, кот мы ща создаем
                                if (!cell_in_ref.DependfromMeCells.Contains(cell))// если в ячейке от кот я зависимам в массиве зависящих от нее нет меня 
                                    cell_in_ref.DependfromMeCells.Add(cell);
                }
            }

            for (int i = 0; i < ColCount; i++)
                ChangeCell(RowCount - 1, i, "", dataGridView);
        }

        public void RefreshReferences()
        {
            foreach(List<Cell> list in grid)
                foreach(Cell cell in list)
                {
                    if (cell.IDependCells != null)
                        cell.IDependCells.Clear();
                    if (cell.New_IDependCells != null)
                        cell.New_IDependCells.Clear();

                    if (cell.Expression == "")
                        continue;
                    string new_expr = cell.Expression;
                    if(cell.Expression[0]=='=')
                    {
                        new_expr = ConvertReferences(cell.RowIndex, cell.ColIndex, cell.Expression);
                        cell.IDependCells.AddRange(cell.New_IDependCells);
                    }
                }
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
                    string new_expression = ConvertReferences(row, col, expr);
                    if (new_expression != "")
                        new_expression = new_expression.Remove(0, 1);
                    //else return;

                    if (!grid[row][col].CheckForLoop(grid[row][col].New_IDependCells))
                    {
                        MessageBox.Show("There is a loop! Change the expression.");
                        return;
                    }

                    grid[row][col].AddPointersAndReferences();
                    value = Calculate(new_expression);
                    if (value == "error")
                    {
                        MessageBox.Show("Error in cell " + grid[row][col].Name + "!");
                        return;
                    }

                    grid[row][col].Value = value;
                    dictionary[grid[row][col].Name] = value;

                    foreach (Cell cell in grid[row][col].DependfromMeCells)
                        RefreshCellAndPointers(cell, dataGridView);
                }
            }
        }
        public bool RefreshCellAndPointers(Cell cell, DataGridView dataGridView)
        {
            cell.New_IDependCells.Clear();
            string new_expr = ConvertReferences(cell.RowIndex, cell.ColIndex, cell.Expression);
            new_expr = new_expr.Remove(0, 1);
            string value = Calculate(new_expr);
            if(value=="error")
            {
                MessageBox.Show("Error in cell" + cell.Name + '!');
                return false;
            }

            grid[cell.RowIndex][cell.ColIndex].Value = value;
            dictionary[grid[cell.ColIndex][cell.RowIndex].Name] = value;
            dataGridView[cell.ColIndex, cell.RowIndex].Value = value;


            foreach (Cell point in cell.DependfromMeCells)
            {
                bool isOk = RefreshCellAndPointers(point, dataGridView);
                if (!isOk)
                    return false;
            }

            return true;
        }
        public string ConvertReferences(int row, int col, string expr)
        {
            string cellPattern = @"[A-Z]+[0-9]+";
            Regex regex = new Regex(cellPattern, RegexOptions.IgnoreCase);
            int[] nums;
            foreach(Match match in regex.Matches(expr))// перебир все ячейки кот есть в моей формуле
                if(dictionary.ContainsKey(match.Value))
                {
                    nums = sys26.From26Sys(match.Value);
                    if (nums[1] < grid.Count && nums[0] < grid[nums[1]].Count)// если строки-столбца на кот мы ссылаемся пока нет, то и яч не добавится
                        grid[row][col].New_IDependCells.Add(grid[nums[1]][nums[0]]);// наполняем массив клеток от кот зависима текущая клетка
                }


            MatchEvaluator myEvaluator = new MatchEvaluator(RefToValue);
            string new_expression = regex.Replace(expr, myEvaluator);
            return new_expression;
        }
        public string RefToValue(Match m)
        {
            if (dictionary.ContainsKey(m.Value))
            {
                if (dictionary[m.Value] == "")
                    return "0";
                else
                    return dictionary[m.Value];
            }
            else
                return m.Value;
        }
        public string Calculate(string expr)
        {
            string res = null;
            try
            {
                res = Convert.ToString(Calculator.Evaluate(expr));
                return res;
            }
            catch
            {
                return "Error";
            }
        }


    }
}
