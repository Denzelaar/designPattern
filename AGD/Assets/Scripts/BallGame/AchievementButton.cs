using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	string _achievementName;
	int _achievementLevel;

	GameObject _button;

	public void Init(string name, int level)
	{
		_achievementName = name;
		_achievementLevel = level;

		_button = this.gameObject;
		_button.GetComponent<Button> ().onClick.AddListener (delegate { ButtonClicked (_achievementLevel); });
	}

	public string GetName()
	{
		return _achievementName;
	}

	public int GetLevel()
	{
		return _achievementLevel;
	}

	public void ButtonClicked(int level)
	{
		AchievementsManager.Instance.ShowDescription (_achievementName, level);
	}

	public void Print()
	{
		Debug.Log ("name = " + _achievementName + ", level = " + _achievementLevel);
	}
}
