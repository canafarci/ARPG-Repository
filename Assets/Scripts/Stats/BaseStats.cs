using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        int currentLevel = 0;

        public event Action<int> OnLevelChange;
        public event Action<float, float> OnExperienceGained;

        Experience experience;

        private void Awake()
        {
            experience = GetComponent<Experience>();
        }
        private void OnEnable()
        {
            if (experience != null)
            {
                experience.OnExperienceGain += UpdateLevel;
            }
        }
        private void Start()
        {
            currentLevel = CalculateLevel(experience == null ? 0 : experience.GetExperience());
            if (experience != null)
                OnLevelChange?.Invoke(currentLevel);
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.OnExperienceGain -= UpdateLevel;
            }
        }

        private void UpdateLevel(float experiencePoints)
        {
            int newLevel = CalculateLevel(experiencePoints);
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                OnLevelChange?.Invoke(currentLevel);
            }
        }

        private void LevelUpEffect()
        {
            GameObject particleFX = Instantiate(levelUpParticleEffect, transform) as GameObject;
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel(experience.GetExperience());
            }
            return currentLevel;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) { return 0; }

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private int CalculateLevel(float currentXP)
        {
            if (experience == null) { return startingLevel; }
            float xPtoLevelUp = 0;
            int maxLevel = progression.GetLevels(characterClass);
            for (int level = 1; level <= maxLevel; level++)
            {
                xPtoLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (xPtoLevelUp > currentXP)
                {
                    OnExperienceGained?.Invoke(currentXP, xPtoLevelUp);
                    return level;
                }
            }

            OnExperienceGained?.Invoke(xPtoLevelUp, xPtoLevelUp);
            return maxLevel;
        }
    }
}
