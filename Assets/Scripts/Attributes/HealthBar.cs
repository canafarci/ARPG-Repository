using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Canvas healthBarCanvas = null;
        [SerializeField] RectTransform barTransform = null;
        Health parentHealth;

        private void Awake() 
        {
            parentHealth = transform.GetComponentInParent<Health>();
        }

        void Update()
        {
            healthBarCanvas.enabled = HealthBarActive();
            if (!HealthBarActive()) { return; }
            
            float healthFraction = parentHealth.GetHealthFraction();
            barTransform.localScale = new Vector3(healthFraction, 1.0f, 1.0f);            
        }

        private bool HealthBarActive()
        {
            float healthFraction = parentHealth.GetHealthFraction();

            if (Mathf.Approximately(healthFraction, 0) || Mathf.Approximately(healthFraction, 1))
            {
                return false;
            } 

            return true;            
        }
    }

}