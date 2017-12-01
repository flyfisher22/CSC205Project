using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    // Use this for initialization
    GameObject player;

	void Start () {
        player = GameObject.Find("Player");
        Debug.Log(player);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position + transform.forward * -10 + transform.up * 2;
	}
}
