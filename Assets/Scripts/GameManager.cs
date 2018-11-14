using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text CountdownText;

    private int money = 0;
    public int Money
    {
        get { return money; }
        set
        {
            //funkcja wybiera wartość większą spośród dwóch
            money = Mathf.Max(0, value);

            if (OnMoneyChanged != null)
                OnMoneyChanged.Invoke(money);
        }
    }

    public event System.Action<int> OnMoneyChanged;

    void Awake()
    {
      //  Spawner.gameObject.SetActive(false);
        var ship = FindObjectOfType<Ship>();
        // Money = 0;
        //subskrybujemy zdarzenie z skryptu Ship
        // FindObjectOfType<Ship>().OnShipDestroyed += () => SceneManager.LoadScene("gameover");
        FindObjectOfType<Ship>().OnShipDestroyed += () => OnGameEnded();
        StartCoroutine(CountdownCoroutine());
    }

    // Use this for initialization
    void Start()
    {
        Money = 0;


     


    }

    void Update()
    {
       // var waveNumber = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber;
      //  if (waveNumber == 2) Instantiate(SpawnerPrefab);
    }

    void OnGameEnded()
    {
        var points = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber * 10;
        GameState.SetCurrentResult(points); //nie tworzymy obiektu bo to klasa statyczna

        SceneManager.LoadScene("gameover");
    }

    IEnumerator CountdownCoroutine() //korutyna odliczania do startu
    {
       
      
        CountdownText.enabled = true; //tekst z odliczaniem

        for (int i = 3; i > 0; i--) //odliczanie
        {
            CountdownText.text = i.ToString();
            yield return new WaitForSeconds(0.5f); //czekanie sekundy
        }

        CountdownText.text = "GO!"; 

        yield return new WaitForSeconds(1f);

        CountdownText.enabled = false; // wyłączamy tekst
     
    }

}