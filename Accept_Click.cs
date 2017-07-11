using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Accept_Click : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable=true;
        GameObject.Find("operate").GetComponent<Button>().interactable = true;
        GameObject.Find("Hint_Window").transform.position = new Vector3(0, 0, -10);
        GameObject.Find("Accept").SetActive(false);
    }
}
