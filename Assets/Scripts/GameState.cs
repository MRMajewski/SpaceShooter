using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState {

    private const string LastResult = "last_result";
    private const string Record = "record";

    public static void SetCurrentResult(int points)
    {
        PlayerPrefs.SetInt(LastResult, points); //LastResult to klucz do wartości points

        var record = GetRecord();

        if (points > record)
            PlayerPrefs.SetInt(Record, points);

    }

    public static int GetLastResult()
    {
        return PlayerPrefs.GetInt(LastResult,0); //0 to wartość domyślna
    }

    public static int GetRecord()
    {
        return PlayerPrefs.GetInt(Record, 0);
    }
}
