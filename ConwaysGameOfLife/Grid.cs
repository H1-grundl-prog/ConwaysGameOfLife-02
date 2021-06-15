using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife
{
    public class Grid
    {
        #region properties
        public short Width { get; }
        public short Height { get; }
        public int Generation { get; set; }
        public Cell[,] Cells { get; }
        #endregion

        #region constuctors
        public Grid() { }

        public Grid(short width, short height)
        {
            Width = width;
            Height = height;

            Generation = 1;

            Cells = new Cell[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y] = new Cell();
                }
            }
        }
        #endregion

        #region methods
        public void Init()
        {
            Random random = new Random();

            PopulateNeighbourCellsList();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y].IsAliveGen[0] = random.Next(101) < Constants.INIT_POPULATION_PERCENTAGE ? (short)1 : (short)0;
                }
            }
        }
        public void Update()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y].Update();
                }
            }
        }

        public void ForwardOneGeneration()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y].IsAliveGen[0] = Cells[x, y].IsAliveGen[1];
                }
            }

            Generation++;
        }

        public void PopulateNeighbourCellsList()
        {
            //
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    CheckBlock(x, y);
                }
            }
        }

        public void CheckBlock(int x, int y)
        {
            // Find neighbours in 3 x 3 block surrounding cell
            for (int iy = -1; iy <= 1; iy++)
            {
                for (int ix = -1; ix <= 1; ix++)
                {
                    //
                    if (iy == 0 && ix == 0) { continue; }

                    int nx = x + ix;
                    int ny = y + iy;

                    // Pac-Man style / toroidial world layout
                    if (nx > Width - 1) { nx = 0; };
                    if (nx < 0) { nx = Width - 1; };
                    if (ny > Height - 1) { ny = 0; };
                    if (ny < 0) { ny = Height - 1; };

                    Cells[x, y].NeighbourCells.Add(Cells[nx, ny].IsAliveGen);
                }
            }
        }
        #endregion
    }
}
