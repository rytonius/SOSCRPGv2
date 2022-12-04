﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : Notification
    {
        private int _hitPoints;
        public string Name { get; private set; }
        public string ImageName { get; set; }
        public int MaxHitPoints { get; private set; }
        public int HitPoints {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }

        public int DiceDamage { get; set; }
        public int RollDamage { get; set; }
        public int BonusDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
        public int RewardGold { get; private set; }

        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        public Monster (string name, string imageName, int maxHitPoints, int diceDamage, int rollDamage, int bonusDamage, int rewardExperiencePoints, int rewardGold)
        {
            
            Name = name;
            ImageName = string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            MaxHitPoints = maxHitPoints;
            HitPoints = maxHitPoints;
            DiceDamage = diceDamage;
            RollDamage = rollDamage;
            BonusDamage = bonusDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}
