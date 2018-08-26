using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    const float _DEFAULT_BOMB_RADIUS = 2f;
    float _radius;
    public Animator anim;
    private bool _initialized;

    public delegate void BombEvent();
    public BombEvent OnExploded;

    ShakeScreen _shakeScreen;

    void _Init()
    {
        _radius = _DEFAULT_BOMB_RADIUS;
        _shakeScreen = GameObject.FindWithTag("GameView").GetComponent<ShakeScreen>();
        iTween.ShakeScale(gameObject, new Vector3(0.5f,0,0), 3);

    }

    public void ArmBomb()
    {
        if (!_initialized)
        {
            _Init();
        }

        Invoke("_Animate", 2.5f);
        Invoke("_Detonate", 3);
   }

    void _Detonate()
    {
        Vector2 explotionPosition = this.gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explotionPosition, _radius);

        foreach (Collider2D hit in colliders)
        {
			if (hit.gameObject.CompareTag("Ball") || hit.gameObject.CompareTag("Triangle") || hit.gameObject.CompareTag("Square"))
            {
                hit.GetComponent<Ball>().SetRemovedByPowerup();
            }
        }

        if (OnExploded != null)
        {
            OnExploded();
        }
        _shakeScreen.ShakeActivation();
        Destroy(this.gameObject);
    }

    void _Animate()
    {
        anim.gameObject.SetActive(true);
        anim.enabled = true;
    }
}

