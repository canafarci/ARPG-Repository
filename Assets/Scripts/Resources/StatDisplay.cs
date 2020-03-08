using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class StatDisplay : MonoBehaviour
    {
        Health health;
        Experience experience;
        [SerializeField] Text healthDisplay;
        [SerializeField] Text experienceDisplay;

        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update() 
        {
            healthDisplay.text =  String.Format("Health : {0:0.00}% " , health.GetHealthPercentage());
            experienceDisplay.text = String.Format("Experience : {0:0}", experience.GetExperience());
        }
    }
}