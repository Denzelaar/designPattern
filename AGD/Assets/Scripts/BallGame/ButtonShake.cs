using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShake : MonoBehaviour {

    GameObject _button1, _button2, _button3, _button4;
    Vector3 _positionShake = new Vector3(0, .5f, 0);
    Vector3 _orangeButtonPos, _redButtonPos, _blueButtonPos, _whiteButtonPos;
    float _animationDuration = 0.1f;
    bool _gameOver = false;

    public bool GameOver
    {
        set
        {
            this._gameOver = value;
        }
    }
    // Use this for initialization
    public void Init () {
        _button1 = transform.Find("TopRow/OrangeButton").gameObject;
        _button2 = transform.Find("TopRow/RedButton").gameObject;
        _button3 = transform.Find("BottomRow/BlueButton").gameObject;
        _button4 = transform.Find("BottomRow/WhiteButton").gameObject;

        _orangeButtonPos = _button1.transform.position;
        _redButtonPos = _button2.transform.position;
        _blueButtonPos = _button3.transform.position;
        _whiteButtonPos = _button4.transform.position;

    }

    public void ShakeButton(int buttonNumber)
    {
        if(_gameOver == false)
        {
            switch (buttonNumber)
            {
                case 0:
                    iTween.ShakePosition(_button1, _positionShake, _animationDuration);
                    return;
                case 1:
                    iTween.ShakePosition(_button2, _positionShake, _animationDuration);
                    return;
                case 2:
                    iTween.ShakePosition(_button3, _positionShake, _animationDuration);
                    return;
                case 3:
                    iTween.ShakePosition(_button4, _positionShake, _animationDuration);
                    return;
            }
        }       
    }

    public void StopButtonShake()
    {
        _gameOver = true;
        Destroy(_button1.GetComponent("I Tween"));
        iTween.Stop(_button1);
        _button1.transform.position = _orangeButtonPos;

        Destroy(_button2.GetComponent("I Tween"));
        iTween.Stop(_button2);
        _button2.transform.position = _redButtonPos;

        Destroy(_button3.GetComponent("I Tween"));
        iTween.Stop(_button3);
        _button3.transform.position = _blueButtonPos;

        Destroy(_button4.GetComponent("I Tween"));
        iTween.Stop(_button4);
        _button4.transform.position = _whiteButtonPos;


    }
}
