using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        private bool isDead = false;

        float healthPoints = -1f;
        float maxHealth;

        private void Start() 
        {   
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }    
            maxHealth = healthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if (healthPoints == 0)
            {
                AwardExperience(instigator);
                //Animation Event
                Die();
            }
            
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperiencePoints));
        }

        private void Die()
        {
            if (isDead == true || healthPoints > 0) {return;}
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public float GetHealthPercentage()
        {
            return healthPoints / maxHealth * 100;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }

    }
}