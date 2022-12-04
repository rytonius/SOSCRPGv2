using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace Engine.ViewModels
{
    public class GameSession : Notification
    {
        public event EventHandler<WorldMessageEventArgs> OnWorldMessageRaised;
        public event EventHandler<BattleMessageEventArgs> OnBattleMessageRaised;
        #region properties
        private Location _currentLocation;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        private Monster _currentMonster;
        public bool HasMonster => CurrentMonster != null;
        public Weapon CurrentWeapon { get; set; }
        public Location CurrentLocation
        {
            get => _currentLocation;
            set {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToUp));
                OnPropertyChanged(nameof(HasLocationToDown));

                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                }
        }
        #endregion
        public GameSession()
        {
            { // start new game, so make a player
                CurrentPlayer = new Player()
                {
                    Name = "John Snow",
                    CharacterClass = "Fighter",
                    Level = 1,
                    Strength = 2,
                    Dexterity = 1,
                    Endurance = 2,
                    GoldString = "10.00"

                };
                // initialize secondary stats, have to seperate so that you can use the starting stats
                {
                    CurrentPlayer.HitPoints += 10 + (CurrentPlayer.Endurance * 2);
                    CurrentPlayer.StaminaPoints += 10 + (CurrentPlayer.Dexterity + CurrentPlayer.Strength);
                    CurrentPlayer.Attack += CurrentPlayer.Strength * 2 + CurrentPlayer.Dexterity;
                    CurrentPlayer.Defense += CurrentPlayer.Dexterity + CurrentPlayer.Endurance * 2;
                    CurrentPlayer.Evade += CurrentPlayer.Dexterity;
                    CurrentPlayer.BonusAccuracy += CurrentPlayer.Dexterity;
                    CurrentPlayer.XPtillNextLvl = (int)Math.Round((CurrentPlayer.Level * 40f) * (1.1f), 0);
                }
                
                if (!CurrentPlayer.Weapons.Any())
                {
                    CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
                }
                // init world factory and create world
                CurrentWorld = WorldFactory.CreateWorld();
                CurrentLocation = CurrentWorld.LocationAt(xCoordinate: 0, yCoordinate: -1, zCoordinate: 0);

                

            }

        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                //We need to add “using System.Linq;”, 
                //so we can use LINQ to search through the CurrentPlayer’s Quests list – 
                //to ensure we don’t give the player a quest they already have (in the GivePlayerQuestsAtLocation function).
                if (!CurrentPlayer.Quests.Any(currentquest => currentquest.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    WorldMessage($"A wild \"{CurrentMonster.Name}\" has appeared!");
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                { BattleMessage("You must select a weapon, to attack."); }
            }
            else
            {
                // determine damage to monster 
                int damageToMonster = RandomNumberGen.DiceRollDamageCalculator(CurrentWeapon.Dice, CurrentWeapon.Roll, CurrentPlayer.Attack);
                int test = RandomNumberGen.NumberBetween(1, 100);
                BattleMessage($"weapon dice: {CurrentWeapon.Dice}\t weapon roll: {CurrentWeapon.Roll}\ttest: {test}");
                BattleMessage($"You did {damageToMonster} to {CurrentMonster.Name}");
                // need to put in accuracy and evasion here
                CurrentMonster.HitPoints -= damageToMonster;
            }
            
            
        }

        private void WorldMessage(string message)
        {
            OnWorldMessageRaised?.Invoke(this, new WorldMessageEventArgs(message));
        }

        public void BattleMessage(string message)
        {
            OnBattleMessageRaised?.Invoke(this, new BattleMessageEventArgs(message));
        }
        #region MoveMethodsandStuff
        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate +1, CurrentLocation.ZCoordinate) != null;
        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate) != null;
        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate) != null;
        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1, CurrentLocation.ZCoordinate) != null;
        public bool HasLocationToUp => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate + 1) != null;
        public bool HasLocationToDown => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate - 1) != null;
        public void MoveNorth()
        {
            if (HasLocationToNorth)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1, CurrentLocation.ZCoordinate);
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate);
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate);

        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1, CurrentLocation.ZCoordinate);
        }
        public void MoveUp()
        {
            if (HasLocationToUp)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate + 1);

        }
        public void MoveDown()
        {
            if (HasLocationToDown)
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate, CurrentLocation.ZCoordinate - 1);

        }
        #endregion
        #region font
        int _fontSizeLabel = 14;
        //int _leftSideGridSize = 225;
        
        public int FontSizeLabel
        {
            get => _fontSizeLabel;
            set { _fontSizeLabel = value; OnPropertyChanged(nameof(FontSizeLabel)); }
        }

        //public int LeftSideGridSize
        //{
        //    get => _leftSideGridSize;
        //    set { _leftSideGridSize = value; OnPropertyChanged(nameof(LeftSideGridSize)); }
        //}
        #endregion



    }
}
