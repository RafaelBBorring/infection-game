using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour {

    public Text healthText, damageText, fireRateText, bulletsText, realoadTimeText, upgradeCostText;

    GameManager gManager;
    Player player;

	// Use this for initialization
	void Start () {

        gManager = GameManager.gManager;
        player = FindObjectOfType<Player>();
        UpdateUI();
		
	}

    void UpdateUI()
    {
        healthText.text = "Health: " + gManager.health;
        damageText.text = "Damage: " + gManager.damage;
        fireRateText.text = "FireRate: " + gManager.fireRate;
        bulletsText.text = "Bullets: " + gManager.bullets;
        realoadTimeText.text = "Reaload Time: " + gManager.realoadTime;
        upgradeCostText.text = "Upgrade Cost: " + gManager.upgradesCost;
    }

    public void SetHealth()
    {
        if(gManager.coins >= gManager.upgradesCost)
        {
            gManager.health++;
            FindObjectOfType<UIManager>().UpdateHealthBar();
            player.SetPlayerStatus();
            SetCoins(gManager.upgradesCost);
            gManager.upgradesCost += (gManager.upgradesCost / 5);
            UpdateUI();
        }
    }

    public void SetDamage()
    {
        if (gManager.coins >= gManager.upgradesCost)
        {
            gManager.damage++;
            player.SetPlayerStatus();
            SetCoins(gManager.upgradesCost);
            gManager.upgradesCost += (gManager.upgradesCost / 5);
            UpdateUI();
        }
    }

    public void SetFireRate()
    {
        if (gManager.coins >= gManager.upgradesCost)
        {
            gManager.fireRate-= 0.1f;
            if(gManager.fireRate <= 0)
            {
                gManager.fireRate = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gManager.upgradesCost);
            gManager.upgradesCost += (gManager.upgradesCost / 5);
            UpdateUI();
        }
    }

    public void SetBullets()
    {
        if (gManager.coins >= gManager.upgradesCost)
        {
            gManager.bullets++;
            player.SetPlayerStatus();
            SetCoins(gManager.upgradesCost);
            gManager.upgradesCost += (gManager.upgradesCost / 5);
            UpdateUI();
        }
    }
    public void SetRealoadTime()
    {
        if (gManager.coins >= gManager.upgradesCost)
        {
            gManager.realoadTime -= 0.1f;
            if (gManager.realoadTime <= 0)
            {
                gManager.realoadTime = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gManager.upgradesCost);
            gManager.upgradesCost += (gManager.upgradesCost / 5);
            UpdateUI();
        }
    }

    void SetCoins(int coin)
    {
        gManager.coins -= coin;
        FindObjectOfType<UIManager>().UpdateCoin();
    }
	

}
