using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour {

	const float MOVE_AMOUNT = 7.3f;
	const float MOVE_DURATION = 1f;

	Vector3 _startPosition;
	Vector3 _leftPosition;
	Vector3 _centerPosition;
	Vector3 _rightPosition;

	void Start () {
		_startPosition = this.gameObject.GetComponent<RectTransform> ().anchoredPosition;
		_leftPosition = new Vector3 (-MOVE_AMOUNT, transform.position.y, 0);
		_centerPosition = new Vector3(0, transform.position.y, 0);
		_rightPosition = new Vector3 (MOVE_AMOUNT, transform.position.y, 0);
	}

	public void MoveLeft()
	{
		iTween.MoveTo (this.gameObject, _leftPosition, MOVE_DURATION);
	}

	public void MoveIntoScreen()
	{
		iTween.MoveTo (this.gameObject, _centerPosition, MOVE_DURATION);
	}

	public void MoveRight()
	{

		iTween.MoveTo (this.gameObject, _rightPosition, MOVE_DURATION);
	}

    public void MoveBackButtonTo(Vector3 pos)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", pos, "islocal", true, "time", MOVE_DURATION));
    }

	/*
	 * Sometimes it's necessary to reset a menu screen to its initial position, without
	 * it visually moving across the screen
	 */
	public void ResetPosition()
	{
		this.gameObject.GetComponent<RectTransform> ().anchoredPosition = _startPosition;
	}
}
