using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }


        public GameItem(int itemTypeID, string name, int price, string description)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            Description = description;
        }

        public GameItem CLone()
        {
            return new GameItem(ItemTypeID, Name, Price, Description); 
        }
    }
}
