using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory() 
        {
            //Declare the items need to complete the quest, and it's reward items
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            // create teh quest
            _quests.Add(new Quest(id: 1, name: "Clear the Farmers Field 2", description: "Defeat snakes for Farmer Grant, West of his House", 
                itemsToComplete: itemsToComplete, rewardExperiencePoints: 25, rewardCurrency: 10, rewardItems: rewardItems));
        }

        internal static Quest? GetQuestByID(int id)
        {
            return _quests.Find(quest => quest.ID == id);
        }
    }
}
