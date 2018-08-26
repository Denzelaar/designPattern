using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : Singleton<AchievementsManager> {

	/*
	 * achievement name must be unique & small (<5 digits) for dictionary lookup
	 * achievement names must be public
	 * they are the only way through AchievementsManager.Instance to look up achievement names in other classes.
	 */
	public const string ACHIEVEMENT_NAME_ANSWERS = "answ";		// number of correct in-game answers
	public const string ACHIEVEMENT_NAME_SCORE = "scor";		// highest score achieved
	public const string ACHIEVEMENT_NAME_MODE1 = "mod1";		// number of mode 1 (30 sec) completions
	public const string ACHIEVEMENT_NAME_MODE2 = "mod2";		// number of mode 2 (120 sec) completions

	// Name of the tracked value which will determine the level of an achievement.
	const string VALUE_NAME_ANSWERS = "answ_values";
	const string VALUE_NAME_SCORE = "scor_values";
	const string VALUE_NAME_MODE1 = "mod1_values";
	const string VALUE_NAME_MODE2 = "mod2_values";

	const string ANSWERS_SPRITESHEET_LOCATION = "SpriteSheets/Answers";
	const string SCORE_SPRITESHEET_LOCATION = "SpriteSheets/Score";
	const string MODE1_SPRITESHEET_LOCATION = "SpriteSheets/Mode1";
	const string MODE2_SPRITESHEET_LOCATION = "SpriteSheets/Mode2";

	const int ANSWERS_BEGINNER = 10;
	const int ANSWERS_INTERMEDIATE = 100;
	const int ANSWERS_ADVANCED = 500;
	const string ANSWERS_DESCRIPTION = "Pick the correct color this many times: ";

	const int SCORE_BEGINNER = 1000;
	const int SCORE_INTERMEDIATE = 7500;
	const int SCORE_ADVANCED = 25000;
	const string SCORE_DESCRIPTION = "Achieve a score at least this high: ";

	const int MODE1_BEGINNER = 1;
	const int MODE1_INTERMEDIATE = 5;
	const int MODE1_ADVANCED = 15;
	const string MODE1_DESCRIPTION = "Complete the 30 second mode this many times: ";

	const int MODE2_BEGINNER = 1;
	const int MODE2_INTERMEDIATE = 3;
	const int MODE2_ADVANCED = 10;
	const string MODE2_DESCRIPTION = "Complete the 120 second mode this many times: ";

	// achievements are stored in a dictionary with name as key, and the object as value
	Dictionary<string, Achievement> _achievements;

	GameObject _descriptionMenu, _popupMenu;
	bool _isDescriptionActive, _isPopupActive;

	const float DESCRIPTION_DURATION = 5f;

	override public void Init()
	{
		_achievements = new Dictionary<string, Achievement> ();

		_PrepareAchievement(ACHIEVEMENT_NAME_ANSWERS, VALUE_NAME_ANSWERS, ANSWERS_DESCRIPTION, ANSWERS_SPRITESHEET_LOCATION, ANSWERS_BEGINNER, ANSWERS_INTERMEDIATE, ANSWERS_ADVANCED);
		_PrepareAchievement(ACHIEVEMENT_NAME_SCORE, VALUE_NAME_SCORE, SCORE_DESCRIPTION, SCORE_SPRITESHEET_LOCATION, SCORE_BEGINNER, SCORE_INTERMEDIATE, SCORE_ADVANCED);
		_PrepareAchievement(ACHIEVEMENT_NAME_MODE1, VALUE_NAME_MODE1, MODE1_DESCRIPTION, MODE1_SPRITESHEET_LOCATION, MODE1_BEGINNER, MODE1_INTERMEDIATE, MODE1_ADVANCED);
		_PrepareAchievement(ACHIEVEMENT_NAME_MODE2, VALUE_NAME_MODE2, MODE2_DESCRIPTION, MODE2_SPRITESHEET_LOCATION, MODE2_BEGINNER, MODE2_INTERMEDIATE, MODE2_ADVANCED);
	}

	/*
	 * AchievementManager.cs is initiated before Menu.cs
	 * This is why SetDescriptionMenu() is called from Menu.cs, to initialize this value after Menu.cs has initialized.
	 */
	public void SetDescriptionMenu(GameObject descriptionMenu)
	{
		_descriptionMenu = descriptionMenu;
		_isDescriptionActive = false;
	}

	/*
	 * AchievementManager.cs is initiated before BallGame.cs
	 * This is why SetPopupMenu() is called from BallGame.cs, to initialize this value after BallGame.cs has initialized.
	 */
	public void SetPopupMenu(GameObject popupMenu)
	{
		_popupMenu = popupMenu;
		_isPopupActive = false;
	}

	void _PrepareAchievement(string name, string valueName, string description, string spriteSheetLocation, int beginner, int intermediate, int advanced)
	{
		Achievement newAchievement = new Achievement ();
		newAchievement.Init (name, valueName, description, spriteSheetLocation, beginner, intermediate, advanced);
		_achievements.Add (name, newAchievement);
	}

	public void IncreaseValue(string achievementName)
	{
		_achievements [achievementName].IncreaseValue ();
	}

	public void SetValue(string achievementName, int newValue)
	{
		_achievements [achievementName].IncreaseValue (newValue);
	}

	// * * * * * * * * * * * * * GUI METHODS * * * * * * * * * * * * * //

	public void SetupAchievementContainer(Transform achievementContainer, string achievementName)
	{
		for (int i = 0; i < achievementContainer.childCount; i++)
		{
			GameObject child = achievementContainer.GetChild (i).gameObject;
			AchievementButton achievementButton = child.GetComponent<AchievementButton> ();
			achievementButton.Init (achievementName, i);
		}
	}

	public void UpdateButtons(List<GameObject> achievementButtons)
	{
		foreach (GameObject button in achievementButtons)
		{
			string name = button.GetComponent<AchievementButton> ().GetName();
			int level = button.GetComponent<AchievementButton> ().GetLevel ();
			Sprite correctSprite = _achievements [name].GetCorrectSprite (level);
			button.GetComponent<Image> ().sprite = correctSprite;
		}
	}

	public void ShowPopup (string achievementName)
	{
		if (!_isPopupActive)
		{
			Achievement achievement = _achievements [achievementName];
			int achievedLevel = achievement.GetLevelAchieved () - 1;

			Text descriptionText = _popupMenu.transform.GetComponentInChildren<Text> ();
			descriptionText.text = achievement.GetDescription (achievedLevel);
			GameObject imageObject = _popupMenu.transform.Find ("PopupImage").gameObject;
			imageObject.GetComponent<Image> ().sprite = achievement.GetCorrectSprite (achievedLevel);
			_popupMenu.GetComponent<PopupScreen>().MoveIntoScreen();
			_isPopupActive = true;
			Invoke ("_HidePopup", DESCRIPTION_DURATION);
		}
	}

	void _HidePopup()
	{
		if (_isPopupActive)
		{
			_popupMenu.GetComponent<PopupScreen> ().MoveOut ();
			_isPopupActive = false;
		}
	}

	public void ShowDescription (string achievementName, int level)
	{
		if (!_isDescriptionActive)
		{
			string description = _achievements [achievementName].GetDescription (level);
			Text descriptionText = _descriptionMenu.transform.GetComponentInChildren<Text> ();
			descriptionText.text = description;

			_descriptionMenu.GetComponent<MenuScreen> ().MoveIntoScreen ();
			_isDescriptionActive = true;
		}
	}

	public void HideDescription ()
	{
		if (_isDescriptionActive)
		{
			_descriptionMenu.GetComponent<MenuScreen> ().MoveRight ();
			_isDescriptionActive = false;
		}
	}

	/*public void Print ()
	{
		foreach (Achievement achievement in _achievements.Values)
		{
			Debug.Log(achievement.GetPrintString());
		}
	}*/
}
