using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{

    //Code for the behaviour of any character in the game (movement
    int speed = 3;

    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Forward() // move forward
    {
        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * speed;
    }

    public void Backward() //move backwards
    {
        gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * speed;
    }

    public void Left() // move left
    {
        gameObject.transform.Rotate(0,-180*Time.deltaTime,0);
    }

    public void Right() //move right
    {
        gameObject.transform.Rotate(0, 180*Time.deltaTime, 0);
    }

    public void Jump() //jump, move upwards
    {
        gameObject.transform.position += gameObject.transform.up * Time.deltaTime * speed * 5;
    }
}
