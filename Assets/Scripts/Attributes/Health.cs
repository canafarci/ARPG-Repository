using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }
        protected float maxHealth;
        protected float healthPoints = -1f;
        bool isDead = false;

        protected BaseStats statSystem;

        private void Awake()
        {
            statSystem = GetComponent<BaseStats>();
        }

        private void Start()
        {
            InitialHealth();
        }

        protected virtual void InitialHealth()
        {
            if (healthPoints < 0)
            {
                healthPoints = statSystem.GetStat(Stat.Health);
            }
            maxHealth = statSystem.GetStat(Stat.Health);
        }
        public bool IsDead()
        {
            return isDead;
        }

        public virtual void TakeDamage(GameObject instigator, float damage)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                onDie.Invoke();
                AwardExperience(instigator);
                //Animation Event
                Die();
            }
            else
            {
                takeDamage.Invoke(damage);
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
            return GetHealthFraction() * 100;
        }

        public float GetHealthFraction()
        {
            return healthPoints / maxHealth;
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