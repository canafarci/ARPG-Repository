using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        private bool hasTriggered = false;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == ("Player") && !hasTriggered)
            {
            GetComponent<PlayableDirector>().Play();
            hasTriggered = true;
            }
            else {return;}
        }
    }
}
