using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUserInterface : MonoBehaviour {

    [SerializeField]
    Text WaveCounter;

    [SerializeField]
    Text MoneyCounter;

    // Use this for initialization
    void Awake () {

        FindObjectOfType<GameManager>().OnMoneyChanged += money =>
         {
             MoneyCounter.text = "Money: " + money.ToString();
         };

        FindObjectOfType<AsteroidWaveController>().OnWaveStarted += waveNumber =>
        StartCoroutine(WaveCounterCoroutine(waveNumber));
	}

    private IEnumerator WaveCounterCoroutine(int waveNumber )
    {
        WaveCounter.gameObject.SetActive(true);
        WaveCounter.text = "Wave: " + waveNumber.ToString();

        yield return new WaitForSeconds(2f);
        WaveCounter.gameObject.SetActive(false);
    }

}
