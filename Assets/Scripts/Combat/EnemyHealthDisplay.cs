using System;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        float maxHealth;
        float currentHealth;

        Fighter playerFighter;
        [SerializeField] Text healthDisplay;

        private void Awake() 
        {
            playerFighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update() 
        {
            Health health = playerFighter.GetTarget();
            if (health == null)
            {
                healthDisplay.text = "";
                return;
            }
            
            maxHealth = health.GetMaxHealth();
            currentHealth = health.GetCurrentHealth();

            healthDisplay.text = String.Format("Enemy : {0:0}/{1:0}", currentHealth, maxHealth);          
        }
    }
}