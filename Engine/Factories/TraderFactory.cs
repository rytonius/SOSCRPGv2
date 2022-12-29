﻿using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();
        static TraderFactory()
        {
            Trader SusieQ = new Trader("Susie Q");
            SusieQ.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            Trader FarmerGrant = new Trader("Farmer Grant");
            FarmerGrant.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            Trader BobParkertheHerbalist = new Trader("Bob Parker");
            BobParkertheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            AddTraderToList(SusieQ);
            AddTraderToList(FarmerGrant);
            AddTraderToList(BobParkertheHerbalist);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.Find(t => t.Name == name);

        }

        private static void AddTraderToList(Trader trader)
        {
            if(_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'.");
            }
            _traders.Add(trader);
        }
    }
}
