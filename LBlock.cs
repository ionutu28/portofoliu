
namespace Tetris_
{
    class LBlock:Blocks
    {
        private readonly Poz[][] tiles = new Poz[][]
        {
            new Poz[]{new(0,2),new(1,0),new(1,1),new(1,2)},
            new Poz[]{new(0,1),new(1,1),new(2,1),new(2,2)},
            new Poz[]{new(1,0),new(1,1),new(1,2),new(2,0)},
            new Poz[]{new(0,0),new(0,1),new(1,1),new(2,1)}
        };
        public override int Id => 3;
        protected override Poz StartOffset => new Poz(0,3);
        protected override Poz[][] Tiles => tiles;
    }
}
