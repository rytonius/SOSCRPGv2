using Engine.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Location : Notification
    {
        string _name;
        string _description;
        string _imageName;
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int ZCoordinate { get; set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name));  } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public string ImageName { get => _imageName; set { _imageName = value; OnPropertyChanged(nameof(ImageName)); } }
        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();

        public List<MonsterEncounter> MonsterEncounterHere { get; set;} = new List<MonsterEncounter>();

        public Trader TraderHere { get; set; }
        public void AddMonster (int monsterID, int chanceOfEncountering)
        {
            if(MonsterEncounterHere.Exists(mon => mon.MonsterID == monsterID)) 
            {
                //This monster has already been added to this location.
                // So, overwrite the ChanceofEncountering with the new number.
                MonsterEncounterHere.First(mon => mon.MonsterID == monsterID).ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                // this monster is not already at this location, so add it.
                MonsterEncounterHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster GetMonster()
        {
            if (!MonsterEncounterHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = MonsterEncounterHere.Sum(Mon => Mon.ChanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100)
            int randomNumber = RandomNumberGen.NumberBetween(1, totalChances);

            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach(MonsterEncounter monsterEncounter in MonsterEncounterHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if(randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);

                }
            }

            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonsterEncounterHere.Last().MonsterID);
        }
    }
}
