using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SFMLTest
{
    public static class DebugUtility
    {
        public const string consoleFontPass = "./fonts/20008.ttf";

        public static Font consoleFont;

        private static Color fontColor = Color.Black;

        public static void LoadContent(){
            consoleFont = new Font(consoleFontPass);
        }
        public static void Message(GameLoop gameLoop, object message){

            if (consoleFont == null)
                return;
            fontColor = Color.Black;
            Text text = CreateText(message, 0);

            gameLoop.window.Draw(text);
        }
        public static void Message(GameLoop gameLoop, object message,Color color)
        {

            if (consoleFont == null)
                return;

            Text text = CreateText(message, 0);

            fontColor = color;

            gameLoop.window.Draw(text);
        }
        public static void Message(GameLoop gameLoop, object message,float y)
        {
            if (consoleFont == null)
                return;

            Text text = CreateText(message, y);
            gameLoop.window.Draw(text);
        }
        private static Text CreateText(object message,float y){ 
            Text text = new Text((string)message, consoleFont, 12);
            text.Position = new Vector2f(2f, y);
            text.Color = fontColor;

            return text;
        }
    }
}
