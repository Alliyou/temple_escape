using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ToArchive : MonoBehaviour {

    // Use this for initialization
    public Sprite clicked;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick()
    {
        GetComponent<Image>().sprite = clicked;
        SceneManager.LoadScene("Archives");
    }
}
