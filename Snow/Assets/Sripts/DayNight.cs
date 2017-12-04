using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    float MovementSpeed =2;

	void Update () {
        if (transform.rotation.eulerAngles.x >= 200 || transform.rotation.eulerAngles.x <= -15)
        {
            transform.Rotate(5 * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(MovementSpeed * Time.deltaTime, 0, 0);
        }
    }
}
