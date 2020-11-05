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
    public class DrawPlayerInfo{

        private Button head;
        private Button body;
        private Button legs;
        private Button sword;
        public Text InitsEnemy;
        public Text Inits;
        private Player player;
        private Font font;
        public Button btnAttack;
        public Button btnDefend;
        public Button btnItem;
        public List<InventoryButton> items = new List<InventoryButton>();
        public float start = 10;
        public float speed = 400;
        public float end = 200;
        public void Init(Player player){
            this.player = player;
            head = new Button(CreateShape(new Vector2f(520, 100)));
            body = new Button(CreateShape(new Vector2f(520, 180)));
            legs = new Button(CreateShape(new Vector2f(520, 260)));
            sword = new Button(CreateShape(new Vector2f(440, 180)));
            ////////////////////////////////////////////////
            btnAttack = new Button(new Vector2f(80,20),new Vector2f(100,200),"Attack");
            btnDefend = new Button(new Vector2f(80, 20), new Vector2f(350, 200), "Defend");
            btnItem = new Button(new Vector2f(80, 20), new Vector2f(600, 200), "Item");
            ///////////////////////////////////////////////
            font = new Font(DebugUtility.consoleFontPass);
            Inits = new Text(" ",font,12);
            InitsEnemy = new Text(" ", font,12);
            Inits.FillColor = Color.Black;
            InitsEnemy.FillColor = Color.Black;
        }
        private void UpdateText(){
            UpdatePlayerInfo();
            if (player.enemy != null)
                UpdateEnemyInfo();
        }
        private void UpdateEnemyInfo(){
            string message = "hp: " + player.enemy.level.maxLevelHp + "/" + player.enemy.level.currenMaxHp +
                "\n attack: " + player.enemy.level.maxLevelAttack +
                "\n defence: " + player.enemy.level.maxLevelDefence +
                "\n level: " + player.enemy.level.level;
            InitsEnemy.DisplayedString = message;
            InitsEnemy.Position = new Vector2f(600, 20);
        }
        private void UpdatePlayerInfo(){
            string message = "hp: " + player.level.currenMaxHp + "/" + player.currentHp +
                "\n attack: " + player.level.currentMaxAttack +
                "\n defence: " + player.level.currentMaxDefence +
                "\n level: " + player.level.level +
                "\n exp: " + player.level.getDataBase().levelsExp[player.level.level] + "/" + player.level.exp +
                "\n money: " + player.inventory.money +
                "\n bombs: " + player.bombs;
            Inits.DisplayedString = message;
            if (player.currentState == Player.state.walk)
                Inits.Position = new Vector2f(520, 340);
            else if (player.currentState == Player.state.fight)
                Inits.Position = new Vector2f(20, 340);            
        }
        public void UpdatePlayerInventory(){
            items.Clear();
            for (int i = 0; i < player.inventory.defences.Count; i++){
                InventoryButton btn = new InventoryButton(new Vector2f(120, 20), new Vector2f(600, (i * 30 + 20) - start), "");
                btn.obj = player.inventory.defences[i];
                btn.text.DisplayedString = player.inventory.defences[i].name;
                items.Add(btn);
            }

            for (int j = 0; j < player.inventory.weapons.Count; j++){
                InventoryButton btn = new InventoryButton(new Vector2f(120, 20), new Vector2f(600, ((j + player.inventory.defences.Count) * 30 + 20) - start), "");
                btn.objWeapon = player.inventory.weapons[j];
                btn.text.DisplayedString = player.inventory.weapons[j].name;
                items.Add(btn);
            }

            end = (items.Count) * 10 + 20;
        }
        private void UpdateIcons(){
            if (player.inventory.weapon != null)
                sword.rectangle.Texture = player.inventory.weapon.texture;
            else
                sword.rectangle.Texture = null;

            if (player.inventory.head != null)
                head.rectangle.Texture = player.inventory.head.texture;
            else
                head.rectangle.Texture = null;

            if (player.inventory.legs != null)
                legs.rectangle.Texture = player.inventory.legs.texture;
            else
                legs.rectangle.Texture = null;

            if (player.inventory.body != null)
                body.rectangle.Texture = player.inventory.body.texture;
            else
                body.rectangle.Texture = null;
        }
        public void UseInventory(GameLoop loop){
            foreach(InventoryButton btn in items){
                if(btn.IsButtonHold(loop)){
                    int i = items.IndexOf(btn);
                    if (btn.obj != null)
                        player.inventory.TakeOn(btn.obj);
                    else if(btn.objWeapon != null)
                        player.inventory.TakeOn(btn.objWeapon);
                    items.Remove(btn);
                    break;
                }
            }
            UpdatePlayerInventory();
            UsePlayerIcons(loop);
            UpdateIcons();
        }
        private void UsePlayerIcons(GameLoop loop){
            if (head.IsButtonHold(loop, Mouse.Button.Right) && player.inventory.head !=null)
                player.inventory.TakeOff(Defence.type.head);
            if (legs.IsButtonHold(loop, Mouse.Button.Right) && player.inventory.legs != null)
                player.inventory.TakeOff(Defence.type.legs);
            if (body.IsButtonHold(loop, Mouse.Button.Right) && player.inventory.body != null)
                player.inventory.TakeOff(Defence.type.body);
            if (sword.IsButtonHold(loop, Mouse.Button.Right) && player.inventory.weapon != null)
                player.inventory.TakeOffWeapon();
            UpdateIcons();
        }
        public void Prokrutka(float delta){
            UpdatePlayerInventory();
            if (Input.KeyIsDown(Keyboard.Key.Up))
                if(start  - delta * speed > 0)
                    start -= speed * delta;
            if(Input.KeyIsDown(Keyboard.Key.Down))
                if (start + delta * speed < end)
                    start += speed * delta;
        }
        private RectangleShape CreateShape(Vector2f pos){
            RectangleShape shape = new RectangleShape(new Vector2f(60, 60));
            shape.OutlineColor = Color.Black;
            shape.OutlineThickness = 2f;
            shape.Position = pos;
            return shape;
        }
        public void Update(){
            UpdateText();
            UpdatePlayerInventory();
        }
        public void Draw(GameLoop gameLoop){
            if (player.currentState == Player.state.walk){
                legs.Draw(gameLoop);
                body.Draw(gameLoop);
                sword.Draw(gameLoop);
                head.Draw(gameLoop);
            }
            else if(player.currentState == Player.state.fight){
                btnAttack.Draw(gameLoop);
                btnDefend.Draw(gameLoop);
                btnItem.Draw(gameLoop);
            }
        }
    }
}
