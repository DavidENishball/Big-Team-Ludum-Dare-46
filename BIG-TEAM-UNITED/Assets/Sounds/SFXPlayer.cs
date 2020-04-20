using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    static SFXPlayer instance;
    public static SFXPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("SFXHandler"));
                instance = go.GetComponent<SFXPlayer>();
            }
            return instance;
        }
    }

    public List<AudioClip> buttonNoises;
    public AudioClip heavyButton;
    public AudioClip tubeFlush;
    public AudioClip tubeRaise;
    public AudioClip explosionSound;
    //public AudioClip flushSound;
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip beep;


    public void PlayButtonNoise(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(buttonNoises[Random.Range(0, buttonNoises.Count)], audioPos);
    }

    public void PlayHeavyButtonNoise(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(heavyButton, audioPos);
    }

    public void TubeFlush(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(tubeFlush, audioPos);
    }

    public void TubeRaise(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(tubeRaise, audioPos);
    }

    /*public void FlushSound(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(flushSound, audioPos);
    }*/

    public void ExplosionSound(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(explosionSound, audioPos);
    }

    public void PositiveSound(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(positiveSound, audioPos);
    }

    public void NegativeSound(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(negativeSound, audioPos);
    }

    public void Beep(Vector3 audioPos)
    {
        AudioSource.PlayClipAtPoint(beep, audioPos);
    }
}
