using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame {
    abstract class Potion : IItem {
        protected string name;
        protected string info;
        protected List<MenuActionType> menuOptions;

        public string Name {
            set { this.name = value; }
            get { return this.name; }
        }
        public string Info {
            get { return this.info; }
        }
        public List<MenuActionType> MenuOptions {
            get { return this.menuOptions; }
        }
    }
}
