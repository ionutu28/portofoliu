using System.Collections.Generic;

namespace Tetris_
{
    public abstract class Blocks
    {
        protected abstract Poz[][] Tiles { get; }//poz rotatie
        protected abstract Poz StartOffset { get; }//unde apare
        public abstract int Id { get; }//id block
        private int rotationState;
        private Poz offset;
        public Blocks()
        {
            offset = new Poz(StartOffset.Row, StartOffset.Column);//poz de aoparitie
        }
        public IEnumerable<Poz> TilePoz()
        {
            foreach(Poz p in Tiles[rotationState])//metoda de interactionare cu matricea(poz,rotatie,etc)
            {
                yield return new Poz(p.Row + offset.Row, p.Column + offset.Column);
            }
        }
       public void RotateCW()
        {
            rotationState=(rotationState + 1) % Tiles.Length;
        }
        public void RotateCCW()
        {
            if (rotationState == 0)
                rotationState = Tiles.Length - 1;
            else
                rotationState--;
        }
        public void Move(int rows,int columns)
        {
            offset.Row+=rows;
            offset.Column += columns;
        }
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
