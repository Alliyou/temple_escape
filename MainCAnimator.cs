using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainCAnimator : MonoBehaviour {
    private int num_of_hammer,num_of_bag,num_of_meat;
    public Transform currentcollider;
    public int direction;
    public string scene;
    public GameObject Hammer_num,Bag_num, Meat_num;
    private GameObject Accept_Button;
    public Sprite Bag, Meat;
    private float time;
    private bool timing;
    // Use this for initialization
    void Start () {
        num_of_hammer = PlayerPrefs.GetInt("num_of_hammer",0);
        num_of_bag = PlayerPrefs.GetInt("num_of_bag", 0);
        num_of_meat = PlayerPrefs.GetInt("num_of_meat", 0);
        currentcollider = null;
        direction = PlayerPrefs.GetInt("Char_Direction", 1);
        Accept_Button = GameObject.Find("Accept");
        GameObject.Find("Accept").SetActive(false);
        scene = "Scene1";
        GameObject g=GameObject.Find("Main Camera");
        switch(scene)
        {
            case "Scene1":
                g.transform.position = new Vector3(0, 0, -10);
                break;
            case "Scene2":
                g.transform.position = new Vector3(40, 0, -10);
                break;
            case "Scene3":
                g.transform.position = new Vector3(80, 0, -10);
                break;
            case "Scene4":
                g.transform.position = new Vector3(120, 0, -10);
                break;
        }
        time = 0;
        timing = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(timing)
        {
            time += Time.deltaTime;
            if(time>0.2)
            {
                validate();
            }
        }
        direction = PlayerPrefs.GetInt("Char_Direction", 1);
        num_of_hammer = PlayerPrefs.GetInt("num_of_hammer", 0);
        num_of_bag = PlayerPrefs.GetInt("num_of_bag", 0);
        num_of_meat = PlayerPrefs.GetInt("num_of_meat", 0);
        string s = PlayerPrefs.GetString("choosed_operate", "null");
        if (currentcollider!=null)
        {
            if(s!="Hammer"||Mathf.Abs(Mathf.Pow((currentcollider.transform.position.x-transform.position.x),2)+Mathf.Pow((currentcollider.transform.position.y-transform.position.y),2))>1.5)
            {
                currentcollider = null;
                GameObject.Find("explosion").transform.position = new Vector3(0, 0, -10);
            }
        }
        GameObject.Find("Model").transform.position = new Vector3(0, 0, -10);
        if (s=="Bag"&&num_of_bag>0)
        {
            GameObject g = GameObject.Find("Model");
            g.GetComponent<SpriteRenderer>().sprite = Bag;
            if(direction==1)
            {
                g.transform.position = transform.position + Vector3.right;
            }
            if (direction == 2)
            {
                g.transform.position = transform.position + Vector3.up;
            }
            if (direction == 3)
            {
                g.transform.position = transform.position + Vector3.left;
            }
            if (direction == 4)
            {
                g.transform.position = transform.position + Vector3.down;
            }
            g.transform.Translate(0, 0, 10);
        }
        if(s=="Meat"&&num_of_meat>0)
        {
            GameObject g = GameObject.Find("Model");
            g.GetComponent<SpriteRenderer>().sprite = Meat;
            if (direction == 1)
            {
                g.transform.position = transform.position + Vector3.right;
            }
            if (direction == 2)
            {
                g.transform.position = transform.position + Vector3.up;
            }
            if (direction == 3)
            {
                g.transform.position = transform.position + Vector3.left;
            }
            if (direction == 4)
            {
                g.transform.position = transform.position + Vector3.down;
            }
            g.transform.Translate(0, 0, 10);
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = PlayerPrefs.GetInt("Char_Direction", 1);
        float moveangle = direction * 90 - 90;
        float collisionangle = Mathf.Atan2(collision.collider.transform.position.y - transform.position.y, collision.collider.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
       
        if(collisionangle<0)
        {
            collisionangle += 360;
        }

        if (num_of_hammer>0&&PlayerPrefs.GetString("choosed_operate","null")=="Hammer"&&collision.collider.name=="WoodWall"&&(Mathf.Abs(moveangle-collisionangle)<20||Mathf.Abs(360-Mathf.Abs(moveangle-collisionangle))<20))
        {
            currentcollider= collision.collider.transform;
            GameObject.Find("explosion").transform.position = new Vector3(collision.collider.transform.position.x, collision.collider.transform.position.y, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="broken_hammer")
        {
            num_of_hammer++;
            Hammer_num.GetComponent<Text>().text = num_of_hammer.ToString();
            PlayerPrefs.SetInt("num_of_hammer", num_of_hammer);
            Destroy(collision.gameObject);
            GameObject.Find("Hammer_num").GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            GameObject.Find("Bag_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Meat_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Model").transform.position = new Vector3(0, 0, -10);
            PlayerPrefs.SetString("choosed_operate", "Hammer");
            //openHintWindow();
        }
        if(collision.name=="bag")
        {
            num_of_bag++;
            Bag_num.GetComponent<Text>().text = num_of_bag.ToString();
            PlayerPrefs.SetInt("num_of_bag", num_of_bag);
            Destroy(collision.gameObject);
            GameObject.Find("Bag_num").GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            GameObject.Find("Hammer_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Meat_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            PlayerPrefs.SetString("choosed_operate", "Bag");
            //openHintWindow();
        }
        if(collision.name=="meat")
        {
            num_of_meat++;
            Meat_num.GetComponent<Text>().text = num_of_meat.ToString();
            PlayerPrefs.SetInt("num_of_meat", num_of_meat);
            Destroy(collision.gameObject);
            GameObject.Find("Meat_num").GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            GameObject.Find("Hammer_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            GameObject.Find("Bag_num").GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            PlayerPrefs.SetString("choosed_operate", "Meat");
        }
        if(collision.name=="1to2")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(40, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(24, -7,0);
            scene = "Scene2";
        }
        if(collision.name=="2to1")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(0, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(15, -7, 0);
            scene = "Scene1";
        }
        if(collision.name=="2to3")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(80, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(63, -7, 0);
            scene = "Scene3";
        }
        if(collision.name=="3to2")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(40, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(57, -7, 0);
            scene = "Scene2";
        }
        if(collision.name=="3to4")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(120, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(103, -7, 0);
            scene = "Scene4";
        }
        if(collision.name=="4to3")
        {
            GameObject.Find("Main Camera").transform.position=new Vector3(80, 0, -10);
            GameObject.Find("MainCharacter").transform.position = new Vector3(95, -7, 0);
            scene = "Scene3";
        }
    }
    public void openHintWindow()
    {
        Debug.Log("open");
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = false;
        GameObject.Find("operate").GetComponent<Button>().interactable = false;
        GameObject.Find("Hint_Window").transform.position = new Vector3(0, 0, 0);
        Accept_Button.SetActive(true);
    }

    void invalidate()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = false;
        GameObject.Find("operate").GetComponent<Button>().interactable = false;
        timing = true;

    }

    void validate()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = true;
        GameObject.Find("operate").GetComponent<Button>().interactable = true;
        time = 0;
        timing = false;
    }
}
