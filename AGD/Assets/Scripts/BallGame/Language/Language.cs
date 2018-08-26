using System.Collections.Generic;

public class Language {

	static protected Dictionary<string, string> _languageDictionary;

	public Language() {
		_languageDictionary = null;
	}

	/* Fills the dictionary of IDs and texts for this language */
	virtual public void FillDictionary() {
		_languageDictionary = new Dictionary<string, string>();
	}

	/* Clears the dictionary of IDs and texts for this language */
	public void ClearDictionary() {
		_languageDictionary = null;
	}

	/* Returns the text for the specified ID in this language */
	public string GetLanguageText(string languageTextID) {
		return _languageDictionary[languageTextID];
	}

	public bool HasLanguageTextID(string languageTextID) {
		return _languageDictionary.ContainsKey(languageTextID);
	}
}
