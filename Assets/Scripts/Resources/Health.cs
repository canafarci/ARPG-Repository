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

        BaseStats statSystem;

        private void Awake()
        {
            statSystem = GetComponent<BaseStats>();
        }

        private void OnEnable()
        {
            statSystem.OnLevelChange += UpdateHealth;
        }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = statSystem.GetStat(Stat.Health);
            }

            maxHealth = healthPoints;
        }

        private void OnDisable()
        {
            statSystem.OnLevelChange -= UpdateHealth;
        }

        public void UpdateHealth(int newLevel)
        {
            float currentPercentage = GetHealthPercentage() / 100;
            maxHealth = statSystem.GetStat(Stat.Health);
            healthPoints = maxHealth * currentPercentage;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);

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
            experience.GainExperience(statSystem.GetStat(Stat.ExperiencePoints));
        }

        public float GetHealthPercentage()
        {
            return healthPoints / maxHealth * 100;
        }

        public float GetMaxHealth()
        {
            return statSystem.GetStat(Stat.Health);
        }

        public float GetCurrentHealth()
        {
            return healthPoints;
        }

        private void Die()
        {
            if (isDead == true || healthPoints > 0) { return; }
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
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