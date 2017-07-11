using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ToHome : MonoBehaviour {

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
        SceneManager.LoadScene("Main");
    }
}
