using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SFMLTest
{
    class Program
    {
        private static Game game;
        static void Main(string[] args)
        {
            game = new Game();
            game.Run();
        }
    }
     
}
