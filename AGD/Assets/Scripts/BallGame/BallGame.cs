using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallGame : MonoBehaviour {

    List<Ball> _orangeBalls, _redBalls, _whiteBalls, _blueBalls;
    List<List<Ball>> _allBalls;

	int[] _ballSorter;
	int _highestCountofBalls;
	int _secondHighestCountOfBalls;

    List<bool> _phases;

    List<string> _negativeFeedbackStrings;
    List<string> _positiveFeedbackStrings;

    Text _feedbackText;
    Text _countDownText;
    Text _playerScoreText;
    Text _phaseFeedbackText;
    Text _multiplierText;
    Text _gameOverText;
	public string currentPhase;

    GameObject _retryButton, _homeButton, _showAchievementButton, _showLeaderboardButton;
    GameObject _gameOverOverlay;

	GameObject _pauseMenu;
	GameObject _tutorialText;
	FadeTutorialImage _tutorialHint;
	List<Button> clickableObjects;
	GameObject _pauseButton, _pausePlayButton, _pauseSoundButton,_pauseGyroscopeButton, _pauseScreenshakeButton, _pauseHomeButton;

	GameObject _popupMenu;

    Counter _counter;

    PowerUps _powerUpsManager;

    CanvasGroup _canvasGroup;

    Transform _ballSpawnArea;
    Transform _ballsHolder;
    Transform _buttonHolder;

    RectTransform _rect;

    bool _inGameOverCountdown = false;
    bool _gameOver = false;

    bool _gamemodeTimerTimedout = false;

	bool _isTutorialActive = false;
	bool _firstPlaythrough = true;
	bool _gamePaused = false;

    bool _buttonsSwapped;

    bool _preparing;

	bool _supportsAccelerometer;
	bool _supportsGyroscope;
	bool _enabledGyroscope;
	Vector3 _START_GRAVITY_DIRECTION = new Vector3(0, 9.81f, 0);
	Vector3 _gravityDirection;
  
    float _ballSpawnLocation;

	int _timePlayed;
    int _playerScore;
    int _comboCount;
	int _wrongAnswerCount;
    int _multiplier;
	float _multiplierBallSpawn;

    float _ballsize;
    bool _variateBallsize = false;
	bool _variateShape =  false;
	bool _ghostPhase = false;

    const float _FEEDBACK_DURATION = 2f;

    const int _MAX_AMOUNT_OF_BALLS_HORIZONTAL = 10;
    const float _ASPECT_RATIO = 0.5625f;

    const int _MAX_START_BALLS = 20;
    const int _MIN_START_BALLS = 2;

    const int _MAX_START_BALLS_ZEN = 30;
    const int _MIN_START_BALLS_ZEN = 10;

    const int _MAX_BALLS_ADDED_PER_CYLE = 12;
    const int _MIN_BALLS_ADDED_PER_CYCLE = 1;

    const int _MAX_BALLS_ADDED_ON_INCORRECT = 5;
    const int _MIN_BALLS_ADDED_ON_INCORRECT = 1;

    const int _MAX_BALLS_ALLOWED = 120;

	const int _SCOREPUNISHMENT = 100;
	const int _SCOREREWARD = 100;

    const int _BALLSCORE_BY_POWERUP = 10;

    float _maxSpawnDistance;

    const float _DEFAULT_SPAWNCYCLE_DELAY = 4;
    const float _FASTPHASE_SPAWNCYCLE_DELAY = 2;
    float _spawnCycleDelay;

    const float _GAMEOVER_COUNTDOWN_TIME = 5f;
    float _gameOverCountdownTime;

	const float _TUTORIAL_DELAY = 1.05f;

    TextAnimations _textAnimations;
    ButtonShake _buttonShake;

    TimerBar _timerBar;
    TimerForTimeGameMode _timerForTimeGameMode;
	GameObject _timerCountdown;

    CanvasGroup _buttonHolderCanvasGroup;

    PlayGamesScript _playGamesScript;
	string[] COLORS = {"Orange", "Red", "Blue", "White"};
	const int _ORANGE = 0;
	const int _RED = 1;
	const int _BLUE = 2;
	const int _WHITE = 3;

	string[] PHASES = {"No Phase", "Fast Phase", "Button Swap Phase", "Variating Size Phase", "Variating Shapes Phase", "Ghost Phase" };
	const int _NOPHASE = 0;
	const int _FASTPHASE = 1;
	const int _BUTTONSWAPPHASE = 2;
	const int _SIZEPHASE = 3;
	const int _SHAPEPHASE = 4;
	const int _GHOSTPHASE = 5;

    public bool adsAreOn;

    public void Init()
    {
        _rect = transform.GetComponent<RectTransform>(); 

        _textAnimations = transform.Find("FeedbackText").GetComponent<TextAnimations>();

        _multiplierText = transform.Find("ScoreHolder/MultiplierText").GetComponent<Text>();

        _ballsize = transform.GetComponent<RectTransform>().sizeDelta.x/ _MAX_AMOUNT_OF_BALLS_HORIZONTAL;
        _ballSpawnLocation = 540 / _ASPECT_RATIO / 2;

        _retryButton = transform.Find("RetryButton").gameObject;
		_homeButton = transform.Find ("HomeButton").gameObject;
        _showAchievementButton = transform.Find("ShowAchievementEnd").gameObject;
        _showLeaderboardButton = transform.Find("ShowLeaderBoardEnd").gameObject;
        _gameOverOverlay = transform.Find("GameOverOverlay").gameObject;

		_pauseMenu = transform.Find ("PauseMenu").gameObject;
		_pauseButton = transform.Find ("PauseButton").gameObject;
		_pausePlayButton = transform.Find ("PauseMenu/UnpauseButton").gameObject;
		_pauseSoundButton = transform.Find ("PauseMenu/SoundButton").gameObject;
		_pauseGyroscopeButton = transform.Find ("PauseMenu/GyroscopeButton").gameObject;
		_pauseScreenshakeButton = transform.Find ("PauseMenu/ScreenshakeButton").gameObject;
		_pauseHomeButton = transform.Find ("PauseMenu/PauseHomeButton").gameObject;

        _counter = transform.GetComponent<Counter>();

        _powerUpsManager = transform.GetComponent<PowerUps>();
        _powerUpsManager.Init();

		_supportsAccelerometer = SystemInfo.supportsAccelerometer;
		_supportsGyroscope = SystemInfo.supportsGyroscope;
		_gravityDirection = _START_GRAVITY_DIRECTION;

        _ballSpawnArea = transform.Find("BallsHolder/SpawnArea");
        _ballsHolder = transform.Find("BallsHolder");
        _buttonHolder = transform.Find("ButtonHolder");
        _canvasGroup = transform.GetComponent<CanvasGroup>();

        _feedbackText = transform.Find("FeedbackText").GetComponent<Text>();
        _countDownText = transform.Find("CountdownText").GetComponent<Text>();
        _playerScoreText = transform.Find("ScoreHolder/Score").GetComponent<Text>();
        _phaseFeedbackText = transform.Find("PhaseFeedbackText").GetComponent<Text>();
        _gameOverText = transform.Find("GameOverText").GetComponent<Text>();

        _positiveFeedbackStrings = new List<string>();
        _negativeFeedbackStrings = new List<string>();
        RefreshLanguage();

        PhasesManager.Instance.OnPhaseChange += _HandlePhaseChange;

        _phases = new List<bool>();
        _phases = PhasesManager.Instance.phases;

        _textAnimations = transform.Find("FeedbackText").GetComponent<TextAnimations>();
        _textAnimations.Init();
        _buttonShake = _buttonHolder.GetComponent<ButtonShake>();
        _buttonShake.Init();

        _timerBar = transform.Find("TimerBar").GetComponent<TimerBar>();
        _timerForTimeGameMode = transform.Find("GameTimer").GetComponent<TimerForTimeGameMode>();
		_timerCountdown = transform.Find ("GameTimer").gameObject;

        _buttonHolderCanvasGroup = transform.Find("ButtonHolder").GetComponent<CanvasGroup>();

		_tutorialText = transform.Find ("Tutorial").gameObject;
		_tutorialHint = transform.Find ("TutorialHint").gameObject.GetComponent<FadeTutorialImage>();
		_tutorialHint.Init ();
		clickableObjects = new List<Button> ();
		clickableObjects.Add (_pauseButton.GetComponent<Button>());
		clickableObjects.Add (_timerBar.GetComponent<Button> ());
		clickableObjects.Add (_buttonHolder.transform.GetChild (0).GetChild (0).gameObject.GetComponent<Button> ());
		clickableObjects.Add (_buttonHolder.transform.GetChild (0).GetChild (1).gameObject.GetComponent<Button> ());
		clickableObjects.Add (_buttonHolder.transform.GetChild (1).GetChild (0).gameObject.GetComponent<Button> ());
		clickableObjects.Add (_buttonHolder.transform.GetChild (1).GetChild (1).gameObject.GetComponent<Button> ());

		_highestCountofBalls = 0;
		_secondHighestCountOfBalls = 0;

		_popupMenu = transform.Find ("PopupMenu").gameObject;
		AchievementsManager.Instance.SetPopupMenu (transform.Find ("PopupMenu").gameObject);
    }

    public void RefreshLanguage()
    {
        _positiveFeedbackStrings.Clear();
        _positiveFeedbackStrings.AddRange(new string[] { LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS1), LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS2),
            LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS3), LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS4),
            LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS5), LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDPOS6)});

        _negativeFeedbackStrings.Clear();
        _negativeFeedbackStrings.AddRange(new string[] { LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDNEG1), LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDNEG2),
            LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDNEG3), LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDNEG4),
            LanguageManager.Instance.GetLanguageText(LanguageTextIDs.FEEDNEG5) });

        transform.Find("ScoreHolder/ScoreText").GetComponent<Text>().text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.SCORE);
    }

    public void Prepare()
    {
        _preparing = true;
		currentPhase = PHASES[_NOPHASE];
        _ResetVariables();
        _counter.StartCountDown();
		//Debug.Log ("gyroscope preference is: " + PlayerPrefs.GetInt ("Gyroscope"));
		_enabledGyroscope = (PlayerPrefs.GetInt ("Gyroscope") == 1);
		//Debug.Log ("_enabledGyroscope = " + _enabledGyroscope);
		if (_supportsGyroscope && _enabledGyroscope)
		{
			Input.gyro.enabled = true;
		}
        _canvasGroup.blocksRaycasts = false;
        RefreshLanguage();
        _timerForTimeGameMode.SetGameMode(GameMode.currentGamemode);

        //wait for draw
        Invoke("SetSizesAndPositions", 0.1f);
		_popupMenu.SetActive (false);

        if(GameMode.currentGamemode == 3)
        {
            _timerBar.gameObject.SetActive(false);
            _playerScoreText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _timerBar.gameObject.SetActive(true);
            _playerScoreText.transform.parent.gameObject.SetActive(true);
        }
        
    }

    void SetSizesAndPositions()
    {
        float areaSizeX = _ballsHolder.GetComponent<RectTransform>().sizeDelta.x;
        float areaSizeY = _ballsHolder.GetComponent<RectTransform>().sizeDelta.y;

        _ballsize = Mathf.Sqrt(areaSizeX * areaSizeY / _MAX_BALLS_ALLOWED);

        _ballSpawnLocation = areaSizeY / _ASPECT_RATIO / 2;

        //_ballsHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(_ballsHolder.transform.GetComponent<RectTransform>().sizeDelta.x, _rect.sizeDelta.x / _ASPECT_RATIO - _buttonHolder.transform.GetComponent<RectTransform>().sizeDelta.y - 90);

        _maxSpawnDistance = areaSizeX / 3;
    }

    void SetGravity(Vector3 newDirection)
    {
        Vector3 dir = newDirection * 9.81f;
        dir.x = Mathf.Clamp(dir.x, -5f, 5f);
        dir.y = Mathf.Clamp(dir.x, -10f, -4f);
        Physics2D.gravity = dir;
    }

    void ResetGravity()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void _ResetVariables()
    {
        _multiplierBallSpawn = 1;
        _wrongAnswerCount = 0;

        _multiplier = 1;
        _multiplierText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.MULTIPLIER) + ": x " + _multiplier.ToString();
        _playerScore = 0;
        _playerScoreText.text = _playerScore.ToString();

		_gameOver = false;
        _gamemodeTimerTimedout = false;

        _ResetPhaseVariables();
    }

    void _ResetPhaseVariables()
    {
        _spawnCycleDelay = _DEFAULT_SPAWNCYCLE_DELAY;
        if (_buttonsSwapped && !_preparing)
        {
            iTween.ScaleTo(_buttonHolder.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.3f));          
        }else if (_buttonsSwapped && _preparing)
        {
            _buttonHolder.transform.localScale = Vector3.one;         
        }
        _preparing = false;
        _buttonsSwapped = false;
        _variateBallsize = false;
        _ghostPhase = false;
        _variateShape = false;      
    }

    public void StartGame()
    {
        PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play);
        ResetBallLists();

		_popupMenu.SetActive (true);
        if(GameMode.currentGamemode == 3)
        {
            _SpawnBalls(_MIN_START_BALLS_ZEN, _MAX_START_BALLS_ZEN);
            AudioManager.Instance.PlayMusic(AudioIDs.ZEN_MUSIC);
        }
        else
        {
            _SpawnBalls(_MIN_START_BALLS, _MAX_START_BALLS);
            PhasesManager.Instance.StartPhaseCirculation();
            Invoke("_HandleSpawnCycle", _DEFAULT_SPAWNCYCLE_DELAY);
            _timerBar.RunForSeconds(_DEFAULT_SPAWNCYCLE_DELAY);
        }
        
        if(GameMode.currentGamemode < 2)
        {
            _timerForTimeGameMode.StartTimer();
            _timerForTimeGameMode.TimedOut += _HandleGamemodeTimerTimedout;
        }
        
        _canvasGroup.blocksRaycasts = true;

        if (GameMode.currentGamemode != 3)
        {
            _PrepareTutorial();
        }          
    }

	void _PrepareTutorial()
	{
		if (PreferencesManager.Instance.Get ("tutorial") == 0)
		{
			_tutorialText.SetActive (true);
			foreach (Button button in clickableObjects)
			{
				button.interactable = false;
			}

			Invoke ("_StartTutorial", _TUTORIAL_DELAY);
		}
	}

	void _StartTutorial()
	{
		_tutorialText.transform.Find ("TutorialText").GetComponent<Text> ().text = LanguageManager.Instance.GetLanguageText (LanguageTextIDs.TUTORIALTEXT);
		_tutorialHint.MoveToCorrectButton ();
		_isTutorialActive = true;
		_gamePaused = true;
	}

	void _PrepareTutorialPart2()
	{
		foreach (Button button in clickableObjects)
		{
			button.interactable = false;
		}
		_gamePaused = false;

		Invoke ("_StartTutorialPart2", 1.85f);
	}

	void _StartTutorialPart2()
	{
		_timerBar.GetComponent<Button> ().interactable = true;

		_tutorialText.transform.Find ("TutorialText").GetComponent<Text> ().text = LanguageManager.Instance.GetLanguageText (LanguageTextIDs.TUTORIALPART2TEXT);
		_tutorialHint.MoveToTimerBar ();

		_gamePaused = true;
	}

	void StopTutorial()
	{
		foreach (Button button in clickableObjects)
		{
			button.interactable = true;
		}
		_tutorialHint.FadeOut ();
		_tutorialText.SetActive (false);
		_isTutorialActive = false;
		PreferencesManager.Instance.SetValue ("tutorial", 1);
		_gamePaused = false;
	}

    void ResetBallLists()
    {
        _orangeBalls = new List<Ball>();
        _redBalls = new List<Ball>();
        _blueBalls = new List<Ball>();
        _whiteBalls = new List<Ball>();
        _allBalls = new List<List<Ball>>();
        _allBalls.Add(_orangeBalls);
        _allBalls.Add(_redBalls);
        _allBalls.Add(_blueBalls);
        _allBalls.Add(_whiteBalls);
    }

    void _SpawnBalls(int minBalls, int maxBalls)
    {
		List<int> randomBallAmount = new List<int>();

		for(int i = 0; i < 4; i++)
		{
			int random = Random.Range(minBalls, maxBalls);
			if (randomBallAmount.Contains(random))
			{
				random = Random.Range(minBalls, maxBalls);
			}
			randomBallAmount.Add(random);
		}

        for(int i = 0; i < _allBalls.Count; i++)
        {
			int currentAmount = randomBallAmount[i];

            for(int j = 0; j < currentAmount; j++)
            {
				string resourcePath = "Ball";
				if (_variateShape)
				{
					int randomShape = Random.Range(0, 4);
					string[] shapes = new string[]{ "Ball", "Triangle", "Square", "Pentagon" };

					resourcePath = shapes[randomShape];
				}
                GameObject ball = ResourcesManager.Instance.GetResourceInstance("BallGame/Ball").gameObject;

                ball.transform.SetParent(_ballSpawnArea);
				float size = _ballsize;

				if (_variateBallsize)
				{
					List<float> sizeMultipliers = new List<float>(new float[] { 1, 1.3f, 0.7f});
					int random = Random.Range (0, sizeMultipliers.Count);
					size = _ballsize * sizeMultipliers[random];
				}
					
				ball.GetComponent<Ball>().SetColorAndPosition(i, Random.Range(-_maxSpawnDistance, _maxSpawnDistance), Random.Range(-_maxSpawnDistance, _maxSpawnDistance) + _ballSpawnLocation, size, _variateShape, _ghostPhase);
				_allBalls [i].Add (ball.GetComponent<Ball> ());
            }
        }

        PhaseStateManager phaseStateManager = new PhaseStateManager();


        phaseStateManager.ChangeToGhostPhase();


    }

    void _StartFeedback(bool correct)
    {
        CancelInvoke("_HandleOnFeedbackEnded");
        if (GameMode.currentGamemode != 3)
        {
            int highestValue = Mathf.Max(_allBalls[_ORANGE].Count, _allBalls[_RED].Count, _allBalls[_BLUE].Count, _allBalls[_WHITE].Count);
            if (correct)
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.CORRECT);
                _feedbackText.text = _positiveFeedbackStrings[Random.Range(0, _positiveFeedbackStrings.Count - 1)];
                _textAnimations.ResetPosition();
                _wrongAnswerCount = 0;
                _feedbackText.gameObject.SetActive(true);
                _textAnimations.RandomAnimationPositive();

                if (highestValue == 0 && !_gameOver && !_gamePaused)
                {
                    GiveBonusPoints();
                }

                Invoke("_HandleOnFeedbackEnded", 2f);

            }
            else
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.INCORRECT);
                if (_inGameOverCountdown)
                {
                    _HandleGameOver();
                }
                else
                {
                    CancelInvoke("_HandleSpawnCycle");
                    Invoke("_HandleSpawnCycle", _spawnCycleDelay);
                    _timerBar.RunForSeconds(_spawnCycleDelay);
                    _SpawnBalls(_MAX_BALLS_ADDED_ON_INCORRECT * (int)_multiplierBallSpawn, _MIN_BALLS_ADDED_ON_INCORRECT * (int)_multiplierBallSpawn);
                    _wrongAnswerCount++;
                    _feedbackText.text = _negativeFeedbackStrings[Random.Range(0, _negativeFeedbackStrings.Count - 1)];
                    _textAnimations.ResetPosition();
                    _feedbackText.gameObject.SetActive(true);
                    _textAnimations.RandomAnimationNegative();
                    Invoke("_HandleOnFeedbackEnded", _FEEDBACK_DURATION);
                }
                if (_wrongAnswerCount % 2 == 0 && !_gameOver && !_gamePaused)
                {
                    _multiplierBallSpawn += 0.3f;
                    if (_playerScore - _SCOREPUNISHMENT > 0)
                    {
                        _playerScore -= _SCOREPUNISHMENT;
                    }
                    else
                    {
                        _playerScore = 0;
                    }
                }

                if (_wrongAnswerCount < 1)
                {
                    _multiplierBallSpawn = 1;
                }
            }
        }
        else
        {
            _textAnimations.ResetPosition();
            if (!correct)
            {
                _textAnimations.NoAnimationNegative();
                _feedbackText.text = _negativeFeedbackStrings[Random.Range(0, _negativeFeedbackStrings.Count - 1)];
            }
            else
            {
                _textAnimations.NoAnimationPositive();
                _feedbackText.text = _positiveFeedbackStrings[Random.Range(0, _positiveFeedbackStrings.Count - 1)];
            }  

            _feedbackText.gameObject.SetActive(true);
            Invoke("_HandleOnFeedbackEnded", _FEEDBACK_DURATION);
        }	
    }

    void _RemoveAllBalls()
    {
        foreach (List<Ball> ballList in _allBalls)
        {
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].Explode(true, 0);
            }
        }
        ResetBallLists();
    }

    public void CleanupBalls()
    {
        foreach (List<Ball> ballList in _allBalls)
        {
            for (int i = ballList.Count - 1; i >= 0; --i)
            {
                if (ballList[i].RemovedByPowerup)
                {
                    ballList[i].Explode(false, _BALLSCORE_BY_POWERUP);
                    _playerScore += _BALLSCORE_BY_POWERUP * _multiplier;
                    ballList.RemoveAt(i);
                }
            }
        }
    }

    void ShowPhaseFeedback(int phaseNumber)
    {
        if(phaseNumber > 0)
        {
            _phaseFeedbackText.text = PhasesManager.Instance.phaseTexts[phaseNumber] + LanguageManager.Instance.GetLanguageText(LanguageTextIDs.PHASESTART);
            _phaseFeedbackText.gameObject.SetActive(true);
            Invoke("_HandlePhaseFeedbackEnded", _FEEDBACK_DURATION);
        }
        else
        {
            _phaseFeedbackText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.PHASEEND);
        }       
    }

    public string GetMajorityBalls()
    {
		int max = _allBalls[0].Count;
		int index = 0;

		for (int i = 1; i < _allBalls.Count; i++)
		{
			if (_allBalls[i].Count > max)
			{
				max = _allBalls[i].Count;
				index = i;
			}
		}
		return COLORS[index];
    }

    ///////////////////////////////////////// BUTTON HANDLERS ////////////////////////////////////////////////

    public void HandleButtonClick(int buttonNumber)
    {
        int highestValue = Mathf.Max(_allBalls[_ORANGE].Count, _allBalls[_RED].Count, _allBalls[_BLUE].Count, _allBalls[_WHITE].Count);
        if (GameMode.currentGamemode != 3)
        {
            if (_allBalls[buttonNumber].Count >= highestValue && highestValue != 0)
            {
                if (_isTutorialActive)
                {
                    _PrepareTutorialPart2();
                }

                // handle achievement check for total number of correct answers
                AchievementsManager.Instance.IncreaseValue(AchievementsManager.ACHIEVEMENT_NAME_ANSWERS);

                float delay = 0;
                float delaysmall = 0.03f;
                int ballScore = 1;
                foreach (Ball ball in _allBalls[buttonNumber])
                {
                    ball.Explode(false, ballScore, delay);
                    _playerScore += ballScore * _multiplier;
                    delay += delaysmall;
                    ballScore++;
                }
                _allBalls[buttonNumber] = new List<Ball>();
                _StartFeedback(true);
                _comboCount++;
                //check if we unlocked an achievement
                if (_playerScore >= 1000)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_a_thousand);
                }
                if (_playerScore >= 2000)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_two_thousand);
                }
                if (_playerScore >= 3000)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_three_thousand);
                }
                if (_playerScore >= 7500)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_7500_points);
                }
                if (_playerScore >= 10000)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_really_yes_really_10000);
                }
                if (_playerScore >= 25000)
                {
                    PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_25000_points);
                }

            }
            else if (highestValue == 0)
            {
                //Do nothing, there are no balls on screen
            }
            else
            {
                _StartFeedback(false);
                _buttonShake.ShakeButton(buttonNumber);
                _comboCount = 0;
                _multiplier = 1;
            }

            if (_comboCount % 5 == 0)
            {
                _multiplier++;
                _multiplierText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.MULTIPLIER) + ": x " + _multiplier.ToString();
            }
            if (_comboCount % 5 == 0 && _comboCount != 0)
            {
                //_powerUpsManager.StreakBomb();
            }
            if (_comboCount < 1)
            {
                _multiplier = 1;
                _multiplierText.text = LanguageManager.Instance.GetLanguageText(LanguageTextIDs.MULTIPLIER) + ": x " + _multiplier.ToString();
            }

            _playerScoreText.text = _playerScore.ToString();
        }
        else
        {
            if (_allBalls[buttonNumber].Count >= highestValue && highestValue != 0)
            {
                float delay = 0;
                float delaysmall = 0.03f;
                int ballScore = 0;
                foreach (Ball ball in _allBalls[buttonNumber])
                {
                    ball.Explode(false, ballScore, delay);
                    delay += delaysmall;
                }
                _allBalls[buttonNumber] = new List<Ball>();
                _StartFeedback(true);
            }
            else
            {
                _StartFeedback(false);
            }

            if (_FieldIsEmpty())
            {
                _SpawnBalls(_MIN_START_BALLS_ZEN, _MAX_START_BALLS_ZEN);
            }
        }
        
    }

    bool _FieldIsEmpty()
    {
        int highestValue = Mathf.Max(_allBalls[_ORANGE].Count, _allBalls[_RED].Count, _allBalls[_BLUE].Count, _allBalls[_WHITE].Count);
        if(highestValue == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void GiveBonusPoints()
	{
		_playerScore += _SCOREREWARD;
	}

    public void HandleRetryButton()
    {
        if(AdsManager.Instance.InAdCoolDown == false && adsAreOn == true)
        {
            AdsManager.Instance.StartAd();
        }
        AnalyticsManager.Instance.LogButton("retry button pressed");
        _RemoveAllBalls();
        _retryButton.SetActive(false);
        _homeButton.SetActive(false);
		_showLeaderboardButton.SetActive(false);
		_showAchievementButton.SetActive(false);
        _feedbackText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _gameOverOverlay.SetActive(false);
        _playerScore = 0;
        _playerScoreText.text = _playerScore.ToString();
        Prepare();
        _gamePaused = false;  
        _buttonShake.GameOver = false;
    }

    public void HandleHomeButton()
    {
        if (GameMode.currentGamemode != 3)
        {
            AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "To Home From Game Over", "Time Played", _timePlayed);
        }          
        Time.timeScale = 1;
        if (_gamePaused)
        {
            _pauseMenu.SetActive(false);
            _ballSpawnArea.gameObject.SetActive(true);
            _gamePaused = false;
        }
        Hide();
        GameManager.Instance.QuitGame();
        
    }

	public void HandlePauseButton()
	{
		AnalyticsManager.Instance.LogButton("Pause Button");
		if (!_gamePaused)
		{
            _gamePaused = true;
			_ballSpawnArea.gameObject.SetActive (false);
			_pauseMenu.SetActive (true);
			PreferencesManager.Instance.UpdateButton ("gyroscope", _pauseGyroscopeButton);
			PreferencesManager.Instance.UpdateButton ("screenshake", _pauseScreenshakeButton);
			PreferencesManager.Instance.UpdateButton ("sound", _pauseSoundButton);
		}
	}

    public void ForceBallSpawn()
    {
        if (!_inGameOverCountdown)
        {
			if (_isTutorialActive)
			{
				StopTutorial ();
			}
            _HandleSpawnCycle();
            CancelInvoke("_HandleSpawnCycle");
            Invoke("_HandleSpawnCycle", _spawnCycleDelay);
            _timerBar.RunForSeconds(_spawnCycleDelay);
        }
    }

	///////////////////////////////////////// PAUSE MENU /////////////////////////////////////////////////

	public void HandleUnpauseButton()
	{
		AnalyticsManager.Instance.LogButton("UnPause Button");
		if (_gamePaused)
		{
			_pauseMenu.SetActive (false);
			_ballSpawnArea.gameObject.SetActive (true);
			_gamePaused = false;
		}
	}

	public void HandleSoundButton()
	{
		AnalyticsManager.Instance.LogEvent("Pause Menu", "Buttonpress", "Sound Button", PlayerPrefs.GetInt("sound"));

		PreferencesManager.Instance.ChangePlayerPreference ("sound");
		PreferencesManager.Instance.UpdateButton ("sound", _pauseSoundButton);
		AudioManager.Instance.ToggleSoundOn(PlayerPrefs.GetInt("sound"));
	}

	public void HandleGyroscopeButton()
	{
		AnalyticsManager.Instance.LogEvent("Pause Menu", "Buttonpress", "Gyroscope Button", PlayerPrefs.GetInt("gyroscope"));
		PreferencesManager.Instance.ChangePlayerPreference ("gyroscope");
		PreferencesManager.Instance.UpdateButton ("gyroscope", _pauseGyroscopeButton);
	}

	public void HandleScreenshakeButton()
	{
		AnalyticsManager.Instance.LogEvent("Pause Menu", "Buttonpress", "Screenshake Button", PlayerPrefs.GetInt("screenshake"));
		PreferencesManager.Instance.ChangePlayerPreference ("screenshake");
		PreferencesManager.Instance.UpdateButton ("screenshake", _pauseScreenshakeButton);
	}

	public void HandlePauseHomeButton()
	{
		AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "To Home From Pause", "Time Played", _timePlayed);
        Time.timeScale = 1;
        if (_gamePaused)
        {
            _pauseMenu.SetActive(false);
            _ballSpawnArea.gameObject.SetActive(true);
            _gamePaused = false;
        }
        Hide();
        _timerForTimeGameMode.StopTimer();
        GameManager.Instance.QuitGame ();	
	}

    public void Hide()
    {
        CancelInvoke();
        _textAnimations.ResetPosition();
        _retryButton.SetActive(false);
        _homeButton.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _phaseFeedbackText.gameObject.SetActive(false);
        _feedbackText.gameObject.SetActive(false);
        _gameOverOverlay.SetActive(false);
        _showLeaderboardButton.SetActive(false);
        _showAchievementButton.SetActive(false);
        if(GameMode.currentGamemode != 3)
        {
            PhasesManager.Instance.StopPhasesCirculation();
            _timerBar.Stop();
        }
        else
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayMusic(AudioIDs.GAME_MUSIC);
        }
        GameMode.currentGamemode = -1;
        _RemoveAllBalls();
    }

    ///////////////////////////////////////// EVENT HANDLERS /////////////////////////////////////////////////

    void _HandleOnFeedbackEnded()
    {
        _feedbackText.gameObject.SetActive(false);
    }

    void _HandlePhaseFeedbackEnded()
    {
        _phaseFeedbackText.gameObject.SetActive(false);
    }

    void _HandleSpawnCycle()
    {
        _SpawnBalls(_MAX_BALLS_ADDED_PER_CYLE, _MIN_BALLS_ADDED_PER_CYCLE);
        Invoke("_HandleSpawnCycle", _spawnCycleDelay);
        _timerBar.RunForSeconds(_spawnCycleDelay);
    }

    void _HandleGameOverCountdown()
    {
        if (_gameOverCountdownTime > 1)
        {
            _gameOverCountdownTime--;
            _countDownText.text = "GAME OVER IN: " + _gameOverCountdownTime;
            Invoke("_HandleGameOverCountdown", 1);
        }
        else
        {
			if (!_gameOver)
			{
				_HandleGameOver ();
			}
        }
    }

    void _HandleGamemodeTimerTimedout()
    {
        _timerForTimeGameMode.TimedOut -= _HandleGamemodeTimerTimedout;
        _gamemodeTimerTimedout = true;
        _HandleGameOver();
    }

    void _HandleGameOver()
    {
        //Google play service
        //leaderboard post

        PlayGamesScript.Instance.ReportScore(_playerScore);

		_gameOver = true;
        AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "Game Over", "Score", _playerScore);
        AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "Game Over", "During " + currentPhase, 1);
        AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "Game Over", "Time Played", _timePlayed);
        AnalyticsManager.Instance.LogEvent(GetGameMode(GameMode.currentGamemode), "Game Over", "Ball Amount Difference", GetDifferenceInBalls());
			
        CancelInvoke();
        //_gamePaused = true;
        _buttonShake.StopButtonShake();
        PhasesManager.Instance.StopPhasesCirculation();
        _countDownText.gameObject.SetActive(false);
        _feedbackText.gameObject.SetActive(false);
        _phaseFeedbackText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(true);
        _timerBar.Stop();
        string _gameOverString = "GAME OVER ";
        if(GameMode.currentGamemode < 2 && _gamemodeTimerTimedout)
        {
            _gameOverString = "TIME'S UP ";

			if (GameMode.currentGamemode == 0)
			{
				// handle achievement check for mode 1 completions
				AchievementsManager.Instance.IncreaseValue(AchievementsManager.ACHIEVEMENT_NAME_MODE1);

            }
			else if (GameMode.currentGamemode == 1)
			{
				// handle achievement check for mode 2 completions
				AchievementsManager.Instance.IncreaseValue(AchievementsManager.ACHIEVEMENT_NAME_MODE2);
			}
        }
        if (PlayerPrefs.HasKey("HighScore" + GameMode.currentGamemode))
        {
            if(PlayerPrefs.GetInt("HighScore" + GameMode.currentGamemode) < _playerScore)
            {
                _gameOverText.text = _gameOverString + "\n" + LanguageManager.Instance.GetLanguageText(LanguageTextIDs.NEWHIGHSCORE) + " " + _playerScore;
                PlayerPrefs.SetInt("HighScore" + GameMode.currentGamemode, _playerScore);
                PlayerPrefs.Save();
            }
            else
            {
                _gameOverText.text = _gameOverString + "\n" + LanguageManager.Instance.GetLanguageText(LanguageTextIDs.YOURSCORE) + " " + _playerScore + "\n" + LanguageManager.Instance.GetLanguageText(LanguageTextIDs.YOURHIGHSCORE) + " " + PlayerPrefs.GetInt("HighScore" + GameMode.currentGamemode);
            }
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(AudioIDs.NEW_HIGHSCORE);
            _gameOverText.text = _gameOverString + "\n" + LanguageManager.Instance.GetLanguageText(LanguageTextIDs.NEWHIGHSCORE) + " " + _playerScore;
            PlayerPrefs.SetInt("HighScore" + GameMode.currentGamemode, _playerScore);
            PlayerPrefs.Save();
        }

		// handle achievement check for highest player score
		AchievementsManager.Instance.SetValue(AchievementsManager.ACHIEVEMENT_NAME_SCORE, _playerScore);

		_timerForTimeGameMode.StopTimer();
        _retryButton.SetActive(true);
		_homeButton.SetActive (true);
        _showLeaderboardButton.SetActive(true);
        _showAchievementButton.SetActive(true);
        _gameOverOverlay.SetActive(true);
    }

    public void _HandlePhaseChange()
    {
        _phases = PhasesManager.Instance.phases;
		bool hasActivePhase = _phases.Contains(true);

        _ResetPhaseVariables();

        if (!hasActivePhase)
		{
			currentPhase = PHASES[_NOPHASE];
			ShowPhaseFeedback(0);
			return;
		}


        for (int i = 0; i < _phases.Count; i++)
        {
			if (!_phases[i])
			{
				continue;
			}

			currentPhase = PHASES[i];

			switch (i)
			{
			// Fast
			case _FASTPHASE:
				_spawnCycleDelay = _FASTPHASE_SPAWNCYCLE_DELAY;
				break;

			// Buttons swap
			case _BUTTONSWAPPHASE:
                TurnOffButtonDuringSwitch();
                Invoke("TurnOffButtonDuringSwitch", 0.3f);
                iTween.ScaleTo(_buttonHolder.gameObject, iTween.Hash("scale", new Vector3(-1, 1, 1), "time", 0.3f));
				_buttonsSwapped = true;
                //_buttonHolder.localScale = new Vector3(-1, 1, 1);
				break;

			// VariatingSizes
			case _SIZEPHASE:
				_variateBallsize = true;
				break;

			// Shapes
			case _SHAPEPHASE:
				_variateShape = true;
				break;
			
			// Ghost
			case _GHOSTPHASE:
				_ghostPhase = true;
				break;
			}
			ShowPhaseFeedback(i);
		}
    }

    void TurnOffButtonDuringSwitch()
    {
		_buttonHolderCanvasGroup.blocksRaycasts = !_buttonHolderCanvasGroup.blocksRaycasts;
    }

    /////////////////////////////////////////////// UPDATE /////////////////////////////////////////////////////

    void Update()
    {
		if (_gamePaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}

        if (!_counter.inCountdown && GameMode.currentGamemode != 3)
        {
            //Uncomment this to see ballcount in editor console
            //Debug.Log("Blue: " + _blueBalls.Count + "    Red: " + _redBalls.Count + "     White: " + _whiteBalls.Count + "     Orange: " + _orangeBalls.Count);

            if (_allBalls[_ORANGE].Count + _allBalls[_RED].Count + _allBalls[_BLUE].Count + _allBalls[_WHITE].Count > _MAX_BALLS_ALLOWED)
            {
                if (!_inGameOverCountdown)
                {
                    _inGameOverCountdown = true;
                    _gameOverCountdownTime = _GAMEOVER_COUNTDOWN_TIME;
                    _countDownText.gameObject.SetActive(true);
                    _countDownText.text = "GAME OVER IN: " + _gameOverCountdownTime;
                    Invoke("_HandleGameOverCountdown", 1);
                    CancelInvoke("_HandleSpawnCycle");
                }
                else
                {
                    if (_timerBar.running)
                    {
                        _timerBar.Stop();
                    }
                }
            }
            else
            {
                if (_inGameOverCountdown)
                {
                    _inGameOverCountdown = false;
                    CancelInvoke("_HandleGameOverCountdown");
                    _countDownText.gameObject.SetActive(false);
                    Invoke("_HandleSpawnCycle", _spawnCycleDelay);
                    _timerBar.RunForSeconds(_spawnCycleDelay);
                }
            }

        }
    }

	/////////////////////////////////////////////// HELPER METHODS /////////////////////////////////////////////////////
	private int GetDifferenceInBalls()
	{
		_highestCountofBalls = _blueBalls.Count;
		_secondHighestCountOfBalls = _blueBalls.Count;

		// loops through the balls in an array and decides which one is the highest and which one is the second highest
		_ballSorter = new int[] {_blueBalls.Count, _orangeBalls.Count, _whiteBalls.Count, _redBalls.Count};

		foreach (int i in _ballSorter)
		{
			if (i > _highestCountofBalls)
			{
				_secondHighestCountOfBalls = _highestCountofBalls;
				_highestCountofBalls = i;
			}
			else if (i > _secondHighestCountOfBalls)
			{
				_secondHighestCountOfBalls = i;
			}
		}

		return _highestCountofBalls - _secondHighestCountOfBalls;
	}

	private string GetGameMode(float curGameMode)
	{
		if (curGameMode == 0)
		{
			return "30 seconds mode";
		}
		else if (curGameMode == 1)
		{
			return "120 seconds mode";
		}
		else if (curGameMode == 2)
        {
			return "endless mode";
		}
        else
        {
            return "zen mode";
        }
    }

	private void MoveMenuLeft(GameObject menuScreen)
	{
		iTween.MoveBy (menuScreen, new Vector3 (-6, 0, 0), 1);
	}

	private void MoveMenuRight(GameObject menuScreen)
	{
		iTween.MoveBy (menuScreen, new Vector3 (6, 0, 0), 1);
	}
}
