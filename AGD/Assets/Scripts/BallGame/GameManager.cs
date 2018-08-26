using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    Menu _menu;
    BallGame _ballGame;
    TimerForTimeGameMode _timerForTimeGameMode;
	//PreferencesManager _preferencesManager;

    [SerializeField]
    Transform canvas;

    override public void Init()
    {
        LanguageManager.Instance.RegisterLanguage("Dutch", new LanguageDutch());
        LanguageManager.Instance.RegisterLanguage("English", new LanguageEnglish());
        LanguageManager.Instance.RegisterLanguage("Spanish", new LanguageSpanish());
        LanguageManager.Instance.RegisterLanguage("Chinese", new LanguageChinese());

        LanguageManager.Instance.DetermineAndSetInitialLanguage();

        if (PlayerPrefs.HasKey("PreferedLanguage"))
        {
            LanguageManager.Instance.SetCurrentLanguage(PlayerPrefs.GetString("PreferedLanguage"), true);
        }
        else
        {
            LanguageManager.Instance.SetCurrentLanguage(LanguageManager.Instance.currentLanguageName, true);
            PlayerPrefs.SetString("PreferedLanguage", LanguageManager.Instance.currentLanguageName);
        }

        _menu = canvas.Find("MenuView").GetComponent<Menu>();
        _menu.Init();
        _ballGame = canvas.Find("GameView").GetComponent<BallGame>();      
        _ballGame.Init();
        _timerForTimeGameMode = _ballGame.transform.Find("GameTimer").GetComponent<TimerForTimeGameMode>();
    }

    public void StartGame()
    {
        _menu.gameObject.SetActive(false);
        _ballGame.gameObject.SetActive(true);
        //Animates game
        iTween.MoveFrom(_ballGame.gameObject, iTween.Hash("position", _ballGame.gameObject.transform.localPosition - new Vector3(0, 1000, 0), "islocal", true, "time", 1));

        _ballGame.Prepare();
    }

	public void QuitGame ()
	{
		_ballGame.gameObject.SetActive (false);
		_menu.gameObject.SetActive (true);
		_menu.Reset();
	}
}
