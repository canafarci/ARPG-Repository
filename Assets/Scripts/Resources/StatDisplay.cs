using System;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class StatDisplay : MonoBehaviour
    {
        float healthMax;
        float healthCurrent;

        Health health;
        Experience experience;
        BaseStats playerStats;
        [SerializeField] Text healthDisplay;
        [SerializeField] Text experienceDisplay;
        [SerializeField] Text levelDisplay;

        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            playerStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update() 
        {
            healthDisplay.text =  String.Format("Health : {0:0}/{1:0}", health.GetCurrentHealth(), health.GetMaxHealth());
            experienceDisplay.text = String.Format("Experience : {0:0}", experience.GetExperience());
            levelDisplay.text = String.Format("Level : {0:0}", playerStats.GetLevel());
        }
    }
}