using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTest{
    public class Inventory{
        public int money = 0;
        public Weapon weapon = null;
        public Defence legs = null;
        public Defence head = null;
        public Defence body = null;
        public Level level = null;
        public List<Defence> defences = new List<Defence>();
        public List<Weapon> weapons = new List<Weapon>();
        public void TakeOn(Defence def){
            switch (def.currentType)
            {
                case Defence.type.body:
                    if(body != null)
                        defences.Add(body);
                    body = def;
                    break;
                case Defence.type.head:
                    if(head != null)
                        defences.Add(head);
                    head = def;
                    break;
                case Defence.type.legs:
                    if(legs != null)
                        defences.Add(legs);
                    legs = def;
                    break;
                default:
                    break;
            }
            defences.Remove(def);
            UpdateDefenceStats();
        }
        public void TakeOff(Defence.type def)
        {
            switch (def)
            {
                case Defence.type.body:
                    defences.Add(body);
                    body = null;
                    break;
                case Defence.type.head:
                    defences.Add(head);
                    head = null;
                    break;
                case Defence.type.legs:
                    defences.Add(legs);
                    legs = null;
                    break;
                default:
                    break;
            }
            UpdateDefenceStats();
        }
        public void UpdateInventory(){
            UpdateWeaponStats();
            UpdateDefenceStats();
        }
        public void TakeOn(Weapon wep){
            if (weapon != null)
                weapons.Add(weapon);
            weapon = wep;
            weapons.Remove(wep);
            UpdateWeaponStats();
        }
        public void TakeOffWeapon(){
            weapons.Add(weapon);
            weapon = null;
            UpdateWeaponStats();
        }
        public void Take(Weapon wep){
            weapons.Add(wep);
        }
        public void Take(Defence def){
            defences.Add(def);
        }
        public void UpdateWeaponStats(){
            if(weapon !=null){
                level.currentMaxAttack = level.maxLevelAttack + weapon.damage;
                level.vampireChance = weapon.vampireChance;
                level.vampireDamageProcent = weapon.vampireDamageProcent;
                level.vampireDamageGetPerHitMax = weapon.vampireDamageGetPerHitMax;
            }
            else{
                level.currentMaxAttack = level.maxLevelAttack;
                level.vampireChance = 0;
                level.vampireDamageProcent = 0;
                level.vampireDamageGetPerHitMax = 0;
            }

        }
        public void UpdateDefenceStats(){
            
            level.currenMaxHp = level.maxLevelHp;
            level.currentMaxDefence = level.maxLevelDefence;
            float procentDefence = 0f;
            float dodgeChanse = 0f;
            if (legs != null){
                AddPropertiesDefence(legs);
                procentDefence += legs.defenceProcent;
                dodgeChanse += legs.dodgeChance;
            }
            if(head != null){
                AddPropertiesDefence(head);
                procentDefence += head.defenceProcent;
                dodgeChanse += head.dodgeChance;
            }
            if (body != null){
                AddPropertiesDefence(body);
                procentDefence += body.defenceProcent;
                dodgeChanse += body.dodgeChance;
            }
            level.currentMaxDefence += (int)(level.maxLevelDefence * procentDefence);
            level.dodgeChance += dodgeChanse;
        }
        private void AddPropertiesDefence(Defence def){
            level.currentMaxDefence += def.defence;
            level.currenMaxHp += def.addictiveHp;
        }
    }
}
