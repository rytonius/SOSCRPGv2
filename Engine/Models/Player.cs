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
    public class Player : Stats, PlayerSpecific
    {

        int _level;
        int _xpTillNextLVL;
        int _str;
        int _dex;
        int _end;
        decimal _gold;

        public string? Name { get; set; }
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
            get { return String.Format("{0:C2}", _gold); }
            set { _gold = Convert.ToDecimal(value); }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }
    }

    public abstract class Stats : Notification, BattleStats
    {
        int _hp;
        int _sp;
        int _att;
        int _def;
        int _ev;
        int _ba;

        public int HitPoints
        {
            get => _hp;
            set { _hp = value; OnPropertyChanged(nameof(HitPoints)); }
        }
        public int StaminaPoints
        {
            get => _sp;
            set { _sp = value; OnPropertyChanged(nameof(StaminaPoints)); }
        }
        public int Attack
        {
            get => _att;
            set { _att = value; OnPropertyChanged(nameof(Attack)); }
        }
        public int Defense
        {
            get => _def;
            set { _def = value; OnPropertyChanged(nameof(Defense)); }
        }
        public int Evade
        {
            get => _ev;
            set { _ev = value; OnPropertyChanged(nameof(Evade)); }
        }
        public int BonusAccuracy
        {
            get => _ba;
            set { _ba = value; OnPropertyChanged(nameof(BonusAccuracy)); }
        }

    }

    interface BattleStats
    {
        int HitPoints { get; set; }
        int StaminaPoints { get; set; }
        int Attack { get; set; }
        int Defense { get; set; }
        int Evade { get; set; }
        int BonusAccuracy { get; set; }

    }
    // These interfaces are so not needed, I just had learned them so tried it out.  It's prob more important if you have more
    // than once class that would derive from this.
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


    
