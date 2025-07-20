using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager Instance { get; private set; }

    [SerializeField] private AudioClip bgmLoop, clickSound, matchSound, failSound;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.clip = bgmLoop;
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
    }

    private void Start()
    {
        if (bgmSource.clip != null)
        {
            bgmSource.Play();
            Debug.Log("🎵 BGM Playing...");
        }
        else
        {
            Debug.LogError("❌ BGM clip not assigned!");
        }
    }

    public void PlayClick() => sfxSource.PlayOneShot(clickSound);
    public void PlayMatch() => sfxSource.PlayOneShot(matchSound);
    public void PlayFail() => sfxSource.PlayOneShot(failSound);
}
