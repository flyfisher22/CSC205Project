using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour {

    // World Variables
    public GameObject Snowball;         // Snowball object
    float time = 0.0f;                  // Time passed since called
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // Method to Shoot Snowballs with a perameter of the size
    public void Shoot(float bSize)
    {
        time += Time.deltaTime;                                                                 //Time that has passed
        GameObject thisSB = Instantiate(Snowball, transform.position, transform.rotation);      // Create Snowball at the FiringPoint Position
        thisSB.transform.localScale += new Vector3(bSize, bSize, bSize);                        // Scales the Snowball size according to the size parameter
        thisSB.GetComponent<SnowballScript>().size = bSize;                                     // Passed the Snowball size value to the SnowballScript
        Destroy(thisSB, 3);                                                                     // Destorys Snowball after 3 seconds
    }
}
