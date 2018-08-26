using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTutorialText : MonoBehaviour {

    private float _timer;
	private bool _hasRun;
	GameObject tutorial;
    /*
	void Start()
	{
		tutorial = GameObject.Find ("Tutorial");
		_hasRun = false;
	}

	void Fade()
	{

	}

	// Update is called once per frame
	void Update()
	{
		_timer += Time.deltaTime;
		if(_timer >= 4.1f && _timer <= 5.1f && _hasRun == false)
		{
			_hasRun = true;
		}
	}

	public void FadeOut()
	{
		tutorial.transform.position = new Vector3 (10f, -4f);
	}

	public void FadeIn()
	{
		tutorial.transform.position = new Vector3 (2.7f, -3.5f);
	}
    */
}

