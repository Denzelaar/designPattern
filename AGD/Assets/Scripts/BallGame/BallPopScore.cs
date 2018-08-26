using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPopScore : MonoBehaviour {

    Transform _scoreText;
    public Color32 ballColor;
    bool _initialized = false;

    // Use this for initialization
    void _Init ()
    {
        _scoreText = GameObject.FindGameObjectWithTag("ScoreText").transform;
        gameObject.GetComponent<Text>().color = ballColor;
        _initialized = true;
    }

    public void StartAnimation(int score, float delay)
    {
        if (!_initialized)
        {
            _Init();
        }

        transform.GetComponent<Text>().text = "+" + score.ToString();
        gameObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 0.2f, "delay", delay, "easetype", iTween.EaseType.easeOutBack));
        Invoke("FlyToScoreText", 0.2f+ delay);
    }

    void FlyToScoreText()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", _scoreText.transform.position, "time", 1f, "easetype", iTween.EaseType.easeInBack));
        Invoke("DestroyText", 1f);
    }

    void DestroyText()
    {
        //Destroy(gameObject);
        ResourcesManager.Instance.RemoveResourceInstance(gameObject);
    }
}
