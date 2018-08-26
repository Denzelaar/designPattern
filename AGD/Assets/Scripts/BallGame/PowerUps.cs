using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;


public class PowerUps : MonoBehaviour
{
    Transform _powerUpsHolder;
    Transform _ballsHolder;
    Text _bombAmountText;
    const int _START_BOMB_AMOUNT = 3;
    int _bombAmount;
    private BallGame _ballGame;

	// Use this for initialization
	public void Init ()
	{
	    _ballGame = transform.GetComponent<BallGame>();
        _powerUpsHolder = transform.Find("PowerUpsHolder");
	    _ballsHolder = transform.Find("BallsHolder/SpawnArea");
        _bombAmountText = _powerUpsHolder.Find("BombHolder/BombAmountText").GetComponent<Text>();
	    _bombAmount = _START_BOMB_AMOUNT;
	    _bombAmountText.text = _bombAmount.ToString();
	}
	

    public void HandleBombButton()
    {
        if (_bombAmount > 0)
        {
            _bombAmount -= 1;
            _bombAmountText.text = _bombAmount.ToString();
            GameObject bomb = (GameObject)Instantiate(Resources.Load("BallGame/Bomb"));
            bomb.transform.SetParent(_ballsHolder);
            bomb.transform.localPosition = new Vector2(_ballsHolder.GetComponent<RectTransform>().sizeDelta.x / 2,
            _ballsHolder.GetComponent<RectTransform>().sizeDelta.y + 20);
            bomb.transform.localScale = Vector3.one;
            bomb.GetComponent<BombExplode>().OnExploded += _HandleBombExploded;
            bomb.GetComponent<BombExplode>().ArmBomb();
        }
    }

    public void StreakBomb()
    {
        GameObject bomb = (GameObject)Instantiate(Resources.Load("BallGame/Bomb"));
        bomb.transform.SetParent(_ballsHolder);
        bomb.transform.localPosition = new Vector2(_ballsHolder.GetComponent<RectTransform>().sizeDelta.x / 2,
        _ballsHolder.GetComponent<RectTransform>().sizeDelta.y + 20);
        bomb.transform.localScale = Vector3.one;
        bomb.GetComponent<BombExplode>().OnExploded += _HandleBombExploded;
        bomb.GetComponent<BombExplode>().ArmBomb();
    }

    void _HandleBombExploded()
    {
       _ballGame.CleanupBalls();
    }
}
