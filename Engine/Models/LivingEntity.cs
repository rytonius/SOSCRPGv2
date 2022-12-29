using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity : Notification
    {
        string _name;
        int _maxHP;
        int _hp;
        int _maxSP;
        int _sp;
        int _att;
        int _def;
        int _ev;
        int _ba;

        public string Name
        {
            get => _name; 
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }


        public int MaxHitPoints
        {
            get => _maxHP;
            set { _maxHP = value; OnPropertyChanged(nameof(MaxHitPoints)); }
        }
        public int HitPoints
        {
            get => _hp;
            set { _hp = value; OnPropertyChanged(nameof(HitPoints)); }
        }
        public int MaxStaminaPoints
        {
            get => _maxSP;
            set { _maxSP = value; OnPropertyChanged(nameof(MaxStaminaPoints)); }
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


        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();

        protected LivingEntity()
        {
            _name = "placeholder";
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }

    }
}
