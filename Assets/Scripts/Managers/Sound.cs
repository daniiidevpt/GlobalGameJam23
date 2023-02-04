using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;

[System.Serializable]
public class Sound
{
    [Header("---")]
    public string name;

    public AudioClip[] clips;

    public enum OutputMixer
    {
        Music,
        Effects
    };

    public OutputMixer outputMixer;

    [Header("---")]
    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    [Range(0f, 5f)]
    public float destroyTime = 1f;

    [Range(0f, 5f)]
    public float fadeInTime = 0f;

    [Range(0f, 5f)]
    public float fadeOutTime = 0f;

    [Range(0f, 1f)]
    public float cooldownTime = 0f;

    [Header("---")]
    public bool isLoop;

    [Header("3D Sound Settings")]
    [Header("---")]
    [Range(0f, 1f)]
    public float spatialBlend = 0f;

    [Range(0f, 5f)]
    public float dopplerLevel = 1f;

    [Range(0, 360)]
    public int spread = 0;

    [Header("Volume Rolloff")]
    public AudioRolloffMode audioRolloffMode;
    public float minDistance = 1f;
    public float maxDistance = 500f;

    [HideInInspector]
    public AudioSource source;
}