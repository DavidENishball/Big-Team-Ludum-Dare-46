using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;

[System.Serializable]
public struct MusicSet
{
    public AudioClip topSong;
    public AudioClip bottomSong;
}

public class Jukebox : MonoBehaviour
{
    public List<MusicSet> songs;
    int currentSongs = 0;
    public AudioSource topSource;
    public AudioSource bottomSource;

    public Knob topHighPassKnob;
    public Knob bottomHighPassKnob;
    public Knob topLowPassKnob;
    public Knob bottomLowPassKnob;

    public AxeSimpleSlider volumeSlider;
    public AxeSimpleSlider pitchSlider;
    public AxeSimpleSlider distortionSlider;

    // Start is called before the first frame update
    void Start()
    {
        Signals.Get<PerformVerbSignal>().AddListener(ReceivedVerb);

        topSource.clip = songs[currentSongs].topSong;
        bottomSource.clip = songs[currentSongs].bottomSong;
        topSource.Play();
        bottomSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!topSource.isPlaying && topSource.time >= topSource.clip.length)
        {
            currentSongs++;
            if (currentSongs >= songs.Count)
                currentSongs = 0;
            topSource.clip = songs[currentSongs].topSong;
            bottomSource.clip = songs[currentSongs].bottomSong;
        }
    }

    void SetHighPass(bool topLayer, float highPass)
    {
        if (topLayer)
            topSource.GetComponent<AudioHighPassFilter>().cutoffFrequency = Mathf.Lerp(20, 650, highPass);
        else
            bottomSource.GetComponent<AudioHighPassFilter>().cutoffFrequency = Mathf.Lerp(20, 650, highPass);
    }

    void SetLowPass(bool topLayer, float lowPass)
    {
        if (topLayer)
            topSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = Mathf.Lerp(650, 22000, lowPass);
        else
            bottomSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = Mathf.Lerp(650, 22000, lowPass);
    }

    void SetVolume(float volume)
    {
        topSource.volume = volume;
        bottomSource.volume = volume;
    }

    void SetPitch(float pitch)
    {
        topSource.pitch = Mathf.Lerp(0f, 2f, pitch);
        bottomSource.pitch = Mathf.Lerp(0f, 2f, pitch);
    }

    void SetDistortion(float distortion)
    {
        topSource.GetComponent<AudioDistortionFilter>().distortionLevel = distortion;
        bottomSource.GetComponent<AudioDistortionFilter>().distortionLevel = distortion;
    }

    public void ReceivedVerb(Component source, LifeformManager.EControlVerbs Verb, int data)
    {
        if (source.gameObject.transform.IsChildOf(this.transform))
        {
            //there's probably an easier way but eh
            if (source == topHighPassKnob)
                SetHighPass(true, topHighPassKnob.value);
            else if (source == bottomHighPassKnob)
                SetHighPass(false, bottomHighPassKnob.value);
            else if (source == topLowPassKnob)
                SetLowPass(true, topLowPassKnob.value);
            else if (source == bottomLowPassKnob)
                SetLowPass(false, bottomLowPassKnob.value);
            else if (source == volumeSlider)
                SetVolume(volumeSlider.Value);
            else if (source == pitchSlider)
                SetPitch(pitchSlider.Value);
            else if (source == distortionSlider)
                SetDistortion(distortionSlider.Value);
        }
    }
}
