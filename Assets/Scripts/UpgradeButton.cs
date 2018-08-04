using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    public IUpgradable Upgradable;

	void Awake () {
        var waveController = FindObjectOfType<AsteroidWaveController>();

        waveController.OnWaveStarted += _ => gameObject.SetActive(false);
        waveController.OnWaveEnded += _ => gameObject.SetActive(true);
	}
	
    void Update()
    {

    }
    public void Configure(IUpgradable upgradable)
    {
        Upgradable = upgradable;
        GetComponent<Button>().onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
        Upgradable.Upgrade();
    }

}
