using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        #region properties
        public Grid Grid { get; set; }
        public ViewPort ViewPort { get; set; }
        #endregion

        #region constructors
        public GameOfLife()
        {
            Grid = new Grid(Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            ViewPort = new ViewPort(Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);
        }
        #endregion

        #region methods
        public void Init()
        {
            Grid.Init();
            ViewPort.Init();
            MainLoop();
        }

        public void MainLoop()
        {
            while(true)
            {
                ViewPort.Render(Grid);
                Grid.Update();
                Grid.ForwardOneGeneration();
                //Thread.Sleep(50);
            }
            
        }
        #endregion
    }
}
