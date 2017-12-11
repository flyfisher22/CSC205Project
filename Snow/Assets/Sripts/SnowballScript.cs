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
        transform.position += transform.forward * 40 * Time.deltaTime;      // Moves the Snowball forwards at a rate of 40
    }



    // Method that is called when the Snowball Collides with another ingame Object
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.tag != gameObject.tag)
        {
            if (collision.collider.transform.root.tag == "Player" || collision.collider.transform.root.tag == "Enemy")
            {
                collision.collider.GetComponent<CharacterBehaviour>().health -= size * 10f;
                Debug.Log("Ow!");
            }
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Terrain")
        {
            collision.gameObject.GetComponent<TerrainBehaviour>().Grow(size, transform.position.x, transform.position.z);

        }
    }
}
