using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace SFMLTest
{
   public static class Input{

        public static bool KeyIsDown(Keyboard.Key key)
            => Keyboard.IsKeyPressed(key);
    }
}
