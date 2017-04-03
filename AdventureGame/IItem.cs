using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame {
    interface IItem {
        string Name {
            set;
            get;
        }
        string Info {
            get;
        }
        List<MenuActionType> MenuOptions {
            get;
        }
    }
}
