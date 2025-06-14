using System;

namespace Tetris_
{
    class NextBlock
    {
        private readonly Blocks[] blocks = new Blocks[]//retinem blocurile
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        private readonly Random random = new Random();
        public Blocks NxBlock { get; private set; }
        public NextBlock()
        {
            NxBlock = RandomBlock();
        }
        private Blocks RandomBlock()//returneaza un elem la intamplare
        {
            return blocks[random.Next(blocks.Length)];
        }
        public Blocks Update()
        {
            Blocks bl = NxBlock;
            do
            {
                NxBlock = RandomBlock();
            } while (bl.Id == NxBlock.Id);
            return bl;
        }

    }
}
