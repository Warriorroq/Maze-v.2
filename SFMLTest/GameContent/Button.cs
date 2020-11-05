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
    public class Button{
        public RectangleShape rectangle;
        public Text text;
        public Font font = DebugUtility.consoleFont;
        public Button(Vector2f size,Vector2f position){
            rectangle = new RectangleShape(size);
            rectangle.Position = position;
            rectangle.OutlineColor = Color.Black;
            text.FillColor = Color.Black;
            rectangle.OutlineThickness = 2f;
        }
        public Button(RectangleShape shape)
        {
            rectangle = shape;
            if(text != null)
                text.FillColor = Color.Black;
        }
        public bool IsButtonHold(GameLoop loop)
            => rectangle.GetGlobalBounds().Contains(Mouse.GetPosition(loop.window).X,Mouse.GetPosition(loop.window).Y) && Mouse.IsButtonPressed(Mouse.Button.Left);

        public bool IsButtonHold(GameLoop loop,Mouse.Button btn)
            => rectangle.GetGlobalBounds().Contains(Mouse.GetPosition(loop.window).X, Mouse.GetPosition(loop.window).Y) && Mouse.IsButtonPressed(btn);

        public bool IsHovered()
            => rectangle.GetGlobalBounds().Contains(Mouse.GetPosition().X, Mouse.GetPosition().Y);

        public Button(Vector2f size, Vector2f position,string text){
            rectangle = new RectangleShape(size);
            rectangle.Position = position;
            rectangle.OutlineColor = Color.Black;
            rectangle.OutlineThickness = 2f;
            this.text = new Text(text,font,12);
            this.text.Position = new Vector2f(rectangle.Position.X + 10,rectangle.Position.Y + rectangle.Size.Y/ 4f);
            this.text.FillColor = Color.Black;
            this.text.Font = font;
        }

        public void Draw(GameLoop gl){
            gl.window.Draw(rectangle);
            if(text != null)
                gl.window.Draw(text);
        }

    }
}
