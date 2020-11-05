using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
namespace SFMLTest
{
    public abstract class GameLoop
    {
        public const int FPS = 80;
        public const float updateTime = 1f/FPS;

        public RenderWindow window{
            get;
            protected set;
        }

        public GameTime gameTime{
            get;
            protected set;
        }

        public Color windowClearColor{
            get;
            protected set;
        }

        protected GameLoop(uint widthOfTheWindow, uint heightOfTheWindow,string nameOfTheWindow, Color RefleshColor){
            this.windowClearColor = RefleshColor;
            this.window = new RenderWindow(new VideoMode(widthOfTheWindow, heightOfTheWindow),nameOfTheWindow);
            this.gameTime = new GameTime();
            window.Closed += WindowClosed;
        }
        public void Run(){

            LoadContent();
            Init();

            float totalTimeBeforeUpdate = 0f;
            float previosTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock();

            while (window.IsOpen){
                window.DispatchEvents();
                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previosTimeElapsed;
                previosTimeElapsed = totalTimeElapsed;
                totalTimeBeforeUpdate += deltaTime;

                if(totalTimeBeforeUpdate >= updateTime)
                {
                    gameTime.Update(totalTimeBeforeUpdate, totalTimeElapsed);
                    totalTimeBeforeUpdate = 0f;

                    Update(gameTime);

                    window.Clear(windowClearColor);

                    Draw(gameTime);
                    window.Display();
                }
            }
        }
        private void WindowClosed(object sender,EventArgs e)
            =>window.Close();

        public abstract void LoadContent();
        public abstract void Init();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
