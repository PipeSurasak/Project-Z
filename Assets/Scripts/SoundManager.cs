using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip a_fire,a_bit,a_walk,a_Splash;
    static AudioSource audioSource;
    

    void Start()
    {
        a_fire = Resources.Load<AudioClip>("FIRE");
        a_bit = Resources.Load<AudioClip>("BIT");
        a_walk = Resources.Load<AudioClip>("WALK");
        a_Splash = Resources.Load<AudioClip>("Splash");
        audioSource = GetComponent<AudioSource>();
        
    }

   public static void PlaySound (string clip)
   {
       switch(clip)
       {
        case"FIRE" :
            audioSource.PlayOneShot (a_fire);
            audioSource.volume = 0.1f;

            break;
        case"BIT" :
            audioSource.PlayOneShot (a_bit);
            audioSource.volume = 0.2f;
            break;
         case"WALK" :
            audioSource.PlayOneShot (a_walk);
            break;
        case"Splash" :
            audioSource.PlayOneShot (a_Splash);
            audioSource.volume = 0.2f;
            break;        


       }
       
   }

}
