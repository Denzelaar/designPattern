using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScreen : MonoBehaviour {
	
	const float MOVE_AMOUNT = 4f;
	const float TOP_AMOUNT = 8f;
	const float MOVE_DURATION = 1f;

	Vector3 _startPosition;
	Vector3 _topPosition;
	Vector3 _centerPosition;

	void Start () {
		_startPosition = this.gameObject.GetComponent<RectTransform> ().anchoredPosition;
		_topPosition = new Vector3 (transform.position.x, TOP_AMOUNT, 0);
		_centerPosition = new Vector3 (transform.position.x, MOVE_AMOUNT, 0);
	}
		

	public void MoveIntoScreen()
	{
		iTween.MoveTo (this.gameObject, _centerPosition, MOVE_DURATION);
	}

	public void MoveOut()
	{

		iTween.MoveTo (this.gameObject, _topPosition, MOVE_DURATION);
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