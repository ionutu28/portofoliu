namespace Tetris_
{
    public class Grid_interfata_
    {
        private readonly int[,] grid;//matrice
        public int Rows { get; }
        public int Columns { get; }
        public int this[int r,int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public Grid_interfata_(int rows,int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];//initializare
        }
        public bool Inside(int r,int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }
        public bool IsEmpty(int r,int c)
        {
            return Inside(r, c) && grid[r, c] == 0;
        }
        public bool RowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                { return false; }
            }
            return true;
        }
        public bool RowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                { return false; }
            }     
            return true;
        }
        private void ClearRow(int r)
        {
            for(int c=0;c<Columns;c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MoveRowDown(int r,int nrRows)
        {
            for(int c=0;c<Columns;c++)
            {
                grid[r + nrRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }
        public int FullRowClear()
        {
            int del = 0;
            for(int r=Rows-1;r>=0;r--)
            {
                if(RowFull(r))
                    {
                    ClearRow(r);
                    del++;
                    }
                else
                    if(del>0)               
                    MoveRowDown(r, del);                               
            }
            return del;
        }
    }
}
