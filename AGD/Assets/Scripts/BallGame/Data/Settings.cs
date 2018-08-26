using System;
using UnityEngine;

public class Settings {

    const string _AUDIO_ENABLED_ID = "AudioEnabled";


    // TODO: Base this on actual settings
    static public bool audioEnabled
    {
        get
        {
            return PlayerPrefs.GetInt(_AUDIO_ENABLED_ID, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(_AUDIO_ENABLED_ID, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
