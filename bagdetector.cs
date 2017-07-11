using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagdetector : MonoBehaviour {

    // Use this for initialization
    public int direction;
    public bool stop;
    public Transform derive;
	void Start () {
        stop = false;
        
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        
         if (direction==1)
         {
             transform.Translate(1, 0, 0);
         }
         if (direction == 2)
         {
             transform.Translate(0, 1, 0);
         }
         if (direction == 3)
         {
            
             transform.Translate(-1, 0, 0);
         }
         if (direction == 4)
         {

             transform.Translate(0, -1, 0);
         }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.name=="bag"||collision.name=="zombie")
        {
            derive.gameObject.GetComponent<Guard>().index = 0;
            derive.gameObject.GetComponent<Guard>().move = true; 
            derive.gameObject.GetComponent<Guard>().direction = direction;
            derive.gameObject.GetComponent<Guard>().target = collision.gameObject;
        }
        if (collision.name != "bagdetector" && collision.name != "zombiedetector" &&collision.name!="Model"&& collision.transform.position != derive.position)
        {
            Destroy(gameObject);
        }
    }
}
