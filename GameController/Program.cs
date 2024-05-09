// See https://aka.ms/new-console-template for more information


using ConsoleView.BattleScreen;
using GameController;
using RpgBattleSystem.BattleSystem.BattleProceedings;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Equipment.Weapons;

Character hans = new("Hans");
hans.Equipment.Weapon1 = new Kurzschwert();
hans.Equipment.Weapon2 = new Jagdflinte();
hans.Equipment.Weapon3 = new Kurzspeer();
        
Character erika = new("Erika");
Character gegner = new("Gegner");

List<Character> heros = new List<Character>();
heros.Add(hans);
heros.Add(erika);

List<Character> enemies = new List<Character>();
enemies.Add(gegner);

var controller = new BattleController(heros,enemies);
controller.Start();