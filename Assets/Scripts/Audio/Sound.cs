using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]   // Adding [System.Serializable] to see properties of the class in Unity inspector
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]     // Adding [Range(0f, 1f)] to handling property with a bar in Unity inspector
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
