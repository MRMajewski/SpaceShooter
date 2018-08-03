using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour {


	void Awake () {
        var waveController = FindObjectOfType<AsteroidWaveController>();

        waveController.OnWaveStarted += _ => gameObject.SetActive(false);
        waveController.OnWaveEnded += _ => gameObject.SetActive(true);
	}
	

}
