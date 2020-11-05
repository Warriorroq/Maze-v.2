using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTest
{
    public class EnemyLevel : Level{
        private Random random = new Random();        
        public EnemyLevel() : base(null,null){
            
        }
        public void RenewLastStats(){
            for (int i = level; i > 0; i--)
                AddStats();
            UpdateStats();
        }
        private void UpdateStats(){
            currenMaxHp = maxLevelHp;
            currentMaxAttack = maxLevelAttack;
            currentMaxDefence = maxLevelDefence;
        }
        private void AddStats()
        {
            maxLevelAttack += random.Next(0, 10);
            maxLevelDefence += random.Next(0, 10);
            maxLevelHp += random.Next(0, 15 + level / 10);
        }
    }
}
