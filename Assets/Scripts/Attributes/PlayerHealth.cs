using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Attributes;
using RPG.Stats;


namespace RPG.Attributes
{
    public class PlayerHealth : Health
    {
        public event Action<float, float> OnHealthChange;
        private void OnEnable()
        {
            statSystem.OnLevelChange += UpdateHealth;
        }
        private void OnDisable()
        {
            statSystem.OnLevelChange -= UpdateHealth;
        }
        protected override void InitialHealth()
        {
            base.InitialHealth();
            OnHealthChange?.Invoke(healthPoints, maxHealth);
        }
        public void Heal(float healingAmount)
        {
            healthPoints = Mathf.Min(healthPoints += healingAmount, maxHealth);
            OnHealthChange?.Invoke(healthPoints, maxHealth);
        }
        void UpdateHealth(int newLevel)
        {
            float currentHealthFraction = GetHealthFraction();
            maxHealth = statSystem.GetStat(Stat.Health);
            healthPoints = maxHealth * currentHealthFraction;
            OnHealthChange?.Invoke(healthPoints, maxHealth);
        }

        public override void TakeDamage(GameObject instigator, float damage)
        {
            base.TakeDamage(instigator, damage);
            OnHealthChange?.Invoke(healthPoints, maxHealth);
        }
    }
}
