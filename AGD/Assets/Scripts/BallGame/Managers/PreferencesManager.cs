using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesManager : Singleton<PreferencesManager> {

	Sprite[] _soundSprites, _screenshakeSprites, _gyroscopeSprites;

	override public void Init()
	{
		SetInitialPreference("gyroscope", false);
		SetInitialPreference("screenshake", true);
		SetInitialPreference("sound", true);
		SetInitialPreference("tutorial", false);

		_soundSprites = Resources.LoadAll<Sprite>("SpriteSheets/Sound");
		_gyroscopeSprites = Resources.LoadAll<Sprite> ("SpriteSheets/Gyroscope");
		_screenshakeSprites = Resources.LoadAll<Sprite> ("SpriteSheets/Screenshake");
	}

	/*
	 * True/False preference values are saved as integers, since boolean values can't be used.
	 * "0" == OFF
	 * "1" == ON
	 */
	public void SetInitialPreference(string preference, bool value)
	{
		if (!PlayerPrefs.HasKey (preference))
		{
			int preferenceNumber = System.Convert.ToInt32 (value);
			PlayerPrefs.SetInt (preference, preferenceNumber);
			PlayerPrefs.Save();
		}
	}

	public void SetInitialValue(string name, int value)
	{
		if (!PlayerPrefs.HasKey (name))
		{
			PlayerPrefs.SetInt (name, value);
			PlayerPrefs.Save();
		}
	}

	public void IncreaseValue(string name)
	{
		int newValue = PlayerPrefs.GetInt (name) + 1;
		PlayerPrefs.SetInt (name, newValue);
		PlayerPrefs.Save();
	}

	public void SetValue(string name, int value)
	{
		if (value > PlayerPrefs.GetInt(name))
		{
			PlayerPrefs.SetInt(name, value);
			PlayerPrefs.Save();
		}
	}

	public int Get(string name)
	{
		return PlayerPrefs.GetInt (name);
	}
		
	public void ChangePlayerPreference(string preference)
	{
		PlayerPrefs.SetInt (preference, NegativeOf (PlayerPrefs.GetInt (preference)));
		PlayerPrefs.Save();
	}

	public void UpdateButton(string preference, GameObject button)
	{
		// string preferenceStatus = StringOf (PlayerPrefs.GetInt (preference));
		// this.transform.Find (preference + "Button").GetComponentInChildren<Text> ().text = preference + "=" + preferenceStatus;

		//TODO: refactor this switch case away, by associating the spritesheets with the buttons instead
		int preferenceStatus = PlayerPrefs.GetInt (preference);
		switch (preference) {
		case ("sound"):
			button.GetComponent<Image> ().sprite = _soundSprites [preferenceStatus];
			break;
		case ("gyroscope"):
			button.GetComponent<Image> ().sprite = _gyroscopeSprites [preferenceStatus];
			break;
		case ("screenshake"):
			button.GetComponent<Image> ().sprite = _screenshakeSprites [preferenceStatus];
			break;
		default:
			break;
		}
	}
		
	// BEHOLD THE SPAGHETTI MONSTER
	/*public void UpdateAchievementButton(int achievementNumber, GameObject button)
	{
		int achieved = PlayerPrefs.GetInt ("achievement_" + achievementNumber);
		int spriteNumber = (2 * achievementNumber + achieved) % 6;

		if (achievementNumber <= 2)
		{
			button.GetComponent<Image> ().sprite = _answerSprites [spriteNumber];
		}
		else if (achievementNumber >= 3 && achievementNumber <= 5)
		{
			button.GetComponent<Image> ().sprite = _scoreSprites [spriteNumber];
		}
		else if (achievementNumber >= 6 && achievementNumber <= 8)
		{
			button.GetComponent<Image> ().sprite = _mode1Sprites [spriteNumber];
		}
		else if (achievementNumber >= 9 && achievementNumber <= 10)
		{
			button.GetComponent<Image> ().sprite = _mode2Sprites [spriteNumber];
		}
	}*/

	private string StringOf(int num)
	{
		if (num == 1) {
			return "ON";
		}
		return "OFF";
	}
	private int NegativeOf(int num)
	{
		if (num == 1) {
			return 0;
		}
		return 1;
	}
}
