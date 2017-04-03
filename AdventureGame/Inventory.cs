using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;

namespace AdventureGame {
    class Inventory {
        private List<IItem> items;
        private int count;
        public int Count {
            get { return this.count; }
        }

        public Inventory() {
            this.items = new List<IItem>();
            this.count = 0;
        }
        
        public void AddItem(IItem item) {
            this.count++;
            this.items.Add(item);
        }
        public List<IItem> GetItems() {
            return this.items;
        }
        public IItem GetItem(int index) {
            return this.items[index];
        }
        public IItem GetItem(IItem item) {
            foreach (IItem i in this.items) {
                if (i.Name.Equals(item.Name)) {
                    return i;
                }
            }
            throw (new NullReferenceException("Item not found"));
        }
        public bool RemoveItem(IItem item) {
            this.count--;
            return this.items.Remove(item);
        }
        public bool Has(IItem item) {
            foreach (IItem i in this.items) {
                if (i.Name.Equals(item.Name)) {
                    return true;
                }
            }
            return false;
        }
    }
}
