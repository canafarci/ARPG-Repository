using System;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
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
            
            healthDisplay.text = String.Format("Enemy : {0:0.00}% ", health.GetHealthPercentage());          
        }
    }
}