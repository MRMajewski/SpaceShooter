using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

   // Button button;
	// Use this for initialization
	void Start () {

     //   button = GetComponent<Button>();
    //    button.onClick.AddListener(Pause);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
