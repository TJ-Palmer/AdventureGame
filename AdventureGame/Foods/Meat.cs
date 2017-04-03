using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Foods {
    class Meat : Food {
        public Meat() : base() {
            this.name = "Meat";
            this.healAmount = 5;
            this.quantity = 1;
        }

        public override string Info {
            get { return $"Food: {this.name}\nHeal Amount: {this.healAmount}\nQuantity: {this.quantity}"; }
        }
    }
}
