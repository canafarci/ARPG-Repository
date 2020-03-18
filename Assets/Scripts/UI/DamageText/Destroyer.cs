using UnityEngine;

namespace RPG.UI.DamageText
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] GameObject parentObject;
        
        public void DestroyText()
        {
            Destroy(parentObject);
        }
    }
}


