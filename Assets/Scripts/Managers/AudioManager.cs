using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioMixer")]
    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    Sound[] sounds;

    [SerializeField]
    string sceneMusic;

    private static Dictionary<string, float> soundTimerDictionary;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        soundTimerDictionary = new Dictionary<string, float>();
    }

    private void Start()
    {
        if(!String.IsNullOrEmpty(sceneMusic))
        {
            Play(sceneMusic);
        }
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("Music", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("Effects", volume);
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        AssociateSounds(sound, gameObject);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void Play(string name, GameObject obj)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        AssociateSounds(sound, obj);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void PlayOneShot(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        AssociateSounds(sound, gameObject);
        sound.source.PlayOneShot(sound.source.clip, sound.volume);

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void PlayOneShot(string name, GameObject obj)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        AssociateSounds(sound, obj);
        sound.source.PlayOneShot(sound.source.clip, sound.volume);

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }
    public void PlayCooldown(string name)
    {
        StartCoroutine(PlayCooldownRoutine(name));
    }

    IEnumerator PlayCooldownRoutine(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        if (!CanPlaySound(sound)) yield return null;

        yield return new WaitForSeconds(sound.cooldownTime);

        AssociateSounds(sound, gameObject);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void PlayCooldown(string name, float cooldownTime)
    {
        StartCoroutine(PlayCooldownRoutine(name, cooldownTime));
    }

    IEnumerator PlayCooldownRoutine(string name, float cooldownTime)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        if (!CanPlaySound(sound)) yield return null;

        yield return new WaitForSeconds(cooldownTime);

        AssociateSounds(sound, gameObject);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void PlayCooldown(string name, GameObject obj)
    {
        StartCoroutine(PlayCooldownRoutine(name, obj));
    }

    IEnumerator PlayCooldownRoutine(string name, GameObject obj)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        if (!CanPlaySound(sound)) yield return null;

        yield return new WaitForSeconds(sound.cooldownTime);

        AssociateSounds(sound, obj);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void PlayCooldown(string name, GameObject obj, float cooldownTime)
    {
        StartCoroutine(PlayCooldownRoutine(name, obj, cooldownTime));
    }

    IEnumerator PlayCooldownRoutine(string name, GameObject obj, float cooldownTime)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        if (!CanPlaySound(sound)) yield return null;

        yield return new WaitForSeconds(cooldownTime);

        AssociateSounds(sound, obj);
        sound.source.Play();

        if (sound.destroyTime > 0f)
        {
            Destroy(sound.source, sound.destroyTime);
        }
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
        Destroy(sound.source);
    }

    private void AssociateSounds(Sound sound, GameObject obj)
    {
        sound.source = obj.AddComponent<AudioSource>();

        try
        {
            sound.source.clip = sound.clips[UnityEngine.Random.Range(0, sound.clips.Length)];
        }
        catch (NullReferenceException)
        {
            print("Clip missing from Sound " + sound.name);
        }

        try
        {
            sound.source.outputAudioMixerGroup = mixer.FindMatchingGroups(sound.outputMixer.ToString())[0];
        }
        catch (NullReferenceException)
        {
            print("No group mixer named " + mixer.FindMatchingGroups(sound.outputMixer.ToString())[0] + " found");
        }

        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.isLoop;
        sound.source.spatialBlend = sound.spatialBlend;
        sound.source.dopplerLevel = sound.dopplerLevel;
        sound.source.spread = sound.spread;
        sound.source.rolloffMode = sound.audioRolloffMode;
        sound.source.minDistance = sound.minDistance;
        sound.source.maxDistance = sound.maxDistance;
    }


    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + sound.clips[UnityEngine.Random.Range(0, sound.clips.Length)].length < Time.time)
            {
                soundTimerDictionary[sound.name] = Time.time;
                return true;
            }

            return false;
        }

        return true;
    }

    public void FadeOutSound(string name)
    {
        StartCoroutine(FadeOut(name));
    }

    private IEnumerator FadeOut(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        float startVolume = sound.source.volume;

        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / sound.fadeOutTime;
            yield return null;
        }

        sound.source.Stop();
        Destroy(sound.source);
    }

    public void FadeInSound(string name)
    {
        StartCoroutine(FadeIn(name));
    }

    private IEnumerator FadeIn(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            yield return null;
        }

        if (!CanPlaySound(sound)) yield return null;

        AssociateSounds(sound, gameObject);
        sound.source.Play();
        sound.source.volume = 0f;

        while (sound.source.volume < 1)
        {
            sound.source.volume += Time.deltaTime / sound.fadeInTime;
            yield return null;
        }
    }
}