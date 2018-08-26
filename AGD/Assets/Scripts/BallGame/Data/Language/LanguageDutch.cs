using System.Collections.Generic;

public class LanguageDutch : Language {

	public LanguageDutch() : base() {

	}

    /* Fills the dictionary of IDs and texts for this language */
    override public void FillDictionary()
    {
        _languageDictionary = new Dictionary<string, string>();

        _languageDictionary.Add(LanguageTextIDs.YES, "Ja");
        _languageDictionary.Add(LanguageTextIDs.NO, "Nee");
        _languageDictionary.Add(LanguageTextIDs.OK, "OK");
        _languageDictionary.Add(LanguageTextIDs.CANCEL, "Annuleren");
        _languageDictionary.Add(LanguageTextIDs.CLOSE, "Sluiten");

        // General
        _languageDictionary.Add(LanguageTextIDs.START, "Beginnen");
        _languageDictionary.Add(LanguageTextIDs.RESTART, "Opnieuw");
        _languageDictionary.Add(LanguageTextIDs.CONTINUE, "Verder");
        _languageDictionary.Add(LanguageTextIDs.BACK, "Terug");
        _languageDictionary.Add(LanguageTextIDs.LEVEL, "Niveau");
        _languageDictionary.Add(LanguageTextIDs.CORRECT, "Goed");
        _languageDictionary.Add(LanguageTextIDs.INCORRECT, "Fout");
        _languageDictionary.Add(LanguageTextIDs.SCORE, "Score:");
        _languageDictionary.Add(LanguageTextIDs.BONUS, "Tijdsbonus");
        _languageDictionary.Add(LanguageTextIDs.TOTAL, "Totaalscore");
        _languageDictionary.Add(LanguageTextIDs.HIGHSCORE, "Topscore");
        _languageDictionary.Add(LanguageTextIDs.QUIT, "Stoppen");
        _languageDictionary.Add(LanguageTextIDs.MULTIPLIER, "Bonus");


        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS1, "GOED ZO");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS2, "COOL");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS3, "WOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS4, "KAPOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS5, "PRACHTIG");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS6, "GENIAAL");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG1, "OEPS");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG2, "FOUT");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG3, "INCORRECT");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG4, "OH NEE");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG5, "IDIOOT");

        //Phase
        _languageDictionary.Add(LanguageTextIDs.PHASESTART, "PHASE BEGONNEN");
        _languageDictionary.Add(LanguageTextIDs.PHASEEND, "PHASE GEINDIGD");

        //Tutorial
        _languageDictionary.Add(LanguageTextIDs.TUTORIALTEXT, "Klik op de meest voorkomende kleur");
		_languageDictionary.Add(LanguageTextIDs.TUTORIALPART2TEXT, "Als deze balk vol is, verschijnen nieuwe ballen. Klik er op om de ballen direct te krijgen.");

        //SCORE
        _languageDictionary.Add(LanguageTextIDs.NEWHIGHSCORE, "JOUW NIEUWE HIGHSCORE IS:");
        _languageDictionary.Add(LanguageTextIDs.YOURHIGHSCORE, "JOUW HIGHSCORE IS:");
        _languageDictionary.Add(LanguageTextIDs.YOURSCORE, "JOUW SCORE IS:");

        //GAMEMODES
        _languageDictionary.Add(LanguageTextIDs.SEC30, "30 Seconden");
        _languageDictionary.Add(LanguageTextIDs.SEC120, "120 Seconden");
        _languageDictionary.Add(LanguageTextIDs.ENDLESS, "Eindeloos");
        _languageDictionary.Add(LanguageTextIDs.ZEN, "Zen");

        //MAIN MENU
        _languageDictionary.Add(LanguageTextIDs.EXITGAME, "Afsluiten");
        _languageDictionary.Add(LanguageTextIDs.SOUNDS, "Geluiden");
        _languageDictionary.Add(LanguageTextIDs.LANGUAGEBUTTON, "Taal");

    }

}
