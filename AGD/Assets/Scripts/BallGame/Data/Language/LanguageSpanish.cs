using System.Collections.Generic;

public class LanguageSpanish : Language
{

    public LanguageSpanish() : base()
    {

    }

    /* Fills the dictionary of IDs and texts for this language */
    override public void FillDictionary()
    {
        _languageDictionary = new Dictionary<string, string>();

        _languageDictionary.Add(LanguageTextIDs.YES, "Si");
        _languageDictionary.Add(LanguageTextIDs.NO, "No");
        _languageDictionary.Add(LanguageTextIDs.OK, "OK");
        _languageDictionary.Add(LanguageTextIDs.CANCEL, "Cancelar");
        _languageDictionary.Add(LanguageTextIDs.CLOSE, "Close");

        // General
        _languageDictionary.Add(LanguageTextIDs.START, "Comienzo");
        _languageDictionary.Add(LanguageTextIDs.RESTART, "Reiniciar");
        _languageDictionary.Add(LanguageTextIDs.CONTINUE, "Continuar");
        _languageDictionary.Add(LanguageTextIDs.BACK, "Espalda");
        _languageDictionary.Add(LanguageTextIDs.LEVEL, "Nivel");
        _languageDictionary.Add(LanguageTextIDs.CORRECT, "Correcto");
        _languageDictionary.Add(LanguageTextIDs.INCORRECT, "Incorrecto");
        _languageDictionary.Add(LanguageTextIDs.SCORE, "Score:");
        _languageDictionary.Add(LanguageTextIDs.BONUS, "Bonus");
        _languageDictionary.Add(LanguageTextIDs.TOTAL, "Total");
        _languageDictionary.Add(LanguageTextIDs.HIGHSCORE, "Highscore");
        _languageDictionary.Add(LanguageTextIDs.QUIT, "Bastante");
        _languageDictionary.Add(LanguageTextIDs.MULTIPLIER, "Multiplicador");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS1, "MUY BIEN");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS2, "INCREÍBLE");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS3, "WOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS4, "KAPOW");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS5, "FABULOSO");
        _languageDictionary.Add(LanguageTextIDs.FEEDPOS6, "GENIO");

        // FeedBack Positive
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG1, "Uy");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG2, "GODER");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG3, "INCORRECTO");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG4, "OH NO");
        _languageDictionary.Add(LanguageTextIDs.FEEDNEG5, "IDIOTA");

        //Phase
        _languageDictionary.Add(LanguageTextIDs.PHASESTART, "FASE COMENZÓ");
        _languageDictionary.Add(LanguageTextIDs.PHASEEND, "FASE TERMINADA");

        //Tutorial
        _languageDictionary.Add(LanguageTextIDs.TUTORIALTEXT, "Pulsa el color de la bola más común en el campo de juego");
		_languageDictionary.Add (LanguageTextIDs.TUTORIALPART2TEXT, "Cuando esta barra está llena, las bolas aparecen. Pulsa para generar bolas al instante");

        //SCORE
        _languageDictionary.Add(LanguageTextIDs.NEWHIGHSCORE, "TU NUEVA PUNTUACIÓN MÁS ALTA ES:");
        _languageDictionary.Add(LanguageTextIDs.YOURHIGHSCORE, "TU PUNTUACIÓN MÁS ALTA ES:");
        _languageDictionary.Add(LanguageTextIDs.YOURSCORE, "TU PUNTUACION ES:");

        //GAMEMODES
        _languageDictionary.Add(LanguageTextIDs.SEC30, "30 Segundos");
        _languageDictionary.Add(LanguageTextIDs.SEC120, "120 Segundos");
        _languageDictionary.Add(LanguageTextIDs.ENDLESS, "Interminable");
        _languageDictionary.Add(LanguageTextIDs.ZEN, "Zen");

        //MAIN MENU
        _languageDictionary.Add(LanguageTextIDs.EXITGAME, "Salir del juego");
        _languageDictionary.Add(LanguageTextIDs.SOUNDS, "Sonidos");
        _languageDictionary.Add(LanguageTextIDs.LANGUAGEBUTTON, "Lenguaje");

    }

}
