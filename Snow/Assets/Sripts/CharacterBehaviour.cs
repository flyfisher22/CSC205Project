using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour
{

    //Code for the behaviour of any character in the game (movement
    public int speed = 20;                                                                  //Speed of character movement
    public float health = 100;                                                              //Character health
    public Slider slider;                                                                   //Character health bar


    // Use this for initialization
    void Start()
    {
        health = 100;                                                                       //Sets the health of the player to 100
        if (gameObject.tag == "Enemy")                                                      //Puts enemy health bar in world space canvas
        {
            slider.transform.parent = GameObject.Find("HealthCanvas").transform;
        }
        else if (gameObject.tag == "Player")                                                //Puts player health bar in screen space canvas
        {
            slider.transform.parent = GameObject.Find("Canvas").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;                                                              //Sets the health slider value to health value
        if (gameObject.tag == "Enemy")                                                      //Puts enemy health bar above enemy's head
        {
            slider.transform.position = transform.position + transform.up * 10;
        }
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
        gameObject.transform.position -= gameObject.transform.right * Time.deltaTime * speed;
    }

    public void Right() //move right
    {
        gameObject.transform.position += gameObject.transform.right * Time.deltaTime * speed;
    }

    public void Jump() //jump, move upwards
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 400);
    }
}
