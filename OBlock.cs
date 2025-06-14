namespace Tetris_
{
    class OBlock : Blocks
    {
        private readonly Poz[][] tiles = new Poz[][]
        {
         new Poz[]{new (0,0),new(0,1),new(1,0),new(1,1)}
        };
        public override int Id=>4;
        protected override Poz StartOffset => new Poz(0, 4);
        protected override Poz[][] Tiles => tiles;
    }
}
