using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    GameObject camera;
    GameObject FiringPoint;
	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        FiringPoint = GameObject.FindGameObjectWithTag("FiringPoint");
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Move()
    {
        if (Input.GetKey("w"))
        {
            gameObject.GetComponent<CharacterBehaviour>().Forward();
        }
        if (Input.GetKey("a"))
        {
            gameObject.GetComponent<CharacterBehaviour>().Left();
        }
        if (Input.GetKey("s"))
        {
            gameObject.GetComponent<CharacterBehaviour>().Backward();
        }
        if (Input.GetKey("d"))
        {
            gameObject.GetComponent<CharacterBehaviour>().Right();
        }
        if (Input.GetKey("space"))
        {
            gameObject.GetComponent<CharacterBehaviour>().Jump();
        }
    }
   
    void Fire()
    {
        FiringPoint.GetComponent<GunBehaviour>().Shoot();
    }

}
