using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int money = 0;
    public int Money
    {
        get { return money; }
        set
        {
            //funkcja wybiera wartość większą spośród dwóch
            money = Mathf.Max(0,value);

            if (OnMoneyChanged != null)
                OnMoneyChanged.Invoke(money);
        }
    }

    public event System.Action<int> OnMoneyChanged;

    void Awake()
    {
       // Money = 0;
        //subskrybujemy zdarzenie z skryptu Ship
        // FindObjectOfType<Ship>().OnShipDestroyed += () => SceneManager.LoadScene("gameover");
        FindObjectOfType<Ship>().OnShipDestroyed += () => OnGameEnded();
    }

	// Use this for initialization
	void Start () {
        Money = 0;

    }

    void OnGameEnded()
    {
        var points = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber * 10;

        SceneManager.LoadScene("gameover");
    }
}
