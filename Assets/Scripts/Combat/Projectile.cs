using System.Collections;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {        
        [SerializeField] float speed = 1f;
        [SerializeField] float maxLifeTime = 6f;
        [SerializeField] bool isHoming = false;
        [SerializeField] bool isPersistenParticleEffect = false;
        [SerializeField] float travelParticleDestroyDelay = 0f;
        [SerializeField] float endParticleDestroyDelay = 0f;
        [SerializeField] GameObject endParticleEffect;
        [SerializeField] GameObject[] travelParticles;
        [SerializeField] UnityEvent onHit;        
        float damage = 0f;
        GameObject instigator = null;
        Health currentTarget;
        
        private void Start() 
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, maxLifeTime);
        }      

        private void Update() 
        {
            if (isHoming && !currentTarget.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime );
        }

        public void SetTarget(Health target, GameObject character, float weaponDamage)
        {
            currentTarget = target;
            damage = weaponDamage;
            instigator = character;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = currentTarget.GetComponent<CapsuleCollider>();
            return currentTarget.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (currentTarget != other.GetComponent<Health>()) { return; }
            if (currentTarget.IsDead()) { return; }

            currentTarget.TakeDamage(instigator, damage);
            speed = 0f;
            onHit.Invoke();
            if (!isPersistenParticleEffect) { Destroy(gameObject); }
            PersistentEffects();
        }

        private void PersistentEffects()
        {   
            if (travelParticles != null)
            {
                foreach (GameObject travelParticle in travelParticles)
                {
                    Destroy(travelParticle);
                }
            }            
            StartCoroutine(DestroyAfterDelay(travelParticleDestroyDelay));
        }

        IEnumerator DestroyAfterDelay(float travelParticleDestroyDelay)
        {
            if (endParticleEffect != null)
            {
                GameObject endFX = Instantiate(endParticleEffect, GetAimLocation(), transform.rotation);
                endFX.transform.parent = gameObject.transform;
                int childCount = gameObject.transform.childCount;
                yield return new WaitForSeconds(endParticleDestroyDelay);
                Destroy(endFX);
            }

            yield return new WaitForSeconds(travelParticleDestroyDelay);
            
            Destroy(gameObject);
        }
    }
}