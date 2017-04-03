using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame {
    abstract class Weapon : IItem {
        protected int damage;
        protected int defence;
        protected int durability;
        protected int maxDurability;
        protected bool equipped;
        protected string name;
        protected string info;
        protected List<MenuActionType> menuOptions;

        public Weapon() {
            this.equipped = false;
            this.menuOptions = new List<MenuActionType>();
            this.menuOptions.Add(MenuActionType.Drop);
            this.menuOptions.Add(MenuActionType.Back);
        }

        public string Name {
            set { this.name = value; }
            get { return this.name; }
        }
        public virtual string Info {
            get;
        }
        public List<MenuActionType> MenuOptions {
            get { return this.menuOptions; }
        }
        public int Damage {
            set { this.damage = value; }
            get { return this.damage; }
        }
        public int Defence {
            set { this.defence = value; }
            get { return this.defence; }
        }
        public int Durability {
            set { this.durability = value <= this.maxDurability ? value : this.maxDurability; }
            get { return this.durability; }
        }
        public int MaxDurability {
            set { this.maxDurability = value; }
            get { return this.maxDurability; }
        }
        public bool Equipped {
            set {
                this.equipped = value;
                if (value) {
                    this.menuOptions.Remove(MenuActionType.Equip);
                    this.menuOptions.Insert(0, MenuActionType.Unequip);
                } else {
                    this.menuOptions.Remove(MenuActionType.Unequip);
                    this.menuOptions.Insert(0, MenuActionType.Equip);
                }
            }
            get { return this.equipped; }
        }
        
        public void RepairWeapon(int amount) {
            this.durability += amount;
        }
        
    }
}
