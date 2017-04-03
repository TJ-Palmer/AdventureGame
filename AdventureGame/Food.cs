using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame {
    abstract class Food : IItem {
        protected string name;
        protected string info;
        protected List<MenuActionType> menuOptions;
        protected int healAmount;
        protected int quantity;

        public Food() {
            this.menuOptions = new List<MenuActionType>();
            this.menuOptions.Add(MenuActionType.Eat);
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
        public int HealAmount {
            get { return this.healAmount; }
        }
        public int Quantity {
            set { this.quantity = value; }
            get { return this.quantity; }
        }
    }
}
