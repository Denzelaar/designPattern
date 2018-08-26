using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeTutorialImage : MonoBehaviour
{
	const float _FADE_TIME = 1f;

    private Image _image;
    BallGame _ballGame;
    private Vector3 _orangeButtonPos, _redButtonPos, _whiteButtonPos, _blueButtonPos;

    public void Init()
    {
        _ballGame = transform.parent.GetComponent<BallGame>();
        this._image = this.GetComponent<Image>();
    }


	IEnumerator Fade()
	{
		while (_image.color.a > 0)
		{
			Color tempColor = _image.color;
			tempColor.a -= Time.deltaTime / _FADE_TIME;
			_image.color = tempColor;
			yield return null;
		}

        if(_image.color.a <= 0)
        {
            gameObject.SetActive(false);
        }
	}

	public void MoveToTimerBar()
	{
		float newX = _ballGame.transform.GetChild (1).transform.position.x;
		float newY = _ballGame.transform.GetChild (1).transform.position.y - 0.4f;
		this.transform.position = new Vector3 (newX, newY);
	}

	public void MoveToCorrectButton()
	{
        gameObject.SetActive(true);
        if (_ballGame.GetMajorityBalls() == "Orange")
		{
			this.transform.position = _ballGame.transform.GetChild(2).GetChild(0).GetChild(0).transform.position;
			_ballGame.transform.GetChild (2).GetChild (0).GetChild (0).GetComponent<Button> ().interactable = true;
		}
		if (_ballGame.GetMajorityBalls() == "Red")
		{
			this.transform.position = _ballGame.transform.GetChild(2).GetChild(0).GetChild(1).transform.position;
			_ballGame.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<Button> ().interactable = true;
		}
		if(_ballGame.GetMajorityBalls() == "White")
		{
			this.transform.position = _ballGame.transform.GetChild(2).GetChild(1).GetChild(1).transform.position;
			_ballGame.transform.GetChild (2).GetChild (1).GetChild (1).GetComponent<Button> ().interactable = true;
		}
		if (_ballGame.GetMajorityBalls() == "Blue")
		{
			this.transform.position = _ballGame.transform.GetChild(2).GetChild(1).GetChild(0).transform.position;
			_ballGame.transform.GetChild (2).GetChild (1).GetChild (0).GetComponent<Button> ().interactable = true;
		}
	}

	public void FadeOut()
	{
		StartCoroutine ("Fade");
	}
}