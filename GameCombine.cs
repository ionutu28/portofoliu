namespace Tetris_
{
    class GameCombine
    {
        private Blocks currentBlock;
        
        public Grid_interfata_ GameGrid { get; }
        public NextBlock BlockQueue { get; }//seria de blocuri ce va urma
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public Blocks HeldBlock { get; private set; }
        public bool CanHold { get; private set; }
        public Blocks CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();//apelam reset pt a fi plasat elementul corect
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                        currentBlock.Move(-1, 0);

                }
            }
        }

        public GameCombine()
        {
            GameGrid = new Grid_interfata_(22, 10);
            BlockQueue = new NextBlock();
            CurrentBlock = BlockQueue.Update();
            CanHold = true;
        }//structura matricei
        private bool BlockFits()//verifica daca pozitia e elementului poate fi realizata
        {
            foreach(Poz p in CurrentBlock.TilePoz())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                    return false;       
            }
            return true;
        }
        public void Hold()
        {
            if (!CanHold)
                return;
            if(HeldBlock==null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.Update();
            }
            else
            {
                Blocks aux = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = aux;
            }
            CanHold = false;
        }
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
                CurrentBlock.RotateCCW();
        }
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
                CurrentBlock.RotateCW();
        }//daca dupa rotire elem nu poare fi poz este resetat
        public void MoveLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
                CurrentBlock.Move(0, 1);
        }
        public void Moveright()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
                CurrentBlock.Move(0, -1);
        }
        private bool IsGameOver()
        {
            return !(GameGrid.RowEmpty(0) && GameGrid.RowEmpty(1));//daca liniile principale sunt goale stop
        }
        private void PlaceBlocks()
        {
            foreach(Poz p in CurrentBlock.TilePoz())//pune elementul in interfata
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            Score+=GameGrid.FullRowClear();
            if (IsGameOver())
                GameOver = true;
            else
            {
                CurrentBlock = BlockQueue.Update();
                CanHold = true;
            }

        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if(!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlocks();//verificam daca se poate duce in jos
            }
        }
        private int TileDrop(Poz p)
        {
            int dp = 0;
            while (GameGrid.IsEmpty(p.Row + dp + 1, p.Column))
                dp++;
            return dp;
        }
        public int Blockdistance() 
        {
            int drop = GameGrid.Rows;
            foreach(Poz p in currentBlock.TilePoz())
            {
                drop = System.Math.Min(drop, TileDrop(p));
            }
            return drop;
        }
        public void FizDrop()//realizare dropului
        {
            CurrentBlock.Move(Blockdistance(), 0);
            PlaceBlocks();
        }
    }
}
