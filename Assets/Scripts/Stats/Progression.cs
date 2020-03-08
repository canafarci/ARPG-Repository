using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        Dictionary<CharacterClass, Dictionary<int, Dictionary<Stat, float>>> lookupTable = null;

        public float GetStat(Stat stat, CharacterClass setCharacterClass, int setLevel)
        {
            BuildLookup();

            int levelCount = lookupTable[setCharacterClass].Count;
            if (levelCount < setLevel)
            {
                return 0;
            }
            
            return lookupTable[setCharacterClass][setLevel - 1][stat];
        }

        public int GetLevels(CharacterClass setCharacterClass)
        {
            BuildLookup();
            
            int levelCount = lookupTable[setCharacterClass].Count;
            return levelCount;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) { return; }

            lookupTable = new Dictionary<CharacterClass, Dictionary<int, Dictionary<Stat, float>>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {   var levelLookupTable = new Dictionary<int, Dictionary<Stat, float>>();

                foreach (ProgressionCharacterLevel statlevel in progressionClass.level)
                {
                    var statLookupTable = new Dictionary<Stat, float>();

                    foreach (Stat statType in Enum.GetValues(typeof(Stat)))
                    {
                        statLookupTable[statType] = statlevel.GetStatType(statType);
                    }

                    levelLookupTable[Array.IndexOf(progressionClass.level, statlevel)] = statLookupTable;
                }
                
                lookupTable[progressionClass.characterClass] = levelLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionCharacterLevel[] level;        
        }

        [System.Serializable]
        class ProgressionCharacterLevel
        {
            public float health;
            public float experiencePoints;
            public float experienceToLevelUp;

            public float GetStatType(Stat statType)
            {
                if (statType == Stat.Health)
                {
                    return health;
                }

                if (statType == Stat.ExperiencePoints)
                {
                    return experiencePoints;
                }

                if (statType == Stat.ExperienceToLevelUp)
                {
                    return experienceToLevelUp;
                }

                return 0;
            }
        }
    }
}

