using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    Text _counterText;

    bool _counting = true;

    BallGame _game;

    const float _COUNTDOWN_AMOUNT = 3;
    float _counter;
    Quaternion _rotation;

    bool _initialized = false;

	// Use this for initialization
	void _Init () {
        _counterText = transform.Find("PrepareCountdownText").GetComponent<Text>();
        _game = transform.GetComponent<BallGame>();
        _counter = _COUNTDOWN_AMOUNT;
        _initialized = true;
        _rotation = _counterText.transform.rotation;
    }

    public void StartCountDown()
    {
        if (!_initialized)
        {
            _Init();
        }
        _counter = _COUNTDOWN_AMOUNT;
        if(GameMode.currentGamemode != 3)
        {
            AudioManager.Instance.PlaySoundEffect(AudioIDs.TIMER_TICK_3);
        }       
        _counting = true;
        _counterText.text = _counter.ToString();
        _counterText.gameObject.SetActive(true);
        Invoke("_Count", 1);
    }

    void _Count()
    {
        if (_counter > 1)
        {
            _counter--;
            _counterText.text = _counter.ToString();

            if(_counter == 2 && GameMode.currentGamemode != 3)
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.TIMER_TICK_2);
            }
            
            if(_counter == 1 && GameMode.currentGamemode != 3)
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.TIMER_TICK_1);
            }

            Invoke("_Count", 1);
        }
        else if(_counter == 1)
        {
            
            iTween.PunchRotation(_counterText.gameObject, new Vector3(0, 0, 180), 3);
            _counter--;
            _counterText.text = "GO!";
            if(GameMode.currentGamemode != 3)
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.TIMER_TICK_GO);
            }
            _game.StartGame();
            _counting = false;
            Invoke("_Count", 1);
        }
        else
        {
            iTween.Stop(gameObject);
            _counterText.transform.rotation = _rotation;
            iTween.Stop(_counterText.gameObject);
            _counterText.gameObject.SetActive(false);
        }
        
    }

    public bool inCountdown
    {
        get
        {
            return _counting;
        }
    }
}
