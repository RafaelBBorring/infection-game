using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health = 5;
    public int damage = 1;
    public float fireRate = 0.5f;
    public float realoadTime = 1f;
    public int bullets = 6;
    public int coins;
    public int bombs = 2;
    public int upgradesCost = 20;
    public int Score = 0;

    public static GameManager gManager;

	// Use this for initialization
	void Awake () {

        if(gManager == null)
        {
            gManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
