using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Guard : MonoBehaviour {

    // Use this for initialization
    public bool needdetection;
    private GameObject left, right, up, down;
    public GameObject detector;
    public bool move;
    public bool done;
    public int direction;
    public GameObject target;
    private SpriteRenderer spriterenderer;
    public float index;
    private float framepresecond;
    public Sprite[] sprites;
    public Sprite[] runsprites;
    public Sprite[] attacksprites;
    public bool destroy;
    void Start () {
        move = false;
        target = null;
        spriterenderer = GetComponent<Renderer>() as SpriteRenderer;
        framepresecond = 10;
        done = false;
        index = 0;
        destroy = true;
        left = right = up = down = null;
        needdetection = true;
    }
	
	// Update is called once per frame
	void Update () {
        string s = GameObject.Find("MainCharacter").GetComponent<MainCAnimator>().scene;
        int num = s[s.Length - 1] - '0';
        int guardscene = (int)(transform.position.x + 20) / 40 + 1;
        if (needdetection&&num==guardscene)
        {
            
            left = Instantiate(detector, transform.position, Quaternion.Euler(0,0,0));
            right = Instantiate(detector, transform.position , Quaternion.Euler(0, 0, 0));
            up = Instantiate(detector, transform.position , Quaternion.Euler(0, 0, 0));
            down = Instantiate(detector, transform.position , Quaternion.Euler(0, 0, 0));
            left.transform.name = right.transform.name = up.transform.name = down.transform.name = "bagdetector";
            left.GetComponent<bagdetector>().direction = 3;
            right.GetComponent<bagdetector>().direction = 1;
            up.GetComponent<bagdetector>().direction = 2;
            down.GetComponent<bagdetector>().direction = 4;
            left.GetComponent<bagdetector>().derive = this.gameObject.transform;
            right.GetComponent<bagdetector>().derive = this.gameObject.transform;
            up.GetComponent<bagdetector>().derive = this.gameObject.transform;
            down.GetComponent<bagdetector>().derive = this.gameObject.transform;
            needdetection = false;
            
        }
        if (move)
        {
            spriterenderer.sprite = runsprites[(int)index];
            index = index + Time.deltaTime * framepresecond;
            if(index>=runsprites.Length)
            {
                index = 0;
            }
            if(direction==1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(direction==3)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            invalidate();
            if (target == null&& Mathf.Abs(Mathf.RoundToInt(transform.position.x) - transform.position.x) < 0.05 && Mathf.Abs(Mathf.RoundToInt(transform.position.y) - transform.position.y) < 0.05)
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),0);
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
            if(index>=sprites.Length)
            {
                index = 0;
            }
            
            if(done)
            {
                done = false;
                needdetection = true;
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
        if (collision.name == "bag"||collision.name=="zombie")
        {
            Destroy(target);
            done = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name=="guard")
        {
            collision.collider.GetComponent<Guard>().destroy = false;
            if (destroy)
            {
                Destroy(gameObject);
            }
        }
        if(collision.collider.name=="zombie")
        {
            Destroy(target);
            done = true;
            target = null;
        }
    }
}
