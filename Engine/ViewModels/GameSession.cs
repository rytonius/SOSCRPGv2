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
        private Player _currentPLayer;
        public Player CurrentPlayer { get => _currentPLayer; set => _currentPLayer = value; }
        private Monster _currentMonster;
        public bool HasMonster => CurrentMonster != null;
        private Trader _currentTrader;
        public bool HasTrader => CurrentTrader != null;
        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;
                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }
        public Weapon CurrentWeapon { get; set; }

        public Location CurrentLocation
        {
            get => _currentLocation;
            set 
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToUp));
                OnPropertyChanged(nameof(HasLocationToDown));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
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
                    Level = 0,
                    Strength = 1,
                    Dexterity = 0,
                    Endurance = 1,
                    GoldString = "1.00",
                    XPtillNextLvl = 0
                    

                };
                // initialize secondary stats, have to seperate so that you can use the starting stats

                if (CurrentPlayer.XPtillNextLvl <= 0) LevelUP(ref _currentPLayer);


                if (!CurrentPlayer.Weapons.Any())
                {
                    CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
                }
                // init world factory and create world
                CurrentWorld = WorldFactory.CreateWorld();
                CurrentLocation = CurrentWorld.LocationAt(xCoordinate: 0, yCoordinate: -1, zCoordinate: 0);
            }

        }
        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete = CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }
                        WorldMessage($"You completed the '{quest.Name}' quest");
                        // Give the player the quest rewards
                        WorldMessage($"You receive {quest.RewardExperiencePoints} experience Points");
                        CurrentPlayer.XPtillNextLvl -= quest.RewardExperiencePoints;
                        if (CurrentPlayer.XPtillNextLvl <= 0) LevelUP(ref _currentPLayer);
                        WorldMessage($"You receive {quest.RewardCurrency} Denarius");
                        // this is why strings suck for values
                        double _gold = CurrentPlayer.gold;
                        _gold += quest.RewardCurrency;
                        CurrentPlayer.GoldString = Convert.ToString(_gold);
                        foreach(ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            WorldMessage($"You receive a {rewardItem.Name}");
                        }
                        questToComplete.IsCompleted = true;

                    }
                }
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

                    WorldMessage($"You receive the '{quest.Name}' quest");
                    WorldMessage(quest.Description);

                    WorldMessage("Return with:");
                    foreach(ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        WorldMessage($"\t{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    WorldMessage($"And you will receive:\n\t{quest.RewardExperiencePoints} XP\n\t{quest.RewardCurrency} Denarius");
                    foreach(ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        WorldMessage($"\t{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    


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
            if (CurrentWeapon == null || CurrentMonster == null)
            {
                { BattleMessage("You must select a weapon, to attack."); }
            }
            else
            {
                // determine damage to monster 
                int damageToMonster = RandomNumberGen.DiceRollDamageCalculator(CurrentWeapon.Dice, CurrentWeapon.Roll, CurrentPlayer.Attack);
                int test = RandomNumberGen.NumberBetween(1, 100);
                BattleMessage($"\n(weapon.dice: {CurrentWeapon.Dice} * weapon.roll: {CurrentWeapon.Roll}) + AttackBonus {CurrentPlayer.Attack}\ttest(dev): {test}");
                int defenseOfMonster = RandomNumberGen.NumberBetween(0, CurrentMonster.Defense);
                
                BattleMessage($"You striked for + {damageToMonster}, {CurrentMonster.Name} was able to block {defenseOfMonster}");
                if (damageToMonster > defenseOfMonster)
                {
                    int TotalDamageDealt = damageToMonster - defenseOfMonster;
                    BattleMessage($"You dealt {TotalDamageDealt} to {CurrentMonster.Name}");
                    CurrentMonster.HitPoints -= damageToMonster;
                }

                else BattleMessage($"{CurrentMonster.Name} blocked all the damage");
 
                // need to put in accuracy and evasion here

            }
            // kill the monster
            if (CurrentMonster.HitPoints <= 0) 
            {
                BattleMessage($"You defeated the {CurrentMonster.Name}!!!\nYou received XP: {CurrentMonster.RewardExperiencePoints}\tDenarius: {CurrentMonster.RewardGold}\n----------------");
                CurrentPlayer.XPtillNextLvl -= CurrentMonster.RewardExperiencePoints;

                // this is why strings suck for values
                double _gold = CurrentPlayer.gold;
                _gold += RandomNumberGen.CurrencyBetween(CurrentMonster.RewardGold, CurrentMonster.RewardGold * 2);
                CurrentPlayer.GoldString = Convert.ToString(_gold);
                
                foreach(GameItem gameItem in CurrentMonster.Inventory)
                {
                    
                    CurrentPlayer.AddItemToInventory(gameItem);
                    WorldMessage($"You receive 1 : {gameItem.Name} from killing {CurrentMonster.Name}\n----------------");
                }

                if (CurrentPlayer.XPtillNextLvl <= 0) LevelUP(ref _currentPLayer);
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                //If monster is still alive, let the monster attack
                int damageToPlayer = RandomNumberGen.DiceRollDamageCalculator(dice: CurrentMonster.DiceDamage, roll: CurrentMonster.RollDamage, bonusDamage: CurrentMonster.BonusDamage);
                int defenseOfPlayer = RandomNumberGen.NumberBetween(0, CurrentPlayer.Defense);
                BattleMessage($"{CurrentMonster.Name} attacked you for +{damageToPlayer}, while you were able to block {defenseOfPlayer}");
                if (damageToPlayer > defenseOfPlayer)
                {
                    int TDD = damageToPlayer - defenseOfPlayer;
                    BattleMessage($"Your received {TDD} damage to your Hit Points");
                    CurrentPlayer.HitPoints -= TDD;
                }
                else BattleMessage($"You Succesfully blocked all the damage!"); 

                if (CurrentPlayer.HitPoints <= 0)
                {
                    WorldMessage($"The {CurrentMonster.Name} is now eating your corpse.  Sucks to suck lol, git gud\nSomeone found your body, and brought you back home");
                    CurrentLocation = CurrentWorld.LocationAt(0, -1, 0);
                    CurrentPlayer.HitPoints = CurrentPlayer.MaxHitPoints;
                }
            }
            
        }

        private void LevelUP(ref Player CuPl)
        {
            CuPl.Level += 1;
            CuPl.XPtillNextLvl += (int)Math.Round((CurrentPlayer.Level * 40f) * (1.1f), 0);

            CuPl.Strength += 1;
            CuPl.Dexterity+= 1;
            CuPl.Endurance+= 1;

            CurrentPlayer.MaxHitPoints = 10 + (CurrentPlayer.Endurance * 4);
            CurrentPlayer.HitPoints = CurrentPlayer.MaxHitPoints;
            CurrentPlayer.MaxStaminaPoints = 10 + (CurrentPlayer.Dexterity + CurrentPlayer.Strength);
            CurrentPlayer.StaminaPoints = CurrentPlayer.MaxStaminaPoints;
            CurrentPlayer.Attack = CurrentPlayer.Strength * 2 + CurrentPlayer.Dexterity;
            CurrentPlayer.Defense = CurrentPlayer.Dexterity + CurrentPlayer.Endurance * 2;
            CurrentPlayer.Evade = CurrentPlayer.Dexterity;
            CurrentPlayer.BonusAccuracy = CurrentPlayer.Dexterity;
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


        public void CurrencyChange(double Money)
        {
            double _gold = CurrentPlayer.gold;
            _gold += Money;
            CurrentPlayer.GoldString = Convert.ToString(_gold);
        }
    }
}
