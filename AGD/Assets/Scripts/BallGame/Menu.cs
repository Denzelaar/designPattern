using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	MenuScreen _mainMenu, _settingsMenu, _languageMenu, _achievementsMenu, _gameModesMenu, _backButton;

    Text _30secText, _120secText, _endlessText, _zenText, _soundsText, _languageButton;

    GameObject _soundButton, _gyroscopeButton, _screenshakeButton, _colorblindButton, _exitButton;

    const float _ANIMATION_DURATION = 1.05f;
	int _timeOnMainMenu;

    Vector3 _backButtonDefaultPos;
    Vector3 _backButtonMovedRightPos;
    Vector3 _backButtonMovedLeftPos;
   
	Transform _achievements;
	Transform _answersAchievements, _scoreAchievements, _mode1Achievements, _mode2Achievements;
	List<GameObject> _achievementButtons;

	//The current menu, where 0 is main menu, 1 highscore, 2 achievements, 3 settings, 4 gamemodes, 5 language
    int _currentMenu;

    int _gameMode = 0;
    
    public void Init()
    {
		// Main Menu
		_mainMenu = GameObject.Find("MainMenu").GetComponent<MenuScreen>();

        // Settings Menu
        _settingsMenu = GameObject.Find("SettingsMenu").GetComponent<MenuScreen>();

        _soundButton = transform.Find ("SettingsMenu/SoundsButton").gameObject;
		_gyroscopeButton = transform.Find("SettingsMenu/GyroscopeButton").gameObject;
		_screenshakeButton = transform.Find ("SettingsMenu/ScreenshakeButton").gameObject;
		_colorblindButton = transform.Find ("SettingsMenu/ColorblindButton").gameObject;
        _exitButton = transform.Find("MainMenu/MenuButtonsHolder/QuitButton").gameObject;

        // Language Menu
        _languageMenu = GameObject.Find("LanguageMenu").GetComponent<MenuScreen>();

		// Achievements Menu
		_achievementsMenu = GameObject.Find("AchievementsMenu").GetComponent<MenuScreen>();

		// Gamemodes Menu
		_gameModesMenu = GameObject.Find("GameModesMenu").GetComponent<MenuScreen>();

        // Back button
        _backButton = transform.Find("BackButton").GetComponent<MenuScreen>();
        _backButtonDefaultPos = _backButton.transform.localPosition;
        _backButtonMovedRightPos = _backButtonDefaultPos + new Vector3(700, 0, 0);
        _backButtonMovedLeftPos = _backButtonDefaultPos - new Vector3(700, 0, 0);
        _backButton.gameObject.SetActive(true);

		// Achievements
		_answersAchievements = transform.Find("AchievementsMenu/Achievements/Answers");
		_scoreAchievements = transform.Find("AchievementsMenu/Achievements/Score");
		_mode1Achievements = transform.Find("AchievementsMenu/Achievements/Mode1");
		_mode2Achievements = transform.Find("AchievementsMenu/Achievements/Mode2");

		AchievementsManager.Instance.SetupAchievementContainer (_answersAchievements, AchievementsManager.ACHIEVEMENT_NAME_ANSWERS);
		AchievementsManager.Instance.SetupAchievementContainer (_scoreAchievements, AchievementsManager.ACHIEVEMENT_NAME_SCORE);
		AchievementsManager.Instance.SetupAchievementContainer (_mode1Achievements, AchievementsManager.ACHIEVEMENT_NAME_MODE1);
		AchievementsManager.Instance.SetupAchievementContainer (_mode2Achievements, AchievementsManager.ACHIEVEMENT_NAME_MODE2);

		_achievements = transform.Find ("AchievementsMenu/Achievements");
		_achievementButtons = new List<GameObject> ();
		_PopulateAchievementButtonList (_achievements);
		AchievementsManager.Instance.SetDescriptionMenu(transform.Find("DescriptionMenu").gameObject);

        // Game mode Texts
        _30secText = transform.Find("GameModesMenu/30SecButton/Text").GetComponent<Text>();
        _120secText = transform.Find("GameModesMenu/120SecButton/Text").GetComponent<Text>();
        _endlessText = transform.Find("GameModesMenu/EndlessButton/Text").GetComponent<Text>();
        _zenText = transform.Find("GameModesMenu/ZenButton/Text").GetComponent<Text>();
        _soundsText = transform.Find("SettingsMenu/SoundsButton/Text").GetComponent<Text>();
        _languageButton = transform.Find("SettingsMenu/LanguageButton/Text").GetComponent<Text>();

        RefreshLanguage();

        Reset();
    }

	// * * * * * * * * * * * * * MAIN MENU METHODS * * * * * * * * * * * * * //

	public void Reset()
	{
		PreferencesManager.Instance.UpdateButton ("gyroscope", _gyroscopeButton);
		PreferencesManager.Instance.UpdateButton ("screenshake", _screenshakeButton);
		PreferencesManager.Instance.UpdateButton ("sound", _soundButton);

		_mainMenu.MoveIntoScreen();
        _backButton.transform.localPosition = _backButtonMovedRightPos;
        _settingsMenu.ResetPosition ();
		_languageMenu.ResetPosition ();
		_achievementsMenu.ResetPosition ();
		_gameModesMenu.ResetPosition ();
        _currentMenu = 0;

		UpdateAchievementButtons ();
    }

    public void HandleStartButton()
    {
		AnalyticsManager.Instance.LogEvent("Main Menu", "Play Pressed", "Time on Main Menu", _timeOnMainMenu); 
        //_googleAnalytics.LogScreen("Start Game");


		_mainMenu.MoveLeft ();
		_gameModesMenu.MoveIntoScreen ();
        _backButton.MoveBackButtonTo(_backButtonDefaultPos);
        _currentMenu = 4;
    }

    void RefreshLanguage()
    {
        _30secText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.SEC30);
        _120secText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.SEC120);
        _endlessText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.ENDLESS);
        _zenText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.ZEN);

        _soundsText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.SOUNDS);
        _languageButton.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.LANGUAGEBUTTON);

    }

    public void HandleSoundButton()
	{
		AnalyticsManager.Instance.LogEvent("Main Menu", "Buttonpress", "Audio Button", PlayerPrefs.GetInt("sound"));
		PreferencesManager.Instance.ChangePlayerPreference ("sound");
		PreferencesManager.Instance.UpdateButton ("sound", _soundButton);
        AudioManager.Instance.ToggleSoundOn(PlayerPrefs.GetInt("sound"));
	}

	public void HandleHighscoresButton()
	{
        //TODO: add highscore menu / functionality
        //AnalyticsManager.Instance.LogScreen("Highscore Screen");
        //_currentMenu = 1;
    }

	public void HandleAchievementsButton()
	{
		AnalyticsManager.Instance.LogScreen("Achievement Screen");
		_mainMenu.MoveLeft ();
		_achievementsMenu.MoveIntoScreen ();
        _backButton.MoveBackButtonTo(_backButtonDefaultPos);
        _currentMenu = 2;
    }

	public void HandleSettingsButton()
	{
		AnalyticsManager.Instance.LogScreen("Settings Screen");
		_mainMenu.MoveLeft ();
		_settingsMenu.MoveIntoScreen ();
        _backButton.MoveBackButtonTo(_backButtonDefaultPos);
        _currentMenu = 3;
    }

    public void HandleExitButton()
    {
        Application.Quit();
    }

    // * * * * * * * * * * * * * GAME MODES MENU METHODS * * * * * * * * * * * * * //

    public void HandleGameModeButton(int gameMode)
	{
		_gameModesMenu.MoveLeft ();
		_gameMode = gameMode;
		Invoke("_HandleOnAnimationEnded", _ANIMATION_DURATION);
        _backButton.MoveBackButtonTo(_backButtonMovedLeftPos);

        switch (gameMode)
        {
            case 0:
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_30);
                break;
            case 1:
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_120);
                break;
            case 2:
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_endless);
                break;
            case 3:
                AudioManager.Instance.StopMusic();
                break;
            default:
                break;

        }
	}

	public void _HandleOnAnimationEnded()
	{
        GameMode.currentGamemode = _gameMode;
        GameManager.Instance.StartGame();    
	}

	// * * * * * * * * * * * * * SETTINGS MENU METHODS * * * * * * * * * * * * * //


	public void HandleGyroscopeButton()
	{
		AnalyticsManager.Instance.LogEvent("Settings Menu", "Buttonpress", "Gyroscope Button", PlayerPrefs.GetInt("gyroscope"));
		PreferencesManager.Instance.ChangePlayerPreference("gyroscope");
		PreferencesManager.Instance.UpdateButton ("gyroscope", _gyroscopeButton);
	}

    public void HandleLanguageButton()
    {
		_settingsMenu.MoveLeft ();
		_languageMenu.MoveIntoScreen ();
        _currentMenu = 5;
    }

	public void HandleScreenshakeButton()
	{
		AnalyticsManager.Instance.LogEvent("Settings Menu", "Buttonpress", "Screenshake Button", PlayerPrefs.GetInt("screenshake"));
		PreferencesManager.Instance.ChangePlayerPreference("screenshake");
		PreferencesManager.Instance.UpdateButton ("screenshake", _screenshakeButton);
	}
		
	public void HandleColorblindButton()
	{
		//AnalyticsManager.Instance.LogEvent("Settings Menu", "Buttonpress", "Colorblind Button", PlayerPrefs.GetInt("colorblind"));

		// TODO: colorblind functionality
	}

	// * * * * * * * * * * * * * LANGUAGE MENU METHODS * * * * * * * * * * * * * //

    //TODO: Hier één functie van maken met een int in de parameters en een switchcase erin.
    public void HandleDutchButton()
    {
        LanguageManager.Instance.SetCurrentLanguage("Dutch", true);
        PlayerPrefs.SetString("PreferedLanguage", "Dutch");
        RefreshLanguage();
        _mainMenu.MoveIntoScreen();
        _languageMenu.MoveRight();
        _backButton.MoveBackButtonTo(_backButtonMovedRightPos);
        _settingsMenu.ResetPosition();
        _currentMenu = 0;
        RefreshLanguage();
    }

    public void HandleEnglishButton()
    {
        LanguageManager.Instance.SetCurrentLanguage("English", true);
        PlayerPrefs.SetString("PreferedLanguage", "English");
        RefreshLanguage();
        _mainMenu.MoveIntoScreen();
        _languageMenu.MoveRight();
        _backButton.MoveBackButtonTo(_backButtonMovedRightPos);
        _settingsMenu.ResetPosition();
        _currentMenu = 0;
        RefreshLanguage();
    }

    public void HandleSpanishButton()
    {
        LanguageManager.Instance.SetCurrentLanguage("Spanish", true);
        PlayerPrefs.SetString("PreferedLanguage", "Spanish");
        RefreshLanguage();
        _mainMenu.MoveIntoScreen();
        _languageMenu.MoveRight();
        _backButton.MoveBackButtonTo(_backButtonMovedRightPos);
        _settingsMenu.ResetPosition();
        _currentMenu = 0;
        RefreshLanguage();
    }

    public void HandleChineseButton()
    {
        LanguageManager.Instance.SetCurrentLanguage("Chinese", true);
        PlayerPrefs.SetString("PreferedLanguage", "Chinese");
        RefreshLanguage();
        _mainMenu.MoveIntoScreen();
        _languageMenu.MoveRight();
        _backButton.MoveBackButtonTo(_backButtonMovedRightPos);
        _settingsMenu.ResetPosition();
        _currentMenu = 0;
        RefreshLanguage();
    }

	// * * * * * * * * * * * * * ACHIEVEMENTS MENU METHODS * * * * * * * * * * * * * //

	public void HandleAchievementButton(int achievementNumber)
	{
        if (achievementNumber == 11)
        {
            PlayerPrefs.DeleteAll();
            PreferencesManager.Instance.SetInitialPreference("gyroscope", false);
            PreferencesManager.Instance.SetInitialPreference("screenshake", true);
            PreferencesManager.Instance.SetInitialPreference("sound", true);
            PreferencesManager.Instance.SetInitialPreference("tutorial", false);
        }
    }

	public void UpdateAchievementButtons()
	{
		AchievementsManager.Instance.UpdateButtons (_achievementButtons);
	}

	public void HandleDescriptionMenuClick()
	{
		AchievementsManager.Instance.HideDescription ();
	}

	public void HandleGooglePlayAchievementsClick()
	{
		//TODO: do thing
	}

    // * * * * * * * * * * * * * BACK BUTTON * * * * * * * * * * * * * //

    public void HandleBackButton()
    {
        _backButton.GetComponent<Button>().interactable = false;
        switch (_currentMenu)
        {
            case 1:
                //move highscore in, menu out
                break;
		case 2:
				AchievementsManager.Instance.HideDescription ();
                _achievementsMenu.MoveRight();
                break;
            case 3:
                _settingsMenu.MoveRight();
                break;
            case 4:
                _gameModesMenu.MoveRight();
                break;
        }
        if (_currentMenu != 5)
        {
            _mainMenu.MoveIntoScreen();
            _backButton.MoveBackButtonTo(_backButtonMovedRightPos);
            _currentMenu = 0;
        }
        else
        {
            _settingsMenu.MoveIntoScreen();
            _languageMenu.MoveRight();
            _currentMenu = 3;
        }

        Invoke("_ReenableButton", 0.5f);
    }

    void _ReenableButton()
    {
        _backButton.GetComponent<Button>().interactable = true;
    }

    // * * * * * * * * * * * * * HELPER METHODS * * * * * * * * * * * * * //

	void _PopulateAchievementButtonList(Transform transf)
	{
		foreach (Transform child in transf)
		{
			if (child.childCount == 0)
			{
				_achievementButtons.Add (child.gameObject);
			}
			else
			{
				_PopulateAchievementButtonList (child);
			}
		}
	}
}
