using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour {

    Vector3 _shakePosition;
    

	// Use this for initialization
	void Start () {
        _shakePosition = new Vector3(0.2f,0,0);

    }

    public void ShakeActivation()
    {
        iTween.ShakePosition(gameObject, _shakePosition, 1);
    }
}
