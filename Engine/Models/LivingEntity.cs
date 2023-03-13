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
        #region VarsProps
        string _name;
        int _maxHP;
        int _hp;
        int _maxSP;
        int _sp;
        int _att;
        int _def;
        int _ev; // evasion
        int _ba; // bonus accuracy

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
        #endregion

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();

        protected LivingEntity()
        {
            _name = "placeholder";
            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            if (item.IsUnique) GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            else
            {
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID))
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
            
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItemToRemove = GroupedInventory.FirstOrDefault(gi => gi.Item == item);

            if (groupedInventoryItemToRemove != null)
            {
                if (groupedInventoryItemToRemove.Quantity == 1)
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                else
                    groupedInventoryItemToRemove.Quantity--;
                
            }
            OnPropertyChanged(nameof(Weapons));
        }

    }
}
