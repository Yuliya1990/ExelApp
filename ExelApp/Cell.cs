using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelApp
{
    public class Cell
    {
        public string Name { get; private set; } 
        public int ColIndex { get; private set; }
        public int RowIndex { get; private set; }

        public string Value;
        public string Expression;

        public List<Cell> DependfromMeCells = new List<Cell>(); //pointersToThis
        public List<Cell> IDependCells = new List<Cell>(); //referencesFromThis
        public List<Cell> New_IDependCells = new List<Cell>(); 

        public Cell(string name, int row, int col)
        {
            this.Name = name;
            this.RowIndex = row;
            this.ColIndex = col;
            this.Value = "0";
            this.Expression = "";
        }
        public void SetCell(string value, string expression, List<Cell> iDepend, List<Cell> depFromMe)
        {
            this.Value = value;
            this.Expression = expression;

            this.IDependCells.Clear();
            this.DependfromMeCells.Clear();

            this.IDependCells.AddRange(iDepend);
            this.DependfromMeCells.AddRange(depFromMe);
        }
        public bool CheckForLoop(List<Cell> check_list)
        {
            //предотвращает обращение в формуле клетки на саму же себя
            foreach(Cell susp in check_list)
            {
                if (susp.Name == Name)
                    return false;
            }
            foreach (Cell point in DependfromMeCells)
            {
                foreach (Cell susp in check_list)
                {
                    if (susp.Name == point.Name)
                        return false;
                }
                if (!point.CheckForLoop(check_list))
                    return false;
            }
            return true;
        }
        public void AddPointersAndReferences()
        {
            foreach (Cell point in New_IDependCells)
                point.DependfromMeCells.Add(this);

            IDependCells = New_IDependCells;
        }
        public void DelIDependCells()
        {
            if (IDependCells != null)
                foreach (Cell point in IDependCells)
                    point.DependfromMeCells.Remove(this);
            IDependCells = null;
        }
    }
}
