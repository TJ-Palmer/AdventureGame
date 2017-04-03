using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Enums;
using AdventureGame.Creatures;

namespace AdventureGame {
    class Maze {
        private List<MenuActionType> menuOptions;
        private LocationType location;
        private List<Enemy> enemies;
        private int maxNumberOfEnemies = 5;
        private int numberOfEnemies;
        private bool fight = false;
        private string locationInfo;
        private Random random;

        public LocationType Location {
            get { return this.location; }
        }
        public List<MenuActionType> MenuOptions {
            get { return this.menuOptions; }
        }

        public Maze() {
            this.location = LocationType.ForestPath;
            this.menuOptions = new List<MenuActionType>();
            this.enemies = new List<Enemy>();
            this.random = new Random();
            UpdateMenuOptions();
        }
        
        public bool Fight {
            set { this.fight = value; }
            get { return this.fight; }
        }

        public bool Explore() {
            this.fight = false;
            this.enemies.Clear();
            ChangeLocation(GetRandomLocation());
            return this.fight;
        }
        private LocationType GetRandomLocation() {
            int num = Enum.GetNames(typeof(LocationType)).Length;
            Random randomNumber = new Random();
            LocationType type = (LocationType)randomNumber.Next(num);

            switch (type) {
                case LocationType.Cave:
                    SpawnEnemiesForRoom();
                    break;
                case LocationType.ForestEntrance:
                    SpawnShop();
                    break;
            }

            return type;
        }
        private void SpawnEnemiesForRoom() {
            this.fight = true;
            this.numberOfEnemies = this.random.Next(this.maxNumberOfEnemies);
            this.numberOfEnemies++;
            for (int i = 0; i < this.numberOfEnemies; i++) {
                this.enemies.Add(new Enemy(this.random));
            }
        }
        private void SpawnShop() {

        }
        private void ChangeLocation(LocationType locationType) {
            this.location = locationType;
            UpdateMenuOptions();
        }
        private void UpdateMenuOptions() {
            this.menuOptions.Clear();
            this.menuOptions.Add(MenuActionType.OpenInventory);
            this.menuOptions.Add(MenuActionType.MainMenu);

            switch (this.location) {
                case LocationType.Cave:
                    this.menuOptions.Add(MenuActionType.Explore);
                    this.menuOptions.Add(MenuActionType.Attack);
                    this.menuOptions.Add(MenuActionType.Run);
                    break;
                case LocationType.CavePath:
                    this.menuOptions.Add(MenuActionType.Explore);
                    break;
                case LocationType.ForestEntrance:
                    this.menuOptions.Add(MenuActionType.Explore);
                    break;
                case LocationType.ForestPath:
                    this.menuOptions.Add(MenuActionType.Explore);
                    break;
                case LocationType.OpenForest:
                    this.menuOptions.Add(MenuActionType.Explore);
                    break;
                default:
                    break;
            }
        }
        public string GetLocationInfo() {
            this.locationInfo = $"You see that you are in a {this.location.ToString()}.";
            if (fight) {
                if (this.enemies.Count > 1) {
                    this.locationInfo += $"\nYou are surrounded by {this.enemies.Count} enemies";
                } else {
                    this.locationInfo += "\nYou are surrounded by an enemy";
                }
            }
            return this.locationInfo;
        }
        public List<Enemy> GetEnemies () {
            return this.enemies;
        }
        public Enemy GetStrongestEnemy() {
            Enemy strongest = this.enemies[0];
            foreach (Enemy e in this.enemies) {
                strongest = e.Health > strongest.Health ? e : strongest;
            }

            return strongest;
        }
    }
}
