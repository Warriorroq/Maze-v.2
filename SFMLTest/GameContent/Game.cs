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
    class Game : GameLoop
    {
        public const int windowWidthDefault = 740;
        public const int windowHeightDefault = 480;
        public const string titleOfTheWindow = "Game";
        private Random random = new Random();
        private World world = Factory.CreateWorld(24, 20);
        private DataBase dataBase = new DataBase();
        private Player player;
        private DrawPlayerInfo info;
        private float step = 0.15f;
        private float deltaStep = 0;
        private float worldDelta =0;
        public Game() : base(windowWidthDefault, windowHeightDefault, titleOfTheWindow, Color.White){

        }
        public override void LoadContent()
        {
            DebugUtility.LoadContent();
            world.LoadContent();
        }
        public override void Init(){
            player = Factory.CreatePlayer(world);
            player.AddStuff(dataBase, this);
            info = new DrawPlayerInfo();
            info.Init(player);
            world.GenerateObject(player);                        
            for (int i =0;i<3;i++){
                Enemy en = Factory.CreateUsualEnemy(player.level.level,world);
                world.GenerateObject(en);
                en.enemy = world.textures[6];
            }
            for(int i =0;i< 12;i++)
                player.inventory.Take(Factory.CreateUsualDefence(player.level.level + 1));
            for (int i = 0; i < 12; i++)
                player.inventory.Take(Factory.CreateUsualWeapon(player.level.level + 1));
        }
        public override void Update(GameTime gameTime){
            info.Prokrutka(gameTime.DeltaTime);
            if (player.currentState == Player.state.walk)
                PlayerStateWalk();
            else if(player.currentState == Player.state.fight){
                worldDelta += gameTime.DeltaTime;
                if (worldDelta >= step)
                    PlayerStateFight();
            }
            else if(player.currentState == Player.state.openChest){
                if(random.NextDouble() < 0.5f)
                    player.inventory.Take(Factory.CreateUsualDefence(player.level.level));
                else
                    player.inventory.Take(Factory.CreateUsualWeapon(player.level.level));

                player.currentState = Player.state.walk;
            }
            else if(player.currentState == Player.state.reload){
                player = Factory.CreatePlayer(world, player);
                player.AddStuff(dataBase, this);
                player.currentState = Player.state.walk;
                world = Factory.CreateWorld(24, 20);
                world.Reload(player);
            }
        }
        public override void Draw(GameTime gameTime){

            info.Update();
            window.Draw(info.Inits);
            if (player.currentState == Player.state.fight)
                PlayerStateFightDraw();
            else if (player.currentState == Player.state.walk)
                PlayerStateWalkDraw();
            DrawFrameRate();

            info.Draw(this);
        }
        private void MovePlayer(){
            if (Input.KeyIsDown(Keyboard.Key.S))
                player.Move(0, 1);
            else if (Input.KeyIsDown(Keyboard.Key.W))
                player.Move(0, -1);
            else if (Input.KeyIsDown(Keyboard.Key.D))
                player.Move(1, 0);
            else if (Input.KeyIsDown(Keyboard.Key.A))
                player.Move(-1, 0);
            else if (Input.KeyIsDown(Keyboard.Key.B))
                player.CreateBoom();

            deltaStep = 0;
        }
        public void PlayerStateWalk()
        {
            worldDelta += gameTime.DeltaTime;            
            if (worldDelta >= step)
                MoveWorld();

            info.UseInventory(this);

            deltaStep += gameTime.DeltaTime;
            if (deltaStep >= step)
                MovePlayer();
        }
        public void PlayerStateFight(){
            CheckOnAlive();
            if (info.btnAttack.IsButtonHold(this))
                PlayerAttack();
            if (info.btnDefend.IsButtonHold(this))
                PlayerDefend();
        }
        public void CheckOnAlive(){
            if (player.currentHp <= 0)
                window.Close();
            else if (player.enemy.level.currenMaxHp <= 0)
                PlayerWins();
        }
        public void PlayerWins(){
            player.currentState = Player.state.openChest;
            player.level.GetExp(15);
            world.RemoveEnemy(player.enemy);
        }
        public void PlayerDefend(){
            player.Defend();
            player.enemy.Attack();
            worldDelta = 0;
        }
        public void PlayerAttack() {
            if(player.enemy != null)
                player.enemy.Attack();
            player.Attack();
            worldDelta = 0;
        }
        public void MoveWorld(){
            world.MoveEnemies(player);
            worldDelta = 0;
        }
        //draw
        public void PlayerStateWalkDraw(){
            world.Draw();
            for (int i = 0; i < world.y; i++)
                for (int j = 0; j < world.x; j++)
                    window.Draw(world.plates[i, j]);

            foreach (Button btn in info.items){
                window.Draw(btn.rectangle);
                window.Draw(btn.text);
            }
        }
        public void PlayerStateFightDraw(){
            window.Draw(info.InitsEnemy);
        }

        //tools
        private void DrawFrameRate()
            =>DebugUtility.Message(this, "FPS: " + (1f / gameTime.DeltaTime).ToString("0.0"), Color.Black);
    }
}
