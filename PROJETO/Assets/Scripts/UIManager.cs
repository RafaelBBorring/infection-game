using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text bulletText;
    public Text healthText;
    public Slider healthBar;
    public Text coinsText;
    public Text ScoreText;
    public Text bombsText;
    public Text fireRateText;

    // Use this for initialization
    void Start () {

        UpdateHealthBar();
        UpdateCoin();
        UpdateScore();
	}
	
    
    public void UpdateBulletUI(int bullet)
    {
        bulletText.text = bullet.ToString();
    }

    public void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health;
    }
    public void UpdateCoin()
    {
        coinsText.text = GameManager.gManager.coins.ToString();
    }
    public void UpdateScore()
    {
        ScoreText.text = GameManager.gManager.Score.ToString();
    }

    public void UpdateBombs(int bombs)
    {
        bombsText.text = bombs.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = GameManager.gManager.health;
    }



    
}
