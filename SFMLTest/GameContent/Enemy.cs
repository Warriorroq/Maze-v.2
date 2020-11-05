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
    public class Enemy{
        public enum enemyType {
            usual,
        }
        public enemyType currentType = enemyType.usual;
        public EnemyLevel level;
        public int y = 0;
        public int x = 0;
        private Random random = new Random();
        public Texture enemy;
        public Player player;
        public Enemy(){
            level = new EnemyLevel();
        }
        public void Move(int x, int y,World world,Player player)
        {
            if (InIndex(x, y,world))
            {
                if (world.maze[this.y + y, this.x + x] == 'P'){
                    player.Fight(this);
                    this.player = player;
                }
                else if (world.maze[this.y + y, this.x + x] != '#' && world.maze[this.y + y, this.x + x] != 'E')
                {
                    MoveVertical(x, world);
                    MoveHorisontal(y, world);
                }
            }
        }
        private bool InIndex(int x, int y,World world)
            => this.y + y < world.y && this.x + x < world.x && this.x + x >= 0 && this.y + y >= 0;
        private void MoveVertical(int x,World world){
            world.maze[this.y, this.x] = ' ';
            this.x += x;
            world.maze[this.y, this.x] = 'N';
        }
        private void MoveHorisontal(int y, World world)
        {
            world.maze[this.y, this.x] = ' ';
            this.y += y;
            world.maze[this.y, this.x] = 'N';
        }
        public void TakeDamage(int damage){
            damage -= level.currentMaxDefence;
            level.currenMaxHp -= damage;
        }
        public void Attack(){
            if(player != null)
                player.Takedamage(random.Next(level.maxLevelAttack/3, level.maxLevelAttack));
        }
    }
}
