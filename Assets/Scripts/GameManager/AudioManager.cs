using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip themeMusic;
    public AudioClip gameMusic;
    public AudioClip buttonClickClip;

    public AudioMixer audioMixer;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayThemeMusic();
    }
    public void SetMusicVolume(float volume)
    {
        //musicSource.volume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        //sfxSource.volume = volume;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        sfxVolume = volume;
    }
    public void PlayInGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
    public void PlayThemeMusic()
    {
        musicSource.clip = themeMusic;
        musicSource.Play();
    }
    public void PlaySFXButtonClicked()
    {
        sfxSource.PlayOneShot(buttonClickClip);
    }
    
}
