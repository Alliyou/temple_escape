using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Zombie : MonoBehaviour {

    public bool move;
    public bool done;
    public bool needdetection;
    private GameObject left, right, up, down;
    public GameObject detector;
    public Transform targettrans;
    private SpriteRenderer spriterenderer;
    public float index;
    private float framepresecond;
    public Sprite[] sprites;
    public Sprite[] runsprites;
    public Sprite[] attacksprites;
    public bool destroy;
    public int direction;
    // Use this for initialization
    void Start () {
        move = false;
        targettrans = null;
        spriterenderer = GetComponent<Renderer>() as SpriteRenderer;
        framepresecond = 10;
        done = false;
        index = 0;
        destroy = true;
        needdetection=true;
    }
	
	// Update is called once per frame
	void Update () {
        string s = GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene;
        int num = s[s.Length - 1] - '0';
        int zombiescene = (int)(transform.position.x + 20) / 40 + 1;
        if (needdetection && num == zombiescene)
        {
            left = Instantiate(detector, transform.position , transform.rotation);
            right = Instantiate(detector, transform.position , transform.rotation);
            up = Instantiate(detector, transform.position , transform.rotation);
            down = Instantiate(detector, transform.position, transform.rotation);
            left.transform.name = right.transform.name = up.transform.name = down.transform.name = "zombiedetector";
            left.GetComponent<zombiedetector>().direction = 3;
            right.GetComponent<zombiedetector>().direction = 1;
            up.GetComponent<zombiedetector>().direction = 2;
            down.GetComponent<zombiedetector>().direction = 4;
            left.GetComponent<zombiedetector>().derive = this.gameObject;
            right.GetComponent<zombiedetector>().derive = this.gameObject;
            up.GetComponent<zombiedetector>().derive = this.gameObject;
            down.GetComponent<zombiedetector>().derive = this.gameObject;
            needdetection = false;
        }
        if (move)
        {
            spriterenderer.sprite = runsprites[(int)index];
            index = index + Time.deltaTime * framepresecond;
            if (index >= runsprites.Length)
            {
                index = 0;
            }
            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (direction == 3)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            invalidate();
            if (targettrans == null && Mathf.Abs(Mathf.RoundToInt(transform.position.x) - transform.position.x) < 0.05 && Mathf.Abs(Mathf.RoundToInt(transform.position.y) - transform.position.y) < 0.05)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                move = false;
                index = 0;
                validate();
            }
            else
            {
                if (direction == 1)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(0.1f, 0, 0);
                }
                if (direction == 2)
                {
                    transform.Translate(0, 0.1f, 0);
                }
                if (direction == 3)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(0.1f, 0, 0);
                }
                if (direction == 4)
                {
                    transform.Translate(0, -0.1f, 0);
                }
            }
        }
        else
        {
            spriterenderer.sprite = sprites[(int)index];
            index = index + Time.deltaTime * framepresecond;
            if (index >= sprites.Length)
            {
                index = 0;
            }

            if (done)
            {
                done = false;
                needdetection = true;
                GameObject g = GameObject.Find(GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene + "/guards");
                if (g != null)
                {
                    foreach (Transform child in g.transform)
                    {
                        child.gameObject.GetComponent<Guard>().needdetection = true;
                    }
                }
            }
        }
    }

    void invalidate()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = false;
        GameObject.Find("operate").GetComponent<Button>().interactable = false;
    }

    void validate()
    {
        GameObject.Find("Direction_Control").GetComponent<Button>().interactable = true;
        GameObject.Find("operate").GetComponent<Button>().interactable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "meat")
        {
            Destroy(targettrans.gameObject);
            targettrans = null;
            done = true;
        }

    }
}

