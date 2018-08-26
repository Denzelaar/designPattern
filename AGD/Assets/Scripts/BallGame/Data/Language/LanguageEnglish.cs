using System.Collections.Generic;

public class LanguageEnglish : Language {

	public LanguageEnglish() : base() {

	}

    /* Fills the dictionary of IDs and texts for this language */
    override public void FillDictionary()
    {
        _languageDictionary = new Dictionary<string, string>();

        _languageDictionary.Add(LanguageTextIDs.YES, "Yes");
        _languageDictionary.Add(LanguageTextIDs.NO, "No");
        _languageDictionary.Add(LanguageTextIDs.OK, "OK");
        _languageDictionary.Add(LanguageTextIDs.CANCEL, "Cancel");
        _languageDictionary.Add(LanguageTextIDs.CLOSE, "Close");

        // General
        _languageDictionary.Add(LanguageTextIDs.START, "Start");
        _languageDictionary.Add(LanguageTextIDs.RESTART, "Restart");
        _languageDictionary.Add(LanguageTextIDs.CONTINUE, "Continue");
        _languageDictionary.Add(LanguageTextIDs.BACK, "Back");
        _languageDictionary.Add(LanguageTextIDs.LEVEL, "Level");
        _languageDictionary.Add(LanguageTextIDs.CORRECT, "Correct");
        _languageDictionary.Add(LanguageTextIDs.INCORRECT, "Incorrect");
        _languageDictionary.Add(LanguageTextIDs.SCORE, "Score:");
        _languageDictionary.Add(LanguageTextIDs.BONUS, "Bonus");
        _languageDictionary.Add(LanguageTextIDs.TOTAL, "Total");
        _languageDictionary.Add(LanguageTextIDs.HIGHSCORE, "Highscore");
        _languageDictionary.Add(LanguageTextIDs.QUIT, "Quit");
        _languageDictionary.Add(LanguageTextIDs.MULTIPLIER, "Multiplier");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS1, "GREAT");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS2, "AWESOME");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS3, "WOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS4, "KAPOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS5, "FABULOUS");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS6, "GENIUS");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG1, "OOPS");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG2, "WRONG");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG3, "INCORRECT");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG4, "OH NO");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG5, "IDIOT");

        //Phase
        _languageDictionary.Add(LanguageTextIDs.PHASESTART, "PHASE STARTED");
        _languageDictionary.Add(LanguageTextIDs.PHASEEND, "PHASE ENDED");

        //Tutorial
        _languageDictionary.Add(LanguageTextIDs.TUTORIALTEXT, "Tap the most common ball color in the playing field");
		_languageDictionary.Add(LanguageTextIDs.TUTORIALPART2TEXT, "When this bar is full, balls spawn. Tap it to instantly spawn balls");

        //SCORE
        _languageDictionary.Add(LanguageTextIDs.NEWHIGHSCORE, "YOUR NEW HIGHSCORE IS:");
        _languageDictionary.Add(LanguageTextIDs.YOURHIGHSCORE, "YOUR HIGHSCORE IS:");
        _languageDictionary.Add(LanguageTextIDs.YOURSCORE, "YOUR SCORE IS:");

        //GAMEMODES
        _languageDictionary.Add(LanguageTextIDs.SEC30, "30 Seconds");
        _languageDictionary.Add(LanguageTextIDs.SEC120, "120 Seconds");
        _languageDictionary.Add(LanguageTextIDs.ENDLESS, "Endless");
        _languageDictionary.Add(LanguageTextIDs.ZEN, "Zen");

        //MAIN MENU
        _languageDictionary.Add(LanguageTextIDs.EXITGAME, "Exit game");
        _languageDictionary.Add(LanguageTextIDs.SOUNDS, "Sounds");
        _languageDictionary.Add(LanguageTextIDs.LANGUAGEBUTTON, "Language");

    }
}
