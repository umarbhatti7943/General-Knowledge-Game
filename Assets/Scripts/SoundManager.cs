using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	[Header("Game Audio Clips")]
	public AudioClip gamePlaySound, buttonClick;

    [Header("Audio Sources")]
	public AudioSource musicSource;
	public AudioSource sfxSource;

    [Header("Audio Listener")]
	[SerializeField]
	AudioListener CurrentAudioListener;

	public static SoundManager _SoundManager;

	void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (_SoundManager == null)
		{
			_SoundManager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		    DontDestroyOnLoad(this.gameObject);
		    verifyAudioSources();
	}

    public void sfxVolume()
	{
		sfxSource.volume = PlayerPrefs.GetFloat("SFXVol", 1);
	}

	public void changeVolume()
	{
		AudioListener.volume = PlayerPrefs.GetFloat("Vol", 1);
	}

	void verifyAudioSources()
	{
		musicSource.playOnAwake = false;
		musicSource.loop = true;
		sfxSource.playOnAwake = false;
		sfxSource.loop = false;
    }

	public void playGameplaySound()
	{
		musicSource.clip = gamePlaySound;
		musicSource.Play();
	}
	
	public void playButtonClickSound()
	{
    	if (PlayerPrefs.GetInt("SoundState", 1) == 0) 
			return;
			
   	 	sfxSource.clip = buttonClick;
    	sfxSource.Play();
	}
}