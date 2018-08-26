using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour {

	void Start () {

        // Initialize other managers
        Application.targetFrameRate = 40;
        AudioManager.Instance.Init();
        ResourcesManager.Instance.Init();
        PreferencesManager.Instance.Init();
		AchievementsManager.Instance.Init();
        LanguageManager.Instance.Init();
        GameManager.Instance.Init();
        PhasesManager.Instance.Init();
		AnalyticsManager.Instance.Init();
	}
}
