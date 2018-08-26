using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextAnimations : MonoBehaviour {

    Vector3 _startPos, _startScale;
    Quaternion _startRotation;
    Image _canvasBackGroundImage;
    Color _originalCanvasColor;
    Color _correctColor;
    Color _incorrectColor;

	// Use this for initialization
	public void Init() {
        _startPos = transform.position;
        _startRotation = transform.rotation;
        _startScale = transform.localScale;
        _canvasBackGroundImage = transform.parent.GetComponentInParent<Image>();
        _originalCanvasColor = _canvasBackGroundImage.color;
        _correctColor = new Color32(32, 210, 78, 255);
        _incorrectColor = new Color32(148, 0, 0, 255);
    }

    public void RandomAnimationNegative()
    {
        //_canvasBackGroundImage.color = _incorrectColor;
        //Invoke("ResetBackgroundColor", 0.2f);

        gameObject.GetComponent<Text>().color = _incorrectColor;

        switch (Random.Range(0, 1))
        {
            case 0:
                iTween.PunchScale(gameObject, new Vector3(2, 0, 0), 2.5f);
                return;
            case 1:
                iTween.PunchScale(gameObject, new Vector3(0, 5, 0), 2.5f);
                return;
        }
    }

    public void RandomAnimationPositive()
    {
        //_canvasBackGroundImage.color = _correctColor;
        //Invoke("ResetBackgroundColor", 0.2f);

        gameObject.GetComponent<Text>().color = _correctColor;

        switch (Random.Range(0,5))
        {
            case 0:
                iTween.MoveBy(gameObject, iTween.Hash("x", 5, "time", 2.5f, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
                return;
            case 1:
                iTween.MoveBy(gameObject, iTween.Hash("x", -5, "time", 2.5f, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
                return;
            case 2:
                iTween.MoveBy(gameObject, iTween.Hash("y", 5, "time", 2.5f, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
                return;
            case 3:
                iTween.MoveBy(gameObject, iTween.Hash("y", -10, "time", 2.5f, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
                return;
            case 4:
                iTween.RotateBy(gameObject, iTween.Hash("z", 1, "time", 2.5f, "delay", .1));
                ;
                return;
            case 5:
                iTween.RotateBy(gameObject, iTween.Hash("z", -1, "time", 2.5f, "delay", .1));
                ;
                return;
           /* case 6:
                iTween.RotateBy(gameObject, iTween.Hash("y", 1, "time", 2.5f, "delay", .1, "easetype", "EaseInSline"));
                return;
            case 7:
                iTween.PunchPosition(gameObject, iTween.Hash("x", 5, "time", 2.5f, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
                return;*/
        }
    }

    public void NoAnimationPositive()
    {
        gameObject.GetComponent<Text>().color = _correctColor;
    }

    public void NoAnimationNegative()
    {
        gameObject.GetComponent<Text>().color = _incorrectColor;
    }

    public void ResetPosition()
    {
       iTween.Stop(gameObject);
       transform.position = _startPos;
       transform.rotation  = _startRotation;
        transform.localScale = _startScale;
        gameObject.GetComponent<Text>().color = Color.white;
    }

    void ResetBackgroundColor()
    {
        _canvasBackGroundImage.color = _originalCanvasColor;
    }
}
