using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
  //singleton
    public static SoundEffectsHelper Instance;
    public AudioClip playerShotSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null){
          Debug.LogError("Multiple instances of SoundEffectsHelper!");
      }
      Instance = this;
    }

    public void MakePlayerShotSound()
    {
      MakeSound(playerShotSound); //pew.wav
    }

    public void MakeHitSound()
    {
      MakeSound(hitSound); //boop.mps
    }

    private void MakeSound(AudioClip originalClip)
    {
      AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
