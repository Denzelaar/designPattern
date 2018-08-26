using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLanguagesCommand : MonoBehaviour {

    static public void Execute()
    {
        LanguageManager.Instance.RegisterLanguage(SystemLanguage.Dutch.ToString(), new LanguageDutch());
    }
}
