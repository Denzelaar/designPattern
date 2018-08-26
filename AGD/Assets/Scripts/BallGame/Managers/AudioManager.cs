using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource _musicSource;
    List<AudioSource> _soundEffectSources;
    int _soundEffectSourceIndex;
    bool _soundOn;

    /* Music clips */
    [SerializeField]
    AudioClip _gameMusic;
    [SerializeField]
    AudioClip _zenMusic;

    /* Sound effect clips */
    [SerializeField]
    AudioClip _timerTick3;
    [SerializeField]
    AudioClip _timerTick2;
    [SerializeField]
    AudioClip _timerTick1;
    [SerializeField]
    AudioClip _timerTickGo;
    [SerializeField]
    AudioClip _ballPop;
    [SerializeField]
    AudioClip _correct;
    [SerializeField]
    AudioClip _incorrect;
    [SerializeField]
    AudioClip _newHighscore;


    override public void Init()
    {
        _musicSource = Camera.main.transform.Find("MusicSource").GetComponent<AudioSource>();
        _soundEffectSources = new List<AudioSource>();
        _soundEffectSources.AddRange(Camera.main.transform.Find("Audio Source").GetComponents<AudioSource>());

        _soundEffectSourceIndex = -1;
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            _soundOn = true;
            PlayMusic(AudioIDs.GAME_MUSIC);
        }
        else
        {
            _soundOn = false;
        }
    }

    public void ToggleSoundOn(int preference)
    {
        if (preference==0)
        {
            _soundOn = false;
            _musicSource.Stop();
        }
        else
        {
            _soundOn = true;
            if (GameMode.currentGamemode != 3)
            {
                PlayMusic(AudioIDs.GAME_MUSIC);
            }
            else
            {
                PlayMusic(AudioIDs.ZEN_MUSIC);
            }
        }
    }

    public void PlaySoundEffect(string audioID)
    {
        // Select the next audioSource to use for playing the specified sound effect (NOTE: previous sound effects may be stopped if the number of simultaneous sounds exceeds the amount of sources)
        _soundEffectSourceIndex = (_soundEffectSourceIndex + 1) % _soundEffectSources.Count;
        AudioSource audioSource = _soundEffectSources[_soundEffectSourceIndex];
        switch (audioID)
        {
            case AudioIDs.TIMER_TICK_1:
                audioSource.clip = _timerTick1;
                break;
            case AudioIDs.TIMER_TICK_2:
                audioSource.clip = _timerTick2;
                break;
            case AudioIDs.TIMER_TICK_3:
                audioSource.clip = _timerTick3;
                break;
            case AudioIDs.TIMER_TICK_GO:
                audioSource.clip = _timerTickGo;
                break;
            case AudioIDs.BALL_POP:
                audioSource.clip = _ballPop;
                break;
            case AudioIDs.CORRECT:
                audioSource.clip = _correct;
                break;
            case AudioIDs.INCORRECT:
                audioSource.clip = _incorrect;
                break;
            case AudioIDs.NEW_HIGHSCORE:
                audioSource.clip = _newHighscore;
                break;
        }
        if (_soundOn)
        {
            audioSource.Play();
        }
    }

    public void PlayMusic(string audioID)
    {
        switch (audioID)
        {
            case AudioIDs.GAME_MUSIC:
                _musicSource.clip = _gameMusic;
                break;
            case AudioIDs.ZEN_MUSIC:
                _musicSource.clip = _zenMusic;
                break;
        }
        if (_soundOn)
        {
            _musicSource.Play();
        }
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }
}

