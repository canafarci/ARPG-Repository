using UnityEngine;
 namespace RPG.SFX
 {
    public class RandomAudioPlayer : MonoBehaviour
    {
        public AudioClip[] damageSounds;
        public AudioClip[] dieSounds;
        public AudioClip[] weaponSounds;
        public AudioClip[] magicImpactSounds;
        public AudioSource soundPlayer;
        AudioClip soundClip;

        public void PlayDamageSounds()
        {
            int index = Random.Range(0, damageSounds.Length);
            soundClip = damageSounds[index];
            soundPlayer.clip = soundClip;
            soundPlayer.Play();
        }

        public void PlayDieSounds()
        {
            int index = Random.Range(0, dieSounds.Length);
            soundClip = dieSounds[index];
            soundPlayer.clip = soundClip;
            soundPlayer.Play();
        }

        public void PlayWeaponSounds()
        {
            int index = Random.Range(0, weaponSounds.Length);
            soundClip = weaponSounds[index];
            soundPlayer.clip = soundClip;
            soundPlayer.Play();
        }

        public void PlayMagicSounds()
        {
            int index = Random.Range(0, magicImpactSounds.Length);
            soundClip = magicImpactSounds[index];
            soundPlayer.clip = soundClip;
            soundPlayer.Play();
        }
    }
}