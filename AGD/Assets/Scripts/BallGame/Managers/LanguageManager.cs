using UnityEngine;
using System.Collections.Generic;

public class LanguageManager : Singleton<LanguageManager> {

	const string _CURRENT_LANGUAGE_SAVE_ID = "CurrentLanguage";

	/* Language events */
	public delegate void LanguageEvent();
	public event LanguageEvent OnLanguageChanged;

	Dictionary<string, Language> _languages;

	string _currentLanguageName;
	Language _currentLanguage;

	override public void Init() {
		_currentLanguageName = PlayerPrefs.GetString(_CURRENT_LANGUAGE_SAVE_ID, "");
		_currentLanguage = null;

		_languages = new Dictionary<string, Language>();
	}

	/* Register a language name and Language pair */
	public void RegisterLanguage(string languageName, Language language) {
		_languages.Add(languageName, language);
	}

	/* Returns whether the specified language is available */
	public bool HasLanguage(string languageName) {
		return _languages.ContainsKey(languageName);
	}

	/* Determines and sets an initial Language */
	public void DetermineAndSetInitialLanguage() {
		if (string.IsNullOrEmpty(LanguageManager.Instance.currentLanguageName)) {
			string systemLanguage = Application.systemLanguage.ToString();
			if (_languages.ContainsKey(systemLanguage)) {
				SetCurrentLanguage(systemLanguage, true);
			} else {
				SetCurrentLanguage(SystemLanguage.English.ToString(), true);
			}
		} else {
			SetCurrentLanguage(LanguageManager.Instance.currentLanguageName, true);
		}
	}

	/* Sets the specified language as the current language */
	public void SetCurrentLanguage(string languageName, bool forceReset = false) {
		if (_currentLanguageName != languageName || forceReset) {
			if (_currentLanguage != null) {
				_currentLanguage.ClearDictionary();
			}

			_currentLanguageName = languageName;
			_currentLanguage = _languages[languageName];

			_currentLanguage.FillDictionary();

			PlayerPrefs.SetString(_CURRENT_LANGUAGE_SAVE_ID, _currentLanguageName);
			PlayerPrefs.Save();

			if (OnLanguageChanged != null) {
				OnLanguageChanged();
			}
		}
	}

	/* Returns the text for the specified ID in the current language */
	public string GetLanguageText(string languageTextID) {
		return _currentLanguage.GetLanguageText(languageTextID);
	}

	public bool HasLanguageTextID(string languageTextID) {
		return _currentLanguage.HasLanguageTextID(languageTextID);
	}

	/********************************* Getters/Setters *********************************/
	
	public string currentLanguageName {
		get {
			return _currentLanguageName;
		}
	}

	public List<string> languageNames {
		get {
			return new List<string>(_languages.Keys);
		}
	}

}
