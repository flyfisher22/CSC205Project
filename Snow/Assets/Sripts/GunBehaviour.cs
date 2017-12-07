using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour {
    public GameObject Snowball;
    float time = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot(float bSize)
    {
        time += Time.deltaTime;
        GameObject thisSB = Instantiate(Snowball, transform.position, transform.rotation);
        thisSB.transform.localScale += new Vector3(bSize, bSize, bSize);
        thisSB.GetComponent<SnowballScript>().size = bSize;
        Destroy(thisSB, 3);
    }
}
