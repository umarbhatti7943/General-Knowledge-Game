using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	[Header("Game Audio Clips")]
	public AudioClip mainMenuSound;
	public AudioClip gamePlaySound, buttonClick, popupSound, WinSound, LoseSound, levelWinSound, challengeWinSound, giftSound, bottleSound, hintUnlockedSound,
        spinWheelSound, spinWheelPrizeSound, coinPickedSound, giftOpen, panelOpen, correctBallCollect, wrongBallCollect;

    [Header("Level Audio Clips")]
    public AudioClip boyAngrySound;


    [Header("Audio Sources")]
	public AudioSource musicSource;
	public AudioSource sfxSource;
    public AudioSource sfxSource_2;
    public AudioSource levelSource;


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
        sfxSource_2.playOnAwake = false;
        sfxSource_2.loop = false;
    }

	public void playMainMenuSound()
	{
		musicSource.clip = mainMenuSound;
		musicSource.Play();
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

    public void playPopupSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = popupSound;
        sfxSource.Play();
    }

    public void playWinSound()
	{
    	if (PlayerPrefs.GetInt("SoundState", 1) == 0) 
			return;

    	sfxSource.clip = WinSound;
    	sfxSource.Play();
	}

	public void playLoseSound()
	{
    	if (PlayerPrefs.GetInt("SoundState", 1) == 0) 
			return;

    	sfxSource.clip = LoseSound;
    	sfxSource.Play();
	}

    public void playLevelWinSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = levelWinSound;
        sfxSource.Play();
    }

    public void playChallengeWinSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = challengeWinSound;
        sfxSource.Play();
    }

    public void playGiftSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource_2.clip = giftSound;
        sfxSource_2.Play();
    }

    public void playBottleSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource_2.clip = bottleSound;
        sfxSource_2.Play();
    }

    public void playHintUnlockSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = hintUnlockedSound;
        sfxSource.Play();
    }

    public void playSpinWheelSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = spinWheelSound;
        sfxSource.Play();
    }

    public void playSpinWheelWinSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = spinWheelPrizeSound;
        sfxSource.Play();
    }

    public void playCoinPickedSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        //Debug.Log("Play Huwe?");
        sfxSource.PlayOneShot(coinPickedSound);
        //sfxSource.clip = coinPickedSound;
        //sfxSource.Play();
    }

    public void playGiftOpenSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = giftOpen;
        sfxSource.Play();
    }

    public void playPanelOpenSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource.clip = panelOpen;
        sfxSource.Play();
    }

    public void playCorrectBallCollectSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource_2.clip = correctBallCollect;
        sfxSource_2.Play();
    }

    public void playWrongBallCollectSound()
    {
        if (PlayerPrefs.GetInt("SoundState", 1) == 0)
            return;

        sfxSource_2.clip = wrongBallCollect;
        sfxSource_2.Play();
    }

    public void PlayCurrentSceneMusic()
    {
        if (PlayerPrefs.GetInt("MusicState", 1) == 0)
            return;

        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
        {
            if (musicSource.clip != mainMenuSound)
            {
                musicSource.clip = mainMenuSound;
                musicSource.Play();
            }
            else if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else if (sceneName == "GameScene")
        {
            if (musicSource.clip != gamePlaySound)
            {
                musicSource.clip = gamePlaySound;
                musicSource.Play();
            }
            else if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }
}