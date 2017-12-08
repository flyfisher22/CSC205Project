using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    GameObject camera;
    GameObject FiringPoint;
    float snowSize = 0;
	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("PlayerCam");
        FiringPoint = GameObject.Find("FPP");
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        CameraMovement();
        if (Input.GetMouseButton(0))
            {
                snowSize += Time.deltaTime;
                Debug.Log(snowSize);
            }
        if (Input.GetMouseButtonUp(0))
        {
            Fire();
            snowSize = 0;
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
        FiringPoint.GetComponent<GunBehaviour>().Shoot(snowSize);
    }

    float lrSpeed = 2;
    float udSpeed = 3;
    float pitch = 0.0f;
    float yaw = 0.0f;
    float edge = 1.0f;
    void CameraMovement()
    {
        yaw += lrSpeed * Input.GetAxis("Mouse X");
        pitch -= udSpeed * Input.GetAxis("Mouse Y");
        if (Input.mousePosition.x < 0 - edge)
        {
            yaw -= lrSpeed * 30 * Time.deltaTime;
        }
        if (Input.mousePosition.x > Screen.width + edge)
        {
            yaw += lrSpeed * 30 * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}
