using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Weapons;
using AdventureGame.Foods;

namespace AdventureGame.Creatures {
    class Player : Creature{
        private IItem selectedItem;
        private int weight;

        public Player() : base() {
            this.health = this.maxHealth = 100;
            this.equippedWeapon = new Sword();
            this.equippedWeapon.Equipped = true;
            this.defence = this.equippedWeapon.Defence;
            this.equippedWeapon.MenuOptions.Remove(Enums.MenuActionType.Drop);
            this.inventory.AddItem(this.equippedWeapon);
            this.inventory.AddItem(new Apple());
            // hack fixed. Horrible idea
            Meat placeholderMeat = new Meat();
            placeholderMeat.Quantity = 0;
            this.inventory.AddItem(placeholderMeat);
        }

        public IItem SelectedItem {
            set { this.selectedItem = value; }
            get { return this.selectedItem; }
        }

        public void Eat(Food food) {
            food.Quantity--;
            this.health = this.health + food.HealAmount < this.maxHealth ? food.HealAmount + this.health : this.maxHealth;
        }
    }
}
