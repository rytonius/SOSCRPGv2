using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; set; }

        public int DiceDamage { get; set; }
        public int RollDamage { get; set; }
        public int BonusDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
        public double RewardGold { get; private set; }



        public Monster (string name, string imageName, int maxHitPoints, int diceDamage, int rollDamage, int bonusDamage, int defense, int rewardExperiencePoints, int rewardGold)
        {
            
            Name = name;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
            MaxHitPoints = maxHitPoints;
            HitPoints = maxHitPoints;
            DiceDamage = diceDamage;
            RollDamage = rollDamage;
            BonusDamage = bonusDamage;
            Defense = defense;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
        }
    }
}
