using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        #region properties
        public short[] IsAliveGen { get; set; }
        public List<short[]> NeighbourCells { get; set; }
        #endregion
        
        #region constructors
        public Cell() 
        {
            IsAliveGen = new short[2];

            IsAliveGen[0] = 0;
            IsAliveGen[1] = 0;
            NeighbourCells = new List<short[]>();
        }
        #endregion

        #region methods
        public void Update()
        {
            //int numAliveNeighbours = NeighbourCells.FindAll(c => c.IsAliveGen[0] == 1).Count + this.IsAliveGen[0];
            int numAliveNeighbours = NeighbourCells.Sum(c => c[0]) + this.IsAliveGen[0];

            switch (numAliveNeighbours)
            {
                case 3:
                    IsAliveGen[1] = 1;
                    break;

                case 4:
                    IsAliveGen[1] = IsAliveGen[0];
                    break;

                default:
                    IsAliveGen[1] = 0;
                    break;
            }
        }
        #endregion
    }
}
