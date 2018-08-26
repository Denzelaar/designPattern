using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesScript : Singleton<PlayGamesScript>
{

    public GameObject googlePlayButton, leaderboardButton, achievementsButton;

    //Login


  /*  void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        OnConnectClick();
    }*/

    public void OnConnectClick()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
			{
				googlePlayButton.SetActive(false);
                UnlockAchievement(SCCGPIds.achievement_login);
            }
        });
    }

 

    public void OnAchievementClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void OnLeaderBoardClick()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100.0f, (bool succes) =>
        {
            //Debug.Log("Achievement Unlocked" + succes.ToString());
        });
    }

    public void ReportScore(int score)
    {
        Social.ReportScore(score, SCCGPIds.leaderboard_highscores, (bool succes) => {
            //Debug.Log("Report score to leaderboard" + succes.ToString());
        });
    }
}


/*
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
	}

    void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    #region Achievements    
    public static void UnlockAchievements(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
        Debug.Log("ShowAchievements");
    }

    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
        Debug.Log("ShowLeaderboards");
    }
    #endregion /Leaderboard
}

*/
