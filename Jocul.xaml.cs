using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tetris_
{

    public partial class Jocul : Window
    {
        private readonly ImageSource[] tileImage = new ImageSource[]
    {
        new BitmapImage(new Uri("Assets/TileEmpty.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TileCyan.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TileBlue.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TileOrange.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tileyellow.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TileGreen.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TilePurple.png",UriKind.Relative)),
        new BitmapImage(new Uri("Assets/TileRed.png",UriKind.Relative))
    };
        private readonly ImageSource[] blockImage = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png",UriKind.Relative)),
        };
        private readonly Image[,] imageControls;//fiecarei celule ii corespunde un patrat
        private GameCombine gameState = new GameCombine();
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrese = 25;
        public Jocul()
        {
            InitializeComponent();
            imageControls = SetupGame(gameState.GameGrid);
        }
        private Image[,] SetupGame(Grid_interfata_ grid )
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;
            for(int r=0;r<grid.Rows;r++)
            {
                for(int c=0;c<grid.Columns;c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imageControl, (r - 2) * cellSize+10);
                    Canvas.SetLeft(imageControl, c* cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }//obtinem o matrice in care fiecare celula are atribuita o culoare
        private void DrawGrid(Grid_interfata_ grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImage[id];
                }
            }
        }
        private void DrawBlock(Blocks block)
        { 
            foreach(Poz p in block.TilePoz())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImage[block.Id];
            }
        }
        private void DrawHold(Blocks HeldBlock)
        {
            if (HeldBlock == null)
                Holdimage.Source = blockImage[0];
            else
                Holdimage.Source = blockImage[HeldBlock.Id];
        }
        private void NextBlockShow(NextBlock blockQueue)
        {
            Blocks next = blockQueue.NxBlock;
            NextImage.Source = blockImage[next.Id];
        }
        private void DrawAll(GameCombine gameState)
        {
            DrawGrid(gameState.GameGrid);
            GhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            NextBlockShow(gameState.BlockQueue);
            DrawHold(gameState.HeldBlock);
            ScoreText.Text = $"Score:{ gameState.Score}";
        }
        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }
        private async Task GameLoop()
        {
            DrawAll(gameState);
            while(!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score - delayDecrese));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                DrawAll(gameState);
            }
            GameOver.Visibility = Visibility.Visible;
            Scorfinal.Text = $"Score:{gameState.Score}";
        }
        private void Window_KeyDown(object sender,KeyEventArgs e)
        {
            if (gameState.GameOver)
                return;
            switch(e.Key)
            {
                case Key.Left:
                    gameState.MoveLeft();
                    break;
                case Key.Right:
                    gameState.Moveright();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                case Key.X:
                    gameState.RotateBlockCW();
                    break;
                case Key.LeftShift:
                    gameState.Hold();
                    break;
                case Key.Space:
                    gameState.FizDrop();
                    break;
                default:
                    return;
            }
            DrawAll(gameState);
        }
        private void GhostBlock(Blocks block)
        {
            int dropDist = gameState.Blockdistance();
            foreach(Poz p in block.TilePoz())
            {
                imageControls[p.Row + dropDist, p.Column].Opacity = 0.20;
                imageControls[p.Row + dropDist, p.Column].Source = tileImage[block.Id]; 
            }
        }
        private async void Restart_Click(object sender,RoutedEventArgs e)
        {
            gameState = new GameCombine();
            GameOver.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

        }
    }
}
