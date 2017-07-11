using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class button_operate : MonoBehaviour {

    // Use this for initialization
    public Sprite clicked,realeased;
    public string choosed_operate;
    private SpriteRenderer spriterenderer;
    public Sprite[] exsprites;
    private bool ex;
    private float exindex;
    private float framepersecond;
    public GameObject bag,meat;
	void Start () {
        choosed_operate = PlayerPrefs.GetString("choosed_operate", "null");
        spriterenderer = GameObject.Find("explosion").GetComponent<Renderer>() as SpriteRenderer;
        ex = false;
        exindex = 0;
        framepersecond = 20;
	}
	
	// Update is called once per frame
	void Update () {
        choosed_operate = PlayerPrefs.GetString("choosed_operate", "null");
        
        if(Input.GetMouseButtonDown(0)&& GetComponent<Button>().interactable == true)
        {
            Vector2 pos = GetComponent<RectTransform>().position;
            Vector2 size = GetComponent<RectTransform>().sizeDelta;
            Vector3 mousepos = Input.mousePosition;
            if (Mathf.Abs(mousepos.x - pos.x) <= size.x / 2 && Mathf.Abs(mousepos.y - pos.y) <= size.y / 2)
            {
                GetComponent<Image>().sprite = clicked;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            GetComponent<Image>().sprite = realeased;
        }
        if(ex)
        {
            spriterenderer.sprite = exsprites[(int)exindex];
            exindex =exindex+(Time.deltaTime*framepersecond);
            if (exindex >= exsprites.Length)
            {
                GameObject.Find("explosion").transform.position = new Vector3(0, 0, -10);
                spriterenderer.sprite = exsprites[6];
                ex = false;
                exindex = 0;
                DestroyObject(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().currentcollider.gameObject);
                
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards").transform)
                    { 
                        child.gameObject.GetComponent<Guard>().needdetection = true;
                    }
                }
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies").transform)
                    {
                        child.gameObject.GetComponent<Zombie>().needdetection = true;
                    }
                }
            }
        }
    }

    public void OnButtonClick()
    {
        if (choosed_operate == "Hammer")
        {
            if(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().currentcollider != null)
            {
                int temp = PlayerPrefs.GetInt("num_of_hammer", 0);
                temp--;
                PlayerPrefs.SetInt("num_of_hammer", temp);
                ex = true;
            }
        }
        if(choosed_operate=="Bag")
        {
            if(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().currentcollider == null&& PlayerPrefs.GetInt("num_of_bag", 0)>0)
            {
                GameObject m = GameObject.Find("Model");
                GameObject g=Instantiate(bag, m.transform.position, m.transform.rotation);
                m.transform.position = new Vector3(0, 0, -10);
                g.transform.name = "bag";
                g.transform.parent = GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/bags").transform;
                
                int temp= PlayerPrefs.GetInt("num_of_bag", 0);
                temp--;
                PlayerPrefs.SetInt("num_of_bag", temp);
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards").transform)
                    {
                        child.gameObject.GetComponent<Guard>().needdetection = true;
                    }
                }
            }
        }
        if(choosed_operate=="Meat")
        {
            if (GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().currentcollider == null && PlayerPrefs.GetInt("num_of_meat", 0) > 0)
            {
                GameObject m = GameObject.Find("Model");
                GameObject g = Instantiate(meat, m.transform.position, m.transform.rotation);
                g.transform.name = "meat";
                g.transform.parent = GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/meats").transform;
                m.transform.position = new Vector3(0, 0, -10);
                int temp = PlayerPrefs.GetInt("num_of_meat", 0);
                temp--;
                PlayerPrefs.SetInt("num_of_meat", temp);
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies").transform)
                    {
                        child.gameObject.GetComponent<Zombie>().needdetection = true;
                    }
                }
            }
        }
    }
}
