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

    public void Shoot()
    {
        time += Time.deltaTime;
        GameObject thisSB = Instantiate(Snowball, transform.position, transform.rotation);
    }
}
