using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat ,characterClass, startingLevel);
        }


        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) { return startingLevel; }

            float currentXP = GetComponent<Experience>().GetExperience();
            int maxLevel = progression.GetLevels(characterClass);
            for (int level = 1; level <= maxLevel; level++)
            {
                float xPtoLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(xPtoLevelUp > currentXP)
                {
                    return level;
                }               
            }

            return maxLevel + 1;
        }
    }
}
