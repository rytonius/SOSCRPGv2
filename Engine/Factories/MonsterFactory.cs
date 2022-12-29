using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Monster rat = new Monster(name: "Water Mokemon", imageName: "Enemy1.png", maxHitPoints: 15, diceDamage: 1, rollDamage: 4, bonusDamage: 0, defense: 0, rewardExperiencePoints: 8, rewardGold: 2);
                    AddLootItem(rat, 9001, 75);
                    AddLootItem(rat, 9002, 15);
                    return rat;
                case 2:
                    Monster snake = new Monster(name: "Solid Snake", imageName: "Snake.png", maxHitPoints: 24, diceDamage: 2, rollDamage: 3, bonusDamage: 0, defense: 1, rewardExperiencePoints: 12, rewardGold: 4);
                    AddLootItem(snake, 9003, 75);
                    AddLootItem(snake, 9004, 15);
                    return snake;
                case 3:
                    Monster CultistLeader = new Monster(name: "Cultist Leader", imageName: "Cultist_leader.png", maxHitPoints: 36, diceDamage: 3, rollDamage: 4, bonusDamage: 1, defense: 3, rewardExperiencePoints: 22, rewardGold: 10);
                    AddLootItem(CultistLeader, 9005, 55);
                    AddLootItem(CultistLeader, 9006, 10);
                    return CultistLeader;
                case 4:
                    Monster CultistAbomination = new Monster(name: "Cultist Abomination", imageName: "Cult_Abomination1.png", maxHitPoints: 50, diceDamage: 2, rollDamage: 8, bonusDamage: 4, defense: 5, rewardExperiencePoints: 65, rewardGold: 19);
                    AddLootItem(CultistAbomination, 9005, 70);
                    AddLootItem(CultistAbomination, 9006, 50);
                    return CultistAbomination;

                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }

        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGen.NumberBetween(1, 100) <= percentage)
            {
                Console.WriteLine("percentage: " + percentage);
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }
    }
}
