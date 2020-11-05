using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTest{
    public class Level{
        public int currenMaxHp = 10;
        public int currentMaxAttack = 4;
        public int currentMaxDefence = 1;

        public int maxLevelHp = 10;
        public int maxLevelAttack = 3;
        public int maxLevelDefence = 1;
        
        public int exp = 0;
        public int level = 0;

        private Random random = new Random();
        private DataBase datBase;
        private Inventory inventory;
        private Player player;
        public Level(DataBase basE,Inventory inv){
            datBase = basE;
            inventory = inv;
            
        }
        public void UpdatePlayerInfo(Player player){
            player.currentHp = currenMaxHp;
            this.player = player;
        }
        public void GetExp(int exp){
            this.exp += exp;
            if (this.exp >= datBase.levelsExp[level])
                LevelUp();
        }
        private void LevelUp(){
            level++;
            int hp = currenMaxHp;
            AddStats();
            exp = 0;
            inventory.UpdateInventory();
            player.currentHp += (currenMaxHp - hp) / 2;
        }
        private void AddStats(){
            maxLevelAttack += random.Next(0, 10);
            maxLevelDefence += random.Next(0, 10);
            maxLevelHp += random.Next(0, 15 + level/10);
        }
        public DataBase getDataBase() 
            => datBase;
        //defence properties 
        public float dodgeChance = 0.00f;

        //attack properties 
        public float vampireChance = 0f;
        public float vampireDamageProcent = 0f;
        public int vampireDamageGetPerHitMax = 0;
    }
}
