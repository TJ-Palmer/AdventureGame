using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Weapons {
    class Dud : Weapon {
        public Dud() : base() {
            this.name = "None";
            this.damage = 0;
            this.defence = 0;
            this.durability = this.maxDurability = 0;
            this.menuOptions = base.menuOptions;
        }

        public override string Info {
            get { return $"Weapon: {this.name}\nDamage: {this.damage}\nDurability: {this.durability}/{this.maxDurability}\nDefence: {this.defence}"; }
        }
    }
}
