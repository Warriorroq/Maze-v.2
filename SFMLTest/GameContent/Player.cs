using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTest
{
    public class Player{
        public int x;
        public int y;
        public int bombs = 3;
        public int currentHp = 10;
        public World world;
        public Level level;
        public Inventory inventory;
        public Enemy enemy;
        Random random = new Random();
        private GameLoop loop;
        public enum state{
            fight,
            walk,
            openChest,
            reload
        }
        public state currentState = state.walk;
        public Player(int y, int x, World world){
            inventory = new Inventory();
            this.y = y;
            this.x = x;
            this.world = world;
        }
        public Player(Player player,World world){

            this.world = world;
            x = player.x;
            y = player.y;

            if (inventory == null)
                inventory = new Inventory();
            else 
                inventory = player.inventory;

            if (level == null)
                return;
            else
                level = player.level;

        }
        public void Fight(Enemy enemy){
            currentState = state.fight;
            this.enemy = enemy;
            enemy.player = this;
        }
        public void AddStuff(DataBase DBase,GameLoop loop) {
            level = new Level(DBase, inventory);
            inventory.level = level;
            level.UpdatePlayerInfo(this);
            this.loop = loop;
        }
        public void Move(int x,int y){
            if (InIndex(x, y)){

                TakeBonus(x, y);

                if (world.maze[this.y + y, this.x + x] != '#'){
                    MoveVertical(x);
                    MoveHorisontal(y);
                }
            }
        }
        private void TakeBonus(int x, int y){
            char bonus = world.maze[this.y + y,this.x + x];
            if (bonus == 'B')
                bombs += 3;
            else if (bonus == 'N')
                Fight(world.FindEnemy(this.y + y, this.x + x));
            else if (bonus == 'M')
                inventory.money += random.Next(1, 10);
            else if (bonus == 'C')
                currentState = state.openChest;
            else if (bonus == 'E') 
                currentState = state.reload;
        }
        public void CreateBoom(){
            if (bombs > 0)
            {
                BombNearPlate(0, 1);
                BombNearPlate(1, 0);
                BombNearPlate(-1, 0);
                BombNearPlate(0, -1);
                bombs--;
            }
        }
        private void BombNearPlate(int x,int y){
            try
            {
                world.maze[this.y + y,this.x + x] = ' ';
            }
            catch { }
        }
        private bool InIndex(int x, int y)
            => this.y + y < world.y && this.x + x < world.x && this.x + x >= 0 && this.y + y >= 0;
        private void MoveVertical(int x){
            world.maze[this.y, this.x] = ' ';
            this.x += x;
            world.maze[this.y, this.x] = 'P';
        }
        private void MoveHorisontal(int y){
            world.maze[this.y, this.x] = ' ';
            this.y += y;
            world.maze[this.y, this.x] = 'P';
        }

        public void Takedamage(int damage)
        {
            if (damage - level.currentMaxDefence >= 0)
                damage -= level.currentMaxDefence;
            else
                damage = 0;
            currentHp -= damage;
        }

        public void Attack(){
            if(enemy != null)
                enemy.TakeDamage(random.Next(level.currentMaxAttack/3,level.currentMaxAttack));
            GetDefenceBack();
        }
        public void GetDefenceBack(){
            inventory.UpdateDefenceStats();
        }
        public void Defend(){
            int addictiveDefence = (int)(level.currentMaxDefence * (float)random.Next(10, 17) / 10f);
            if (level.currentMaxDefence + addictiveDefence <= level.currentMaxAttack)
                level.currentMaxDefence += addictiveDefence;
            else
                level.currentMaxDefence = level.currentMaxAttack;
        }
    }
}
