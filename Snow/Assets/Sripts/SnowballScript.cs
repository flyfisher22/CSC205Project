using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour {
    public float size;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame

    void Update()
    {
        move();         // Call to the movement method
    }


    // Method that controls the movement of the Snowball object
    void move()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;      // Moves the Snowball forwards at a rate of 10
    }



    // Method that is called when the Snowball Collides with another ingame Object
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);  // Sends the tag of the object to the debuger
        if (collision.collider.tag != "Player" && collision.collider.tag != "FiringPoint")      //If the Snowball doesn't collide with the Player or Firing Point
        {
            Destroy(gameObject);        // Destroy the object
        }
        if (collision.collider.tag == "Terrain")        // If the SnowBall collides with the Terrain
        {
            // Call method Grow from the TerrainBehaviour Script with the parameters of the Snowball position and size
            collision.gameObject.GetComponent<TerrainBehaviour>().Grow(size, transform.position.x, transform.position.z) ;          

        }
    }
}
