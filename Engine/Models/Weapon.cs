using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int Dice { get; set; }
        public int Roll { get; set; }
        public int Quality { get; set; }
        public Weapon(int itemTypeID, string name, int price, int dice, int roll,
                      string description, int quality)
            : base(itemTypeID, name, price, description)
        {
            Dice = dice;
            Roll = roll;
            Quality = quality;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeID, Name, Price, Dice, Roll, Description, Quality);
        }
    }
}
