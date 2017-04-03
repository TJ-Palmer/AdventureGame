using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Weapons {
    class Axe : Weapon {
        public Axe() : base() {
            this.name = "Stick";
            this.damage = 8;
            this.defence = 6;
            this.durability = this.maxDurability = 15;
            this.menuOptions = base.menuOptions;
        }

        public override string Info {
            get { return $"Weapon: {this.name}\nDamage: {this.damage}\nDurability: {this.durability}/{this.maxDurability}\nDefence: {this.defence}"; }
        }
    }
}
