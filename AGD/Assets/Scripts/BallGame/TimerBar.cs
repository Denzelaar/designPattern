using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour {

    bool _initialized = false;
    bool _running = false;
    GameObject _fill;
    float _minFill;
    float _time;
    Vector3 _fullFillPos;
    Vector3 _emptyFillPos;
    float currentAmountOfSeconds;

    void _Init()
    {
        _fill = transform.Find("Background/Fill").gameObject;
        _fullFillPos = _fill.transform.position;
        _emptyFillPos = _fullFillPos - new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, _fullFillPos.y, _fullFillPos.z);
        _ResetPositions();
        _fill.SetActive(true);
        _initialized = true;
    }

    public void RunForSeconds(float amountOfSeconds)
    {
        currentAmountOfSeconds = amountOfSeconds;

        if (!_initialized)
        {
            _Init();
        }

        if (_running)
        {
            iTween.Stop(_fill);
            Invoke("_Continue", 0.01f);
        }
        else
        {
            _Continue();
        }
    }

    void _Continue()
    {
        _ResetPositions();
        _StartAnimation();
    }

    void _ResetPositions()
    {
        _fill.transform.localPosition = _emptyFillPos;
    }

    void _StartAnimation()
    {
        iTween.MoveTo(_fill, iTween.Hash("position", _fullFillPos, "islocal", true, "oncomplete", "_OnAnimationEnded", "oncompletetarget", gameObject, "time", currentAmountOfSeconds, "easetype", iTween.EaseType.linear));
        _running = true;
    }

    void _OnAnimationEnded()
    {
        _fill.transform.localPosition = _fullFillPos;
        _running = false;
    }

    public void Stop()
    {
        iTween.Stop(_fill);
        _ResetPositions();
    }

    public bool running
    {
        get
        {
            return _running;
        }
    }
}
