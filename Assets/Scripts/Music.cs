﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class Music : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (FindObjectsOfType<Music>().Length > 1) //sprawdzamy ilość obiektów Music w tablicy
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

	}

}
