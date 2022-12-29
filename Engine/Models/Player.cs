using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Engine.Models
{
    public class Player : LivingEntity, PlayerSpecific
    {

        int _level;
        int _xpTillNextLVL;
        int _str;
        int _dex;
        int _end;
        public double gold;

        public string? CharacterClass { get; set; }
        public int Level
        {
            get => _level;
            set { _level = value; OnPropertyChanged(nameof(Level)); }
        }
        public int XPtillNextLvl
        {
            get => _xpTillNextLVL;
            set { _xpTillNextLVL = value; OnPropertyChanged(nameof(XPtillNextLvl)); }
        }
        public int Strength
        {
            get => _str;
            set { _str = value; OnPropertyChanged(nameof(Strength)); }
        }
        public int Dexterity
        {
            get => _dex;
            set { _dex = value; OnPropertyChanged(nameof(Dexterity)); }
        }
        public int Endurance
        {
            get => _end;
            set { _end = value; OnPropertyChanged(nameof(Endurance)); }
        }

        public string GoldString
        {
            get { return string.Format("{0:C2}", gold); }
            set { gold = Convert.ToDouble(value); OnPropertyChanged(nameof(GoldString)); }
        }

       
        public ObservableCollection<QuestStatus> Quests { get; set; }



        public Player()
        {
            
            Quests = new ObservableCollection<QuestStatus>();
        }



        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }
    }

    // These interfaces are so not needed, I just had learned them so tried it out.  It's prob more important if you have more
    // than once class that would derive from this.
    interface BattleStats
    {
        int HitPoints { get; set; }
        int StaminaPoints { get; set; }
        int Attack { get; set; }
        int Defense { get; set; }
        int Evade { get; set; }
        int BonusAccuracy { get; set; }

    }

    interface PlayerSpecific
    {
        int Level { get; set; }
        int XPtillNextLvl { get; set; }
        int Strength { get; set; }
        int Dexterity { get; set; }
        int Endurance { get; set; }
        string GoldString { get; set; }
    }
}


    
