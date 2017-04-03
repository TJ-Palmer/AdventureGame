using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Weapons;
using AdventureGame.Foods;

namespace AdventureGame.Creatures {
    class Enemy : Creature {
        protected Random random;

        public Enemy(Random random) : base() {
            this.health = this.maxHealth = 10;
            this.random = random;

            switch (this.weaponTypes[this.random.Next(this.weaponTypes.Count)]) {
                case "Sword":
                    this.equippedWeapon = new Sword();
                    break;
                case "Axe":
                    this.equippedWeapon = new Axe();
                    break;
                case "Stick":
                    this.equippedWeapon = new Stick();
                    break;
                default:
                    this.equippedWeapon = new Stick();
                    break;
            }

            this.equippedWeapon.Equipped = true;
            this.defence = this.equippedWeapon.Defence;
            this.inventory.AddItem(this.equippedWeapon);
            this.GenerateLoot();
        }

        private void GenerateLoot() {
            int numberOfItems = random.Next(4);
            for (int i = 0; i <= numberOfItems; i++) {
                if (i % 2 == 0) {
                    this.inventory.AddItem(new Meat());
                } else {
                    this.inventory.AddItem(new Apple());
                }
            }
        }
    }
}
