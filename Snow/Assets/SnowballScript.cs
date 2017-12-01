using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

    void Update()
    {
        move();
    }

    void move()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag != "Player" && collision.collider.tag != "FiringPoint")
        {
            Destroy(gameObject);
        }
    }
}
