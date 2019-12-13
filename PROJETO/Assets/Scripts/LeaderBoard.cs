using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{

    public Text[] highScores;
    public InputField playerName;
    public Button btnSalvar;

    int[] highScoreValues;
    string[] highScoreNames;

    // Use this for initialization
    void Start()
    {
        highScoreValues = new int[highScores.Length];
        highScoreNames = new string[highScores.Length];

        for (int x = 0; x < highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("highScoreValues" + x);
            highScoreNames[x] = PlayerPrefs.GetString("highScoreNames" + x);
        }

        DrawScores();
    }

    void SaveScore()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            PlayerPrefs.SetInt("highScoreValues" + x, highScoreValues[x]);
            PlayerPrefs.SetString("highScoreNames" + x, highScoreNames[x]);
        }
    }

    //public void CheckForHighScore(int _value, string _userName)
    public void CheckForHighScore()
    {

        string _userName = playerName.text;
        GameManager gManager;

        gManager = GameManager.gManager;
        //GetComponent<leaderBoard>().CheckForHighScore(gManager.Score, playerName.text);


        for (int x = 0; x < highScores.Length; x++)
        {
            if (gManager.Score > highScoreValues[x])
            {
                for (int y = highScores.Length - 1; y > x; y--)
                {
                    highScoreValues[y] = highScoreValues[y - 1];
                    highScoreNames[y] = highScoreNames[y - 1];
                }
                highScoreValues[x] = gManager.Score;
                highScoreNames[x] = _userName;
                DrawScores();
                SaveScore();
                break;

            }
        }

        //myButton = GameObject.Find("btnSalvar");
        //myButton.button.interactiable = false;
        btnSalvar.gameObject.SetActive(false);
        Destroy(btnSalvar);

    }

    void DrawScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            highScores[x].text = highScoreNames[x] + ":" + highScoreValues[x].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BTNMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}