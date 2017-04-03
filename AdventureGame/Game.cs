using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureGame.Creatures;
using AdventureGame.Enums;
using AdventureGame.Weapons;

namespace AdventureGame {
    class Game {
        private bool running;
        private float difficultyMultiplier;
        private int attackTurn;
        private Maze maze;
        private Player player;
        private List<IItem> loot;
        private List<Enemy> enemies;
        private Enemy currentlyAttacking;
        private int numberOfEnemies;
        private bool startFight = false;
        private bool fighting = true;

        public void Run() {
            this.running = true;
            this.maze = new Maze();
            this.attackTurn = 0;
            this.difficultyMultiplier = 1f;
            this.player = new Player();
            this.loot = new List<IItem>();

            GameIntro();
            GameLoop();
        }
        private void GameLoop() {
            while (this.running) {
                MenuController();
                if (this.startFight) {
                    InitiateFight();
                }
            }
        }
        private void InitiateFight() {
            DisplayPlayerStatBar();
            Console.WriteLine(maze.GetLocationInfo());
            Console.ReadLine();
            this.maze.MenuOptions.Remove(MenuActionType.Explore);
            this.enemies = maze.GetEnemies();
            this.numberOfEnemies = this.enemies.Count;
            this.currentlyAttacking = this.enemies[0];
            this.startFight = false;
            this.fighting = true;
        }
        private void MenuController() {
            MenuActionType type;
            do {
                DisplayPlayerStatBar();
                DisplayMenu(maze.MenuOptions);
                Console.WriteLine(maze.GetLocationInfo());
                type = ProcessInputForMenuActionType(Console.ReadLine(), maze.MenuOptions);
            } while (type == MenuActionType.Failed);

            PreformMenuAction(type);
        }
        private MenuActionType ProcessInputForMenuActionType(string input, List<MenuActionType> menu) {
            MenuActionType type;
            int typeAsInt;

            if (int.TryParse(input, out typeAsInt)) {
                if (typeAsInt >= 0 && typeAsInt < menu.Count) {
                    input = menu[typeAsInt].ToString();
                } else {
                    input = "";
                }
            }
            if (!Enum.TryParse(input, out type)) {
                return MenuActionType.Failed;
            }

            return type;
        }
        private void RunAttackTurn() {
            if (this.player.Health <= 0) {
                EndingDied();
            }
            
            bool enemiesAlive = this.numberOfEnemies > 0 ? true : false;
            
            if (enemiesAlive) {
                //Enemy strongest = maze.GetStrongestEnemy();

                // Check if player has a weapon equipped
                if (this.player.EquippedWeapon.Name == "None") {
                    Console.WriteLine("You don't have a weapon equipped!");
                } else {
                    // Player attack
                    if (this.numberOfEnemies > 1) {
                        Console.WriteLine($"You attack one of the enemies");
                    } else {
                        Console.WriteLine($"You attacked an enemy");
                    }
                    Console.WriteLine($"You delt {player.EquippedWeapon.Damage} damage to the enemy");
                    this.currentlyAttacking.TakeDamage(this.player.DealDamage());
                    if (this.currentlyAttacking.Health <= 0) {
                        foreach (IItem item in this.currentlyAttacking.Inventory.GetItems()) {
                            if (this.player.Inventory.Has(item) && item is Food) {
                                ((Food)this.player.Inventory.GetItem(item)).Quantity++;
                            } else {
                                this.loot.Add(item);
                            }
                        }

                        this.enemies.Remove(this.currentlyAttacking);

                        if (this.enemies.Count > 0) {
                            this.currentlyAttacking = this.enemies[0];
                        } else {
                            enemiesAlive = false;
                        }
                    }
                }
                // Check if enemies are alive.
                if (enemiesAlive) {
                    // Enemies attack
                    foreach (Enemy e in this.enemies) {
                        Console.WriteLine($"An enemy attacks you dealing {e.DealDamage()} damage");
                        this.player.TakeDamage(e.DealDamage());
                    }
                    if (this.enemies.Count > 1) {
                        Console.WriteLine($"There are {this.enemies.Count} enemies left");
                    } else {
                        Console.WriteLine("The enemy is still alive");
                    }
                    if (this.player.Health <= 0) {
                        EndingDied();
                    }
                } else {
                    // All enemies are dead
                    this.maze.Fight = this.fighting = false;
                    if (this.numberOfEnemies > 1) {
                        Console.WriteLine("All enemies are dead");
                    } else {
                        Console.WriteLine("The enemy is dead.");
                    }
                    RewardPlayer();
                    this.attackTurn = 0;
                    this.maze.MenuOptions.Remove(MenuActionType.Attack);
                    this.maze.MenuOptions.Remove(MenuActionType.Run);
                    this.maze.MenuOptions.Add(MenuActionType.Explore);
                }
            }
            Console.ReadLine();
        }
        private void RewardPlayer() {
            this.player.Gold += this.numberOfEnemies * 2;

            if (this.numberOfEnemies > 1) {
                Console.WriteLine($"Each enemy dropped 2 gold. You now have {this.player.Gold} gold.");
            } else {
                Console.WriteLine($"The enemy dropped 2 gold. You now have {this.player.Gold} gold.");
            }

            string loot = ""; ;

            for (int i = 0; i < this.loot.Count; i++) {
                if (i == 0) {
                    loot += this.loot[i].Name;
                } else if (i == this.loot.Count - 1) {
                    loot += $", and {this.loot[i].Name}";
                } else {
                    loot += $", {this.loot[i].Name}";
                }

                if (this.loot[i] is Weapon) {
                    Weapon item = (Weapon)this.loot[i];
                    item.MenuOptions.Remove(MenuActionType.Unequip);
                    item.MenuOptions.Insert(0, MenuActionType.Equip);
                }

                this.player.Inventory.AddItem(this.loot[i]);
            }
            this.loot.Clear();
            loot += " was added to your inventory.";

            Console.WriteLine(loot);
        }
        private void PreformMenuAction(MenuActionType type) {
            if (fighting && type.Equals(MenuActionType.Attack)) {
                attackTurn++;
                RunAttackTurn();
            }

            switch (type) {
                case MenuActionType.Explore:
                    this.startFight = this.maze.Explore();
                    break;
                case MenuActionType.MainMenu:
                    DisplayMainMenu();
                    break;
                case MenuActionType.OpenInventory:
                    DisplayInventory();
                    break;
                case MenuActionType.Unequip:
                    ((Weapon)this.player.SelectedItem).MenuOptions.Remove(MenuActionType.Unequip);
                    ((Weapon)this.player.SelectedItem).MenuOptions.Insert(0, MenuActionType.Equip);
                    this.player.EquippedWeapon = new Dud();
                    break;
                case MenuActionType.Back:
                    return;
                case MenuActionType.Eat:
                    this.player.Eat((Food)this.player.SelectedItem);
                    if (((Food)this.player.SelectedItem).Quantity == 0) {
                        this.player.Inventory.RemoveItem(player.SelectedItem);
                    }
                    break;
                case MenuActionType.Drop:
                    this.player.Inventory.RemoveItem(this.player.SelectedItem);
                    break;
                case MenuActionType.Equip:
                    this.player.EquippedWeapon.MenuOptions.Remove(MenuActionType.Unequip);
                    this.player.EquippedWeapon.MenuOptions.Insert(0, MenuActionType.Equip);
                    ((Weapon)this.player.SelectedItem).MenuOptions.Insert(0, MenuActionType.Unequip);
                    ((Weapon)this.player.SelectedItem).MenuOptions.Remove(MenuActionType.Equip);
                    this.player.EquippedWeapon = (Weapon)this.player.SelectedItem;
                    break;
                default:
                    break;
            }
        }
        private void DisplayInventory() {
            DisplayPlayerStatBar();
            int count = player.Inventory.Count;

            Console.WriteLine("Inventory: (Select an item for item's menu)");
            for (int i = 0; i < count; i++) {
                Console.WriteLine($"{i}. {player.Inventory.GetItems()[i].Name}");
            }
            Console.WriteLine($"{count}. Back");

            int num;
            if (int.TryParse(Console.ReadLine(), out num)) {
                if (num < count && num > -1) {
                    player.SelectedItem = player.Inventory.GetItem(num);
                    DisplayItemMenu(player.Inventory.GetItem(num));
                } else if (num == count) {
                    return;
                }
            }
        }
        private void DisplayItemMenu(IItem item) {
            string menu = "";
            for (int i = 0; i < item.MenuOptions.Count; i++) {
                menu += $"{i}. {item.MenuOptions[i]}";
                if (i == item.MenuOptions.Count - 1) {
                    continue;
                } else {
                    menu += " | ";
                }
            }

            MenuActionType type;
            do {
                DisplayPlayerStatBar();
                Console.WriteLine(item.Info);
                Console.WriteLine(menu);
                type = ProcessInputForMenuActionType(Console.ReadLine(), item.MenuOptions);
            } while (type == MenuActionType.Failed);
            
            if (type == MenuActionType.Back) {
                DisplayInventory();
            } else {
                PreformMenuAction(type);
            }
        }
        private void DisplayMainMenu() {
            this.running = false;
            Console.Clear();
            Console.WriteLine("Would you like to quit the game? (Yes/No)");
            if (Console.ReadLine().ToLower().Equals("yes")) {
                return;
            } else {
                this.running = true;
                return;
            }
        }
        private void DisplayPlayerStatBar() {
            Console.Clear();
            Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}, Weapon: {player.EquippedWeapon.Name}, Durability: {player.EquippedWeapon.Durability}/{player.EquippedWeapon.MaxDurability}");
        }
        private void DisplayMenu(List<MenuActionType> options) {
            for (int i = 0; i < options.Count; i++) {
                Console.WriteLine($"{i}. {options[i]}");
            }
        }
        private void GameIntro() {
            Console.Clear();
            Console.WriteLine("Welcome to Arion. A text based adventure game. To get start hit enter!");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("You're eyes slowly open to a blue sky above. You feel the breeze over your body and see the tress sway. Birds sing into the beautiful sky above. Your body still feels sleepy.");
            Console.WriteLine("Would you like to get up, or go back to sleep? (Sleep/Getup)");
            if (Console.ReadLine().ToLower().Equals("getup")) {
                return;
            } else {
                EndingStart();
            }
        }
        private void EndingStart() {
            Console.Clear();
            Console.WriteLine("You go back to sleep...");
            Console.WriteLine("Hit enter to continue.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("*stab*");
            Console.WriteLine("Hit enter to continue.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("You died in your sleep from some object stabbing you.");
            Console.WriteLine("Hit enter to continue.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Would you like to start over? (Yes/No)");
            if (Console.ReadLine().ToLower().Equals("yes")) {
                Run();
            }
            this.running = false;
        }
        private void EndingDied() {
            Console.Clear();
            Console.WriteLine("You have died in some way or form. Whatever caused you to die is beyond my knowledge but you probably know. I you lived your life to the fullest.");
            Console.ReadLine();
            this.running = false;
            this.fighting = false;
            this.maze.Fight = false;
        }
    }
}
