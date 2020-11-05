using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTest{
    public class DataBase {
        public List<Weapon> weapons = new List<Weapon>();
        public List<Defence> defence = new List<Defence>();
        public List<float> levelsExp = new List<float>();
        private Random random = new Random();

        public DataBase(){
            CreateLevels();
        }
        private void CreateLevels(){
            levelsExp.Add(10);
            levelsExp.Add(30);
            levelsExp.Add(50);
            levelsExp.Add(100);
            levelsExp.Add(100);
            for (int i = 0; i < 10; i++)
                levelsExp.Add(100);
            levelsExp.Add(int.MaxValue);
        }
        public Weapon weaponDrop(int MinRare, int MaxRare)
            => weapons[random.Next(MinRare,MaxRare)];

        public Defence defenceDrop(int MinRare, int MaxRare)
            => defence[random.Next(MinRare, MaxRare)];

    }
}
