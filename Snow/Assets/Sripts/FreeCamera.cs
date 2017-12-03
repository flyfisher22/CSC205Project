using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour {
    float MovementSpeed = 50;

	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.forward * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= (transform.forward * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += (transform.up * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= (transform.up * Time.deltaTime * MovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -180* Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 180 * Time.deltaTime, 0);
        }

    }
}
