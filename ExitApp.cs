﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitApp : MonoBehaviour {

    // Use this for initialization
    public Sprite clicked;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick()
    {
        PlayerPrefs.DeleteAll();
        GetComponent<Image>().sprite = clicked;
        Application.Quit();
    }
}
