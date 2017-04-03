using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Weapons {
    class Stick : Weapon {
        public Stick() : base() {
            this.name = "Stick";
            this.damage = 1;
            this.defence = 3;
            this.durability = this.maxDurability = 5;
            this.menuOptions = base.menuOptions;
        }

        public override string Info {
            get { return $"Weapon: {this.name}\nDamage: {this.damage}\nDurability: {this.durability}/{this.maxDurability}\nDefence: {this.defence}"; }
        }
    }
}
