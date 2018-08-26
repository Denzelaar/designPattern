using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : Singleton<AnalyticsManager>{

	public GoogleAnalyticsV4 _googleAnalytics;
	// Use this for initialization
	override public void Init () 
	{
		_googleAnalytics.StartSession();
		_googleAnalytics.LogScreen("Main Menu Screen");
	}
	
	// Update is called once per frame
	public void LogScreen(string screenToLog)
	{
		_googleAnalytics.LogScreen(screenToLog);
	}

	public void LogButton(string buttonToLog)
	{
		_googleAnalytics.LogScreen(buttonToLog);
	}

	public void LogEvent(string eventCategory, string eventAction, string eventLabel, int eventValue)
	{
		_googleAnalytics.LogEvent(eventCategory, eventAction, eventLabel, eventValue);
    }
}
