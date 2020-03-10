using UnityEngine;

namespace RPG.Core
{
    public class DestroyEffect : MonoBehaviour
    {
        public float timeToDestroy = 1f;

        private void Start() 
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}