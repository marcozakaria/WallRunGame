using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable] // to add for every sound audioSorce to avois sound cnflicts
public class SoundClips
{
    public AudioClip clip;
    public string clipName;
    [Space(10)]

    /*[Range(0.1f,3f)]
    public float pitch =1;  // not needed now future plans*/
    [Range(0f, 1f)]
    public float Volume = 1;
    public bool PlayOnAwake;
    public bool loopClip;

    [HideInInspector]
    public AudioSource audioSource;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // singletone pattern

    public bool musicMuted = false;
    // public AudioListener audioListener;

    public SoundClips[] soundClips;

    public AudioSource effectsSource;
    public AudioSource musicSource; // for background music

    public float lowPitchRange = .95f;   // for randomizing
    public float highPitchRange = 1.05f;

    [Header("Mixers")]
    public AudioMixerGroup normalMixer;
    public AudioMixerGroup lowMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // intialize each clip with audiosoure
        foreach (SoundClips s in soundClips)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.loop = s.loopClip;
            s.audioSource.playOnAwake = s.PlayOnAwake;
            //s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.Volume;
        }

    }

    public void MusicButtonPressed()
    {
        Play("button");
        if (musicMuted)
        {
            musicMuted = false;
            PlayMainMusic();
        }
        else
        {
            musicMuted = true;
            StopMainMusic();
        }
    }

    public void Play(string name)
    {
        SoundClips s = Array.Find(soundClips, sound => sound.clipName == name); // Lambada expression
        if (s == null)
        {
            Debug.Log("Sound " + name + " not Found");
            return;
        }
        s.audioSource.Play();
    }

    public void StopSound(string name) // for stooping looping sounds
    {
        SoundClips s = Array.Find(soundClips, sound => sound.clipName == name); // Lambada expression
        if (s == null)
        {
            Debug.Log("Sound " + name + " not Found");
            return;
        }
        s.audioSource.Stop();
    }

    public void StopMainMusic()
    {
        musicSource.enabled = false;
    }

    public void PlayMainMusic()
    {
        musicSource.enabled = true;
    }

    public void SetLowMixer()
    {
        musicSource.outputAudioMixerGroup = lowMixer;
    }

    public void SetnormalMixer()
    {
        musicSource.outputAudioMixerGroup = normalMixer;
    }

    public void PlaySingle(AudioClip audioClip) // give it audio clip and will play it
    {
        /* effectsSource.clip = audioClip;
         effectsSource.Play();*/
        effectsSource.PlayOneShot(audioClip);
    }

    public bool EffectSourceIsPlaying()
    {
        return effectsSource.isPlaying;
    }

    public void RandomizeSFX(params AudioClip[] clips) // params to send multible clips sebrated by a comma
    {                                                  // nice to avoid repating sounds
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        effectsSource.clip = clips[randomIndex];
        effectsSource.Play();
    }
}