using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerForTimeGameMode : MonoBehaviour
{
    Text _gameTimeText;
    float _gameTime;
    bool _initialised = false;
    bool _running = false;

    public delegate void TimerForTimeGameModeEvent();
    public TimerForTimeGameModeEvent TimedOut;

    void Init()
    {
        _gameTimeText = gameObject.GetComponent<Text>();
        _initialised = true;
    }

    public void SetGameMode(int gameMode)
    {
        if (!_initialised)
        {
            Init();
        }

        switch (gameMode)
        {
            case 0:    
                _gameTime = 30;
                _gameTimeText.text = _gameTime.ToString();
                _gameTimeText.color = Color.white;
                gameObject.SetActive(true);
                return;
            case 1: 
                _gameTime = 120;
                _gameTimeText.text = _gameTime.ToString();
                _gameTimeText.color = Color.white;
                gameObject.SetActive(true);
                return;
            case 2:
                gameObject.SetActive(false);
                return;
            case 3:
                gameObject.SetActive(false);
                return;
        }
    }

    public void StartTimer()
    {
        _running = true;
    }

    public void StopTimer()
    {
        _running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_running)
        {
            if (_gameTime <= 5 && !(_gameTime <= 0))
            {
                _gameTimeText.color = Color.red;
                iTween.PunchScale(_gameTimeText.gameObject, new Vector3(0, 1, 0), 0.5f);
            }
            _gameTime = _gameTime - Time.deltaTime;
            _gameTimeText.text = _gameTime.ToString("F2");
            if (_gameTime <= 0)
            {
                _running = false;
                if (TimedOut != null)
                {
                    TimedOut();
                }
                _gameTime = 0;
                _gameTimeText.text = _gameTime.ToString("F2");
                _gameTimeText.color = Color.white;
            }
        }        
    }
}
