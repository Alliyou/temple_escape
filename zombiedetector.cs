using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiedetector : MonoBehaviour {

    // Use this for initialization
    
    public int direction;
    public bool stop;
    public GameObject derive;
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (direction == 1)
        {
            transform.Translate(0.8f, 0, 0);
        }
        if (direction == 2)
        {
            transform.Translate(0, 0.8f, 0);
        }
        if (direction == 3)
        {
            transform.Translate(-0.8f, 0, 0);
        }
        if (direction == 4)
        {
            transform.Translate(0, -0.8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="meat")
        {
            derive.GetComponent<Zombie>().index = 0;
            derive.GetComponent<Zombie>().move = true;
            derive.GetComponent<Zombie>().direction = direction;
            derive.GetComponent<Zombie>().targettrans = collision.transform;
        }


        if (collision.name != "bagdetector" && collision.name != "zombiedetector" && derive!=null&&collision.transform.position != derive.transform.position)
            Destroy(gameObject);
    }
}
