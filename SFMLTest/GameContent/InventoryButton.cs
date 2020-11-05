using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
namespace SFMLTest
{
    public class InventoryButton : Button
    {
        public InventoryButton(Vector2f size, Vector2f position) : base(size, position){
            rectangle = new RectangleShape(size);
            rectangle.Position = position;
            rectangle.OutlineColor = Color.Black;
            text.FillColor = Color.Black;
            rectangle.OutlineThickness = 2f;
        }
        public InventoryButton(Vector2f size, Vector2f position, string text) : base(size,position,text){
            rectangle = new RectangleShape(size);
            rectangle.Position = position;
            rectangle.OutlineColor = Color.Black;
            rectangle.OutlineThickness = 2f;
            this.text = new Text(text, font, 12);
            this.text.Position = new Vector2f(rectangle.Position.X + 10, rectangle.Position.Y + rectangle.Size.Y / 4f);
            this.text.FillColor = Color.Black;
            this.text.Font = font;
        }
        public Defence obj;
        public Weapon objWeapon;
    }
}
