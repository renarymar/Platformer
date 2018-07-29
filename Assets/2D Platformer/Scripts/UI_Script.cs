using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play(int Num)
    {
        SceneManager.LoadScene(Num);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
