using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;    // Handling instance of the game object between scenes

    void Awake()
    {
        /* Destroying multiple instances of the Audio Manager */
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();       // Affect audio source to the sound
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string pName)
    {
        // Equivalent to foreach
        // Find an item in Sounds[] where name of the item is equal to pName
        Sound sound = Array.Find(sounds, item => item.name == pName);

        if (sound != null)
        {
            sound.source.Play();
        }
    }
}
