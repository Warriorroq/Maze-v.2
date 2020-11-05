using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SFMLTest
{
    public static class Factory{

        private static Random random = new Random();

        public static World CreateWorld(int y,int x) 
            => new World(y,x);

        public static Player CreatePlayer(World world)
            => new Player(random.Next(world.y), random.Next(world.x),world);
        public static Player CreatePlayer(World world, Player player)
            => new Player(player,world);
        private static List<string> weaponTextures = new List<string>{
            "./Images/dungeon/things/swords/goblin_knife.png",
            "./Images/dungeon/things/swords/weapon_sword_1.png",
        };
        private static List<string> bodyTextures = new List<string>{
            "./Images/dungeon/things/armor/armor1.png",
            "./Images/dungeon/things/armor/armor2.png",
        };
        private static List<string> legsTextures = new List<string>{
            "./Images/dungeon/things/legs/legs1.jpeg"
        };
        private static List<string> headTextures = new List<string>{
            "./Images/dungeon/things/head/head1.png",
        };
        public static Enemy CreateUsualEnemy(int playerLevel,World world){

            Enemy enemy = new Enemy();
            enemy.x = random.Next(world.x);
            enemy.y = random.Next(world.y);
            if (playerLevel < 3)
                enemy.level.level = playerLevel - random.Next(-playerLevel,playerLevel);
            else
                enemy.level.level = playerLevel - random.Next(-4, 4);
            enemy.level.RenewLastStats();
            enemy.currentType = Enemy.enemyType.usual;
            return enemy;
        }

        public static Weapon CreateUsualWeapon(int playerLevel){
            Weapon weapon = new Weapon();
            weapon.texture = new Texture(weaponTextures[random.Next(weaponTextures.Count)]);
            weapon.damage = weapon.damage * playerLevel + 2;
            weapon.name = weapon.name + " " + random.Next(999);
            return weapon;
        }
        public static Defence CreateUsualDefence(int playerLevel)
        {
            Defence defence = new Defence();
            defence.defence = random.Next(defence.defence * playerLevel / 2 + 2, defence.defence * playerLevel + 2);
            float i = (float)random.NextDouble();
            if (i < 0.33f){
                defence.currentType = Defence.type.legs;
                defence.name = defence.name + " legs " + random.Next(999);
                //defence.texture = new Texture(legsTextures[random.Next(legsTextures.Count)]);
            }
            else if (i > 0.33f && i < 0.66f)
            {
                defence.currentType = Defence.type.body;
                defence.name = defence.name + " body " + random.Next(999);
                defence.texture = new Texture(bodyTextures[random.Next(bodyTextures.Count)]);
            }
            else if (i > 0.66f)
            {
                defence.currentType = Defence.type.head;
                defence.name = defence.name + " head " + random.Next(999);
                defence.texture = new Texture(headTextures[random.Next(headTextures.Count)]);
            }
            return defence;
        }
    }
    //C:\Users\Артур\source\repos\SFMLTest\SFMLTest\bin\Debug\Images
}
