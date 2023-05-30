using System;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class StatDisplay : MonoBehaviour
    {
        float healthMax;
        float healthCurrent;

        PlayerHealth health;
        Experience experience;
        BaseStats playerStats;
        [SerializeField] Text healthDisplay;
        [SerializeField] Text experienceDisplay;
        [SerializeField] Text levelDisplay;
        [SerializeField] Slider healthSlider;
        [SerializeField] Slider experienceSlider;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            playerStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void OnEnable()
        {
            health.OnHealthChange += HealthChangeHandler;
            playerStats.OnExperienceGained += ExperienceGainedHandler;
            playerStats.OnLevelChange += LevelChangeHandler;
        }
        private void OnDisable()
        {
            health.OnHealthChange -= HealthChangeHandler;
            playerStats.OnExperienceGained -= ExperienceGainedHandler;
            playerStats.OnLevelChange -= LevelChangeHandler;
        }

        private void LevelChangeHandler(int newLevel)
        {
            levelDisplay.text = String.Format("Level : {0:0}", newLevel);
        }

        private void ExperienceGainedHandler(float currentXP, float xPtoLevelUp)
        {
            experienceDisplay.text = String.Format("Experience: {0:0}/{1:0}", currentXP, xPtoLevelUp);
            experienceSlider.value = currentXP / xPtoLevelUp;
        }

        public void HealthChangeHandler(float health, float maxHealth)
        {
            healthDisplay.text = String.Format("{0:0}/{1:0}", health, maxHealth);
            healthSlider.value = health / maxHealth;
        }
    }
}