using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ButtonNV()
    {
        SceneManager.LoadScene("Introducao1");
    }
    public void ButtonCR()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void ButtonRK()
    {
        SceneManager.LoadScene("Rank");
    }
    public void ButtonSR()
    {
        Application.Quit();
    }
    public void BTNPular()
    {
        SceneManager.LoadScene("Fase1");
    }
    public void BTNIntroducao2()
    {
        SceneManager.LoadScene("Introducao2");
    }
    public void BTNIntroducao3()
    {
        SceneManager.LoadScene("Introducao3");
    }
    public void BTNIntroducao4()
    {
        SceneManager.LoadScene("Introducao4");
    }
    public void BTNIntroducao5()
    {
        SceneManager.LoadScene("Introducao5");
    }
    public void BTNIntroducao6()
    {
        SceneManager.LoadScene("Introducao6");
    }
    public void BTNIntroducao7()
    {
        SceneManager.LoadScene("Introducao7");
    }
}
