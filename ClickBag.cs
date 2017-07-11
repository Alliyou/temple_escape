using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickBag : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetString("choosed_operate", "null") == "Bag")
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
        }
        GameObject.Find("Bag_num_text").GetComponent<Text>().text = PlayerPrefs.GetInt("num_of_bag", 0).ToString();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Bag_num_text").GetComponent<Text>().text = PlayerPrefs.GetInt("num_of_bag", 0).ToString();
    }

    public void OnButtonClick()
    {
        string s = PlayerPrefs.GetString("choosed_operate", "null");
        if (s != "Bag")
        {
            GameObject.Find("Hammer_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Meat_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            PlayerPrefs.SetString("choosed_operate", "Bag");
            GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
        }
        else
        {
            PlayerPrefs.SetString("choosed_operate", "null");
            GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        }
        
    }
}
