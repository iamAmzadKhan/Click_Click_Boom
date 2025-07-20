using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string name)
    {
        switch (name)
        {
            case "Click":
                audioSource.clip = audioClips[0]; break;
            case "Match":
                audioSource.clip = audioClips[1]; break;
            case "Fail":
                audioSource.clip = audioClips[2]; break;
        }
        audioSource.Play();
    }
}
