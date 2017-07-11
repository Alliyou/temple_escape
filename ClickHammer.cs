using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickHammer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("choosed_operate", "null")=="Hammer")
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
        }
        GameObject.Find("Hammer_num_text").GetComponent<Text>().text = PlayerPrefs.GetInt("num_of_hammer", 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Hammer_num_text").GetComponent<Text>().text = PlayerPrefs.GetInt("num_of_hammer", 0).ToString();
    }

    public void OnButtonClick()
    {
        string s = PlayerPrefs.GetString("choosed_operate", "null");
        if (s != "Hammer")
        {
            GameObject.Find("Bag_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Meat_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            PlayerPrefs.SetString("choosed_operate", "Hammer");
            GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
        }
        else
        {
            PlayerPrefs.SetString("choosed_operate", "null");
            GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        }
        GameObject.Find("Model").transform.position = new Vector3(0, 0, -10);
    }
}
