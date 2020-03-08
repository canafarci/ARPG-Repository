using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetStat(Stat stat, CharacterClass setCharacterClass, int setLevel)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass == setCharacterClass)
                {
                    ProgressionCharacterLevel currentLevel = progressionClass.level[setLevel - 1];
                    return currentLevel.GetStatType(stat);
                }
                else
                {
                    continue;
                }
            }
            return 0;
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

                return 0;
            }
        }
    }
}

