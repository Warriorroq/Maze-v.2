using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFMLTest{
    public class World{
        public char[,] maze;
        public int y;
        public int x;
        public RectangleShape[,] plates;
        private Random random = new Random();
        public Texture[] textures = new Texture[100];
        public List<Enemy> enemies = new List<Enemy>();

        public World(int sizeY, int sizeX)
        {
            y = sizeY;
            x = sizeX;
            maze = new char[y, x];
            plates = new RectangleShape[y, x];
            GenerateObject('#');
            maze[0, 0] = ' ';
            maze[0, 1] = ' ';
            GenerateObject(' ', y * x,0.7f);
            GenerateObject('B', y * x/8, 0.1f);
            GenerateObject('M', y * x / 16, 0.1f);
            if (random.NextDouble() < 0.6)
                GenerateObject('C',1);
            GenerateObject('E', 1);
        }
        public void LoadContent(){
            textures[2] = new Texture("./Images/dungeon/tiles/floor/floor_1.png");
            textures[0] = new Texture("./Images/top.png");
            textures[1] = new Texture("./Images/dungeon/tiles/wall/wall_1.png");
            textures[3] = new Texture("./Images/dungeon/tiles/wall/door_closed.png");
            textures[4] = new Texture("./Images/dungeon/heroes/knight/knight.png");
            textures[5] = new Texture("./Images/dungeon/props_itens/bomb.png");
            textures[6] = new Texture("./Images/dungeon/enemies/goblin/goblin.png");
            textures[7] = new Texture("./Images/dungeon/props_itens/chest_closed_tile.png");
            textures[8] = new Texture("./Images/dungeon/props_itens/bag_coins_tile.png");
            textures[9] = new Texture("./Images/dungeon/props_itens/barrel_tile.png");
        }
        //C:\Users\Артур\source\repos\SFMLTest\SFMLTest\bin\Debug\Images
        public void GenerateObject(char obj, int counts, double chanse)
        {
            for (; counts > 0; counts--)
            {
                if (random.NextDouble() < chanse)
                    maze[random.Next(y), random.Next(x)] = obj;
            }
        }
        public void GenerateObject(char obj, int counts)
        {
            int y = random.Next(this.y);
            int x = random.Next(this.x);
            for (; counts > 0; counts--)
                maze[random.Next(y), random.Next(x)] = obj;
        }
        public void GenerateObject(char obj)
        {
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    maze[i, j] = obj;
        }
        public void GenerateObject(Player player)
            =>maze[player.y, player.x] = 'P';
        public void GenerateObject(Enemy en){
            enemies.Add(en);
            maze[en.y, en.x] = 'N';
            en.enemy = textures[6];
        }
        public void CreateEnemies(int level){
            for (int i = 0; i < 6; i++){
                Enemy en = Factory.CreateUsualEnemy(level,this);
                GenerateObject(en);
                en.enemy = textures[6];
            }
        }
        public void Reload(Player player) {
            GenerateObject(player);
            for (int i = 0; i < 5; i++){
                Enemy en = Factory.CreateUsualEnemy(player.level.level, this);
                GenerateObject(en);
                en.enemy = textures[6];
            }
        }
        //other
        public void RemoveEnemy(Enemy enemy){

            if(enemy.player != null)
                enemy.player.enemy = null;

            maze[enemy.y, enemy.x] = ' ';
            enemies.Remove(enemy);
        }
        public void MoveEnemies(Player player){
            foreach (Enemy en in enemies)
                if(random.NextDouble() < 0.5)
                    en.Move(0, random.Next(-1,2), this,player);
                else
                    en.Move(random.Next(-1, 2), 0, this,player);
        }
        public Enemy FindEnemy(int y,int x){
            foreach (Enemy en in enemies)
                if (en.y == y && en.x == x)
                    return en;
            return null;
        }
        //draw
        public void Draw(){
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    DrawRectangle(i, j);
        }
        private void DrawRectangle(int y,int x)
        {
            plates[y, x] = new RectangleShape(new Vector2f(20, 20));
            plates[y, x].Position = new Vector2f(20 * x, 20 * y);
            plates[y, x].FillColor = Color.White;
            if (maze[y, x] == '#')
            {
                if (y < this.y - 1)
                {
                    if (maze[y + 1, x] != '#')
                        plates[y, x].Texture = textures[1];
                    else
                        plates[y, x].Texture = textures[0];
                }
                else
                    plates[y, x].Texture = textures[0];
            }
            else if (maze[y, x] == 'E')
                plates[y, x].Texture = textures[3];
            else if (maze[y, x] == ' ')
                plates[y, x].Texture = textures[2];
            else if(maze[y, x] == 'C')
                plates[y, x].Texture = textures[7];
            else if (maze[y, x] == 'M')
                plates[y, x].Texture = textures[8];
            else if (maze[y, x] == 'P') plates[y, x].Texture = textures[4];
            else if (maze[y, x] == 'B') plates[y, x].Texture = textures[5];
            else if (maze[y, x] == 'N') plates[y, x].Texture = textures[6];
        }

    }
}
