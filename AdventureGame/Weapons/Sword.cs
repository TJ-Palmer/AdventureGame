using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame.Weapons {
    class Sword : Weapon{
        public Sword() : base() {
            this.name = "Battle Sword";
            this.damage = 5;
            this.defence = 5;
            this.durability = this.maxDurability = 20;
        }

        public override string Info {
            get { return $"Weapon: {this.name}\nDamage: {this.damage}\nDurability: {this.durability}/{this.maxDurability}\nDefence: {this.defence}"; }
        }
    }
}
