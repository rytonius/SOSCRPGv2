using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static List<GameItem> _standardGameItems;

        static ItemFactory()
        {
            _standardGameItems = new List<GameItem>();

            _standardGameItems.Add(new Weapon(itemTypeID: 1001, name: "Pointy Stick", 
                                              dice: 1, roll: 4, description: "are you happy to see me?", quality: 0, price: 2));
            _standardGameItems.Add(new Weapon(itemTypeID: 1002, name: "Rusty Sword",
                                              dice: 1, roll: 8, description: "Nothing a lil wd40 wouldn't fix", quality: 0, price: 8));
            _standardGameItems.Add(new GameItem(itemTypeID: 9001, name: "Rat tail", price: 1, description: "tail of a rat"));
            _standardGameItems.Add(new GameItem(itemTypeID: 9002, name: "Rat fur", price: 2, description: "makes a good coat for the poor"));
            _standardGameItems.Add(new GameItem(itemTypeID: 9003, name: "Snake fang", price: 3, description: "Snake Chompers"));
            _standardGameItems.Add(new GameItem(itemTypeID: 9004, name: "Snakeskin", price: 5, description: "Snake... SNAAAKE"));
            _standardGameItems.Add(new GameItem(itemTypeID: 9005, name: "Spider fang", price: 4, description: "Spider Chompers"));
            _standardGameItems.Add(new GameItem(itemTypeID: 9006, name: "Spider silk", price: 10, description: "makes a good dress for the royalty"));
            
        }

        public static GameItem? CreateGameItem(int itemTypeID)
        {
            GameItem? standardItem = _standardGameItems.Find(item => item.ItemTypeID == itemTypeID);
            if (standardItem != null)
            {
                return standardItem.CLone();
            }
            return null;
        }
    }
}
