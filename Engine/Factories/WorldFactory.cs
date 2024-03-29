﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            // Start location and farm
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: -1, zCoordinate: 0, name: "Home",
                                description: "This is your home, you have 17 kids from a woman named MARTHA",
                                imageName: "Home.jpg");
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: -2, zCoordinate: 0, name: "Forest Near Home",
                                description: "Area Next to your home, good place to hunt and train",
                                imageName: "ForestAreaHome.jpg");
            // monster spawn for forest near home
            newWorld.LocationAt(0, -2, 0).AddMonster(1, 100);

            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 0, zCoordinate: 0, name: "Mountain Side",
                                description: "Center location of Area, leads to city north or farmland to west.",
                                imageName: "MountainSide.jpg");
            newWorld.AddLocation(xCoordinate: -1, yCoordinate: 0, zCoordinate: 0, name: "Farmer's Field",
                                description: "Head towards Farmer's House west, or Mountain Side East",
                                imageName: "FarmField.jpg");
            newWorld.AddLocation(xCoordinate: -2, yCoordinate: 0, zCoordinate: 0, name: "Farmer's House",
                                description: "Farmer Grant S. lives here, you don't like him, but he pays money if you head west and kill" +
                                "critters that keep eating his darn plants",
                                imageName: "FarmHouse.jpg");
            // farmer house quest kill snakes
            newWorld.LocationAt(-2, 0, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));
            // farmer is a trader
            newWorld.LocationAt(-2, 0, 0).TraderHere = TraderFactory.GetTraderByName("Farmer Grant");

            newWorld.AddLocation(xCoordinate: -3, yCoordinate: 0, zCoordinate: 0, name: "Farmer's Field 2",
                                description: "Monsters keep stealing Farmer Grant's Grains! Get um",
                                imageName: "FarmField2.jpg");
            // Snake attack
            newWorld.LocationAt(-3, 0, 0).AddMonster(2, 100);
            // City Section
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 1, zCoordinate: 0, name: "Entrance Gate",
                                description: "Enter the City from the south side",
                                imageName: "EntranceGate.jpg");
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 2, zCoordinate: 0, name: "City Square",
                                description: "City in the Center, To the east is a Trade Shop, West the Castle, North Exit Gate",
                                imageName: "TownSquare.jpg");
            newWorld.AddLocation(xCoordinate: 1, yCoordinate: 2, zCoordinate: 0, name: "Trading Shop Entrance",
                                description: "Trading Shop Entrance",
                                imageName: "TradingShopEntrance.jpg");
            newWorld.AddLocation(xCoordinate: 1, yCoordinate: 2, zCoordinate: 1, name: "Trading Shop",
                                description: "Trading Shop purchase goods from here",
                                imageName: "TradingShop.jpg");

            // Trader Shop
            newWorld.LocationAt(1, 2, 1).TraderHere = TraderFactory.GetTraderByName("Susie Q");


            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 3, zCoordinate: 0, name: "Exit Gate",
                                description: "North Side of the city, there is the herbalist garden outside the gates and Spider Forest" +
                                "North of there",
                                imageName: "ExitGate.jpg");
            // herbalist section
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 4, zCoordinate: 0, name: "Herbalist Garden",
                                description: "North of here is spider forest, west of here is the Herbalist Hut that sells goods",
                                imageName: "GardenHerbalist2.jpg");
            newWorld.AddLocation(xCoordinate: -1, yCoordinate: 4, zCoordinate: 0, name: "Herbalist Hut Entrance",
                                description: "Herbalist sells healing items, just have to go inside",
                                imageName: "HerbalistGarden.jpg");
            newWorld.AddLocation(xCoordinate: -1, yCoordinate: 4, zCoordinate: 1, name: "Herbalist Hut",
                                description: "Herbalist sells healing items",
                                imageName: "HerbalistHut.jpg");

            // Herbalist Trader
            newWorld.LocationAt(-1, 4, 1).TraderHere = TraderFactory.GetTraderByName("Bob Parker");

            // spider forest 
            newWorld.AddLocation(xCoordinate: 0, yCoordinate: 5, zCoordinate: 0, name: "Spider Forest Entrance",
                                description: "Entrance to spider forest, make sure you are ready before entering",
                                imageName: "SpiderForestEntrance.jpg");
            newWorld.LocationAt(0, 5, 0).AddMonster(3, 90);
            newWorld.LocationAt(0, 5, 0).AddMonster(4, 10);


            return newWorld;
        }
    }
}
