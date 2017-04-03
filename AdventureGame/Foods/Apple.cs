using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Foods {
    class Apple : Food {
        public Apple() : base() {
            this.name = "Apple";
            this.healAmount = 2;
            this.quantity = 1;
        }

        public override string Info {
            get { return $"Food: {this.name}\nHeal Amount: {this.healAmount}\nQuantity: {this.quantity}"; }
        }
    }
}
