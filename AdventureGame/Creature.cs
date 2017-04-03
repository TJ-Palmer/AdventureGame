using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame {
    class Creature : EventArgs{
        protected int health;
        protected int maxHealth;
        protected int level;
        protected int defence;
        protected int gold;
        protected Weapon equippedWeapon;
        protected Inventory inventory;
        protected List<string> weaponTypes;
        protected List<string> foodTypes;

        public Creature() {
            this.inventory = new Inventory();
            this.weaponTypes = new List<string>() {"Sword", "Axe", "Stick"};
            this.foodTypes = new List<string>() {"Apple", "Meat"};
        }

        public int MaxHealth {
            set { this.maxHealth = value; this.health = value; }
            get { return this.maxHealth; }
        }
        public int Health {
            set {
                this.health = value <= this.maxHealth ? value : this.maxHealth;
            }
            get { return this.health; }
        }
        public int Level {
            set { this.level = value; }
            get { return this.level; }
        }
        public int Defence {
            set { this.defence = value; }
            get { return this.defence; }
        }
        public int Gold {
            set { this.gold = value; }
            get { return this.gold; }
        }
        public Weapon EquippedWeapon {
            set { this.equippedWeapon = value; }
            get { return this.equippedWeapon; }
        }
        public Inventory Inventory {
            set { this.inventory = value; }
            get { return this.inventory; }
        }

        public void TakeDamage(int amount) {
            this.health -= amount;
        }
        public int DealDamage() {
            return equippedWeapon.Damage;
        }
    }
}
