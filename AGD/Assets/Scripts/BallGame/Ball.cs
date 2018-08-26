using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    bool _initialized = false;

    bool _removedByPowerup = false;
	byte ghost = 100;
	byte notGhost =255;

	BallGame ballgame;

    Image _ballImage;

    [SerializeField]
    Color32 color1;
    [SerializeField]
    Color32 color2;
    [SerializeField]
    Color32 color3;
    [SerializeField]
    Color32 color4;

    Color32 _currentColor;

    const float _ANIMATION_DURATION = 0.3f;
    const float _DESTROY_DELAY = 1f;
    bool _gameOver;

    GameObject _particleSystem;

    void _Init()
    {	
		_ballImage = transform.GetComponent<Image> ();
	    _initialized = true;
    }

	public void SetColorAndPosition(int colorNumber, float x, float y, float size, bool oddShape = false, bool ghostPhase = false)
    {
        if (!_initialized)
        {
            _Init();
        }

		if (!ghostPhase)
		{
			switch (colorNumber)
			{
			case 0:
				color4.a = (byte)notGhost;
				_currentColor = color1;
				break;
			case 1:
				color4.a = (byte)notGhost;
				_currentColor = color2;
				break;
			case 2:
				color4.a = (byte)notGhost;
				_currentColor = color3;
				break;
			case 3:
				color4.a = (byte)notGhost;
				_currentColor = color4;
				break;
			}
		}
		else if (ghostPhase)
		{
			int rando = Random.Range(0, 3);

			if (rando == 0)
			{
				switch (colorNumber)
				{
				case 0:
					color1.a = (byte)ghost;
					_currentColor = color1;
					break;
				case 1:
					color2.a = (byte)ghost;
					_currentColor = color2;
					break;
				case 2:
					color3.a = (byte)ghost;
					_currentColor = color3;
					break;
				case 3:
					color4.a = (byte)ghost;
					_currentColor = color4;
					break;
				}
			}
			else
			{
				switch (colorNumber)
				{
				case 0:
					color4.a = (byte)notGhost;
					_currentColor = color1;
					break;
				case 1:
					color4.a = (byte)notGhost;
					_currentColor = color2;
					break;
				case 2:
					color4.a = (byte)notGhost;
					_currentColor = color3;
					break;
				case 3:
					color4.a = (byte)notGhost;
					_currentColor = color4;
					break;
				}
			}
		}

        _ballImage.color = _currentColor;

        transform.localPosition = new Vector2(x, y);
        transform.localScale = Vector3.one;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
		if (gameObject.tag == ("Ball"))
		{
			transform.GetComponent<CircleCollider2D> ().radius = size / 2;
		}

		
    }

    public void SetRemovedByPowerup()
    {
        gameObject.SetActive(false);
        _removedByPowerup = true;
    }


    public void Explode(bool gameOver, int score = 0, float delay = 0)
    {
        _gameOver = gameOver;
        //TODO: Start hide animation
        if (gameOver == false && score != 0)
        {
            GameObject _ballScoreHolder = transform.parent.parent.parent.Find("BallScoreHolder").gameObject;
            GameObject _scoreText = ResourcesManager.Instance.GetResourceInstance("BallGame/BallScoreText").gameObject;
            _scoreText.transform.SetParent(_ballScoreHolder.transform);
            _scoreText.GetComponent<Text>().text = "+" + score.ToString();
            _scoreText.GetComponent<BallPopScore>().ballColor = _currentColor;
            _scoreText.transform.position = transform.position;
            _scoreText.GetComponent<BallPopScore>().StartAnimation(score, delay);
        }

            
        Invoke("_StartHideAnimation", delay);
    }

    void _StartHideAnimation()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", _ANIMATION_DURATION));
        Invoke("_DestroyBall", _DESTROY_DELAY);

        if (!_gameOver)
        {
            int _random = Random.Range(0, 2);
            if (_random == 1 && GameMode.currentGamemode != 3)
            {
                AudioManager.Instance.PlaySoundEffect(AudioIDs.BALL_POP);
            }
        }    
    }

    void _DestroyBall()
    {
        iTween.Stop(gameObject);
        //Destroy(gameObject);
        ResourcesManager.Instance.RemoveResourceInstance(gameObject);
    }

    public bool RemovedByPowerup
    {
        get { return _removedByPowerup; }
    }
}
