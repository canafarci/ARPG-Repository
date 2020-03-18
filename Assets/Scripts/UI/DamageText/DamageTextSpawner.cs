using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;
        
        public void Spawn(float damageAmount)
        {
            DamageText damagePrefab = Instantiate<DamageText>(damageTextPrefab, transform);
            damagePrefab.SetValue(damageAmount);
        }
    }
}