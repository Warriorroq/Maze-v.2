using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace SFMLTest{
    public class Defence{
        public enum type{
            head,
            body,
            legs,            
        }
        public type currentType;

        public Texture texture = null;
        //stats
        public int defence = 1;
        public float defenceProcent = 0f;
        public float  dodgeChance = 0.01f;
        public int addictiveHp = 1;
        //description
        public string description = " ";
        public string name = "defence";
    }
}
