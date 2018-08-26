using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class contains all the code dealing with getting, setting and comparing achievement data in PlayerPrefs. 
 */

public class Achievement : MonoBehaviour {

	string _trackedValueName;
	string _achievementName;
	string _description;

	/* 
	 * Each achievement has three levels, each represented by an integer.
	 * 1 = beginner, 2 = intermediate, 3 = advanced.
	 * 0 == not achieved, 1 == 1st level achieved, 2 == 2nd level achieved, 3 == final level achieved.
	 */
	int _levelAchieved;
	int _tresholdBeginner, _tresholdIntermediate, _tresholdAdvanced;
	int _currentValue;

	Sprite[] _sprites;

	public void Init(string name, string valueName, string description, string spriteSheetLocation, int beginner, int intermediate, int advanced)
	{
		_achievementName = name;
		_trackedValueName = valueName;
		_description = description;

		PreferencesManager.Instance.SetInitialValue(_achievementName, 0);
		PreferencesManager.Instance.SetInitialValue(_trackedValueName, 0);
		_levelAchieved = PreferencesManager.Instance.Get (_achievementName);
		_currentValue = PreferencesManager.Instance.Get (_trackedValueName);


		_sprites = Resources.LoadAll<Sprite> (spriteSheetLocation);

		_tresholdBeginner = beginner;
		_tresholdIntermediate = intermediate;
		_tresholdAdvanced = advanced;
	}
		
	public void IncreaseValue()
	{
		PreferencesManager.Instance.IncreaseValue (_trackedValueName);
		_currentValue++;
		_CheckLevel (); 
	}

	public void IncreaseValue( int newValue)
	{
		PreferencesManager.Instance.SetValue (_trackedValueName, newValue);
		_currentValue = newValue;
		_CheckLevel ();
	}

	/* 
	 * Updates the achievement level.
	 * This method will automatically be called when an achievement's tracked value updates.
	 * 0 = not achieved, 1 = beginner, 2 = intermediate, 3 = advanced.
	 */
	void _CheckLevel()
	{
		if (_levelAchieved < 1 && _currentValue >= _tresholdBeginner)
		{
			_SetLevelAchieved (1);
            if (_achievementName == "mod1")
            {
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_30);
            }
            if(_achievementName == "mod2")
                   PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_120);
            if (_achievementName == "answ")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_10_times);
        }
		if (_levelAchieved < 2 && _currentValue >= _tresholdIntermediate)
		{
			_SetLevelAchieved (2);
            if (_achievementName == "mod1")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_30_five);
            if (_achievementName == "mod2")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_120_three);
            if (_achievementName == "answ")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_100_times);
        }
		if (_levelAchieved < 3 && _currentValue >= _tresholdAdvanced)
		{
			_SetLevelAchieved (3);
            if (_achievementName == "mod1")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_30_fifteen);
            if (_achievementName == "mod2")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_play_120_ten);
            if (_achievementName == "answ")
                PlayGamesScript.Instance.UnlockAchievement(SCCGPIds.achievement_500_times);
        }


    }
		
	void _SetLevelAchieved(int level)
	{
		PreferencesManager.Instance.SetValue (_achievementName, level);
		_levelAchieved = level;
		AchievementsManager.Instance.ShowPopup (_achievementName);
	}
		

	public int GetLevelAchieved()
	{
		return _levelAchieved;
	}

	/* 
	 * six options:
	 * level = 0			return 0
	 * level = 0, achieved	return 1
	 * level = 1			return 2
	 * level = 1, achieved	return 3
	 * level = 2			return 4
	 * level = 2, achieved	return 5
	 */
	public Sprite GetCorrectSprite(int level)
	{
		int correctSpriteIndex = level * 2;

		if (_levelAchieved > level)
		{
			correctSpriteIndex++;
		}
		return _sprites[correctSpriteIndex];
	}
		
	public string GetDescription (int level)
	{
		int treshold = 0;
		switch (level)
		{
		case 0:
			treshold = _tresholdBeginner;
			break;
		case 1:
			treshold = _tresholdIntermediate;
			break;
		case 2:
			treshold = _tresholdAdvanced;
			break;
		default:
			break;
		}
		return _description + treshold;
	}
}
