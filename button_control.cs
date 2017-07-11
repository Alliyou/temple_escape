using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class button_control : MonoBehaviour {

    // Use this for initialization
    private float movespeed;
    public Sprite up,down,left,right,normal;
    public Sprite[] sprites;
    private SpriteRenderer spriterenderer;
    private float framesPresecond;
    private int index;
    private bool move;
    private bool continuemove;
    private int direction;
    public float time;
    public bool guardneeddetection;
    public bool characterdestroy;
    void Start () {
        movespeed = 0.005f;
        framesPresecond = 10;
        index = 0;
        move = false;
        spriterenderer = GameObject.Find("MainCharacter").GetComponent<Renderer>() as SpriteRenderer;
        direction = PlayerPrefs.GetInt("Char_Direction", 1);
        time = 0;
        continuemove = false;
        guardneeddetection = true;
        characterdestroy = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
        GameObject g = GameObject.Find("MainCharacter");
        if(Input.GetMouseButton(0)&& GetComponent<Button>().interactable == true)
        {
            
            Vector2 pos = GetComponent<RectTransform>().position;
            Vector2 size = GetComponent<RectTransform>().sizeDelta;
            Vector3 mousepos = Input.mousePosition;
            time += Time.deltaTime;
            if (Mathf.Abs(mousepos.x - pos.x) <= size.x / 2 && Mathf.Abs(mousepos.y - pos.y) <= size.y / 2)
            {
                move = true;
                float angle = Mathf.Atan2(mousepos.y - pos.y, mousepos.x - pos.x) * Mathf.Rad2Deg;
                if (angle >= -45 && angle < 45)
                {
                    if(direction!=1)
                    {
                        g.transform.position = new Vector3(Mathf.RoundToInt(g.transform.position.x), Mathf.RoundToInt(g.transform.position.y), g.transform.position.z);
                    }
                    direction = 1;
                    PlayerPrefs.SetInt("Char_Direction", 1);
                    g.GetComponent<BoxCollider2D>().offset= new Vector2(0.2f, 0);
                    
                    GetComponent<Image>().sprite = right;
                }
                if (angle >= 45 && angle <= 135)
                {
                    if (direction != 2)
                    {
                        g.transform.position = new Vector3(Mathf.RoundToInt(g.transform.position.x), Mathf.RoundToInt(g.transform.position.y), g.transform.position.z);
    
                    }
                    direction = 2;
                    PlayerPrefs.SetInt("Char_Direction", 2);
                    g.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.2f);
                    
                    GetComponent<Image>().sprite = up;
                }
                if (angle >= 135 && angle <= 180 || angle >= -180 && angle < -135)
                {
                    if (direction != 3)
                    {
                        g.transform.position = new Vector3(Mathf.RoundToInt(g.transform.position.x), Mathf.RoundToInt(g.transform.position.y), g.transform.position.z);

                    }
                    direction = 3;
                    PlayerPrefs.SetInt("Char_Direction", 3);
                    g.GetComponent<BoxCollider2D>().offset = new Vector2(-0.2f, 0);
                    
                    GetComponent<Image>().sprite = left;
                }
                if (angle >= -135 && angle < -45)
                {
                    if (direction != 4)
                    {
                        g.transform.position = new Vector3(Mathf.RoundToInt(g.transform.position.x), Mathf.RoundToInt(g.transform.position.y), g.transform.position.z);
                    }
                    direction = 4;
                    PlayerPrefs.SetInt("Char_Direction", 4);
                    g.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.2f);
                    
                    GetComponent<Image>().sprite = down;
                }

            }
            else
            {
                g.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            }
        }
        else
        {
            time = 0;
            g.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            GetComponent<Image>().sprite = normal;
            if (Mathf.Abs(g.transform.position.x - Mathf.RoundToInt(g.transform.position.x)) < 0.1
                && Mathf.Abs(g.transform.position.y - Mathf.RoundToInt(g.transform.position.y)) < 0.1)
            {
               g.transform.position = new Vector3(Mathf.RoundToInt(g.transform.position.x), Mathf.RoundToInt(g.transform.position.y), g.transform.position.z);
                move = false;
                continuemove = false;
                guardneeddetection = true;
            }
            else
            {
                continuemove = true;
            }
        }
        if(move)
        {
            if (time > 0.3)
            {
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards").transform)
                    {
                        child.gameObject.GetComponent<Guard>().needdetection = true;
                    }
                }
                
                guardneeddetection = false;
            }
            /*if(time>0.3)
            {
                if (GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies") != null)
                {
                    foreach (Transform child in GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/zombies").transform)
                    {
                        child.gameObject.GetComponent<Zombie>().needdetection = true;
                    }
                }
            }*/
            if (direction == 1)
            {
                index = 8+(int)(Time.timeSinceLevelLoad * framesPresecond) % 4;
                if(time>0.15||continuemove)
                    g.transform.Translate(movespeed, 0, 0);
            }
            if(direction==2)
            {
                index = 12 + (int)(Time.timeSinceLevelLoad * framesPresecond) % 4;
                if(time>0.15||continuemove)
                    g.transform.Translate(0, movespeed, 0);
            }
            if(direction==3)
            {
                index = 4 + (int)(Time.timeSinceLevelLoad * framesPresecond) % 4;
                if(time>0.15||continuemove)
                    g.transform.Translate(-movespeed, 0, 0);
            }
            if(direction==4)
            {
                index = (int)(Time.timeSinceLevelLoad * framesPresecond) % 4;
                if(time>0.15||continuemove)
                    g.transform.Translate(0, -movespeed, 0);
            }
            spriterenderer.sprite = sprites[index];
        }
        else
        {
            index = index / 4 * 4;
            spriterenderer.sprite = sprites[index];
        }
    }
    void invalidate()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = false;
        GameObject.Find("operate").GetComponent<Button>().interactable = false;
    }

}
