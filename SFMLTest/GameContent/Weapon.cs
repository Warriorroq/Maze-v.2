using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace SFMLTest{
    public class Weapon{
        //damage properties 
        public int damage = 2;
        public Texture texture = null;

        //vampire settings 
        public float vampireChance = 0f;
        public float vampireDamageProcent = 0f;
        public int vampireDamageGetPerHitMax = 0;
        
        //description
        public string description = " ";
        public string name = "weapon";
    }
}
