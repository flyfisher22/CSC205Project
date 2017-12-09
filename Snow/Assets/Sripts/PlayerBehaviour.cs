using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    // World Variables
    GameObject camera;          // FPS  camera attached to the player
    GameObject FiringPoint;     // Point where the snowball will be initalized
    float snowSize = 0;         // Size of Snowball
    public Text SnowSize;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("PlayerCam");         // Finds FPS Camera
        FiringPoint = GameObject.Find("FPP");                           // Finds FiringPoint of Player
        SnowSize = GameObject.Find("SnowSize").GetComponent<Text>();
        Cursor.lockState = CursorLockMode.Locked;                       // Locks the cursor
    }
	
	// Update is called once per frame
	void Update () {
        Move();                                 // Calls Player Movement Method
        CameraMovement();                       // Calls Camer Movement Method
        if (Input.GetMouseButton(0))            // If the left mouse button is pressed
            {
                snowSize += Time.deltaTime;     // Increase size of snowball
            if (snowSize > 3)
            {
                snowSize = 3;                   // Limits the size of Snowballs to 3
            }
                Debug.Log(snowSize);            // Show size of snowball in Log

            SnowSize.text = "Snowball Size = " + snowSize;

        }

        if (Input.GetMouseButtonUp(0))          // When the left mouse button is released
        {                                       
            Fire();                             // Calls the Firing Method
            snowSize = 0;                       // Resets the Snowball Size for the next snowball
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None; // Frees the Mouse

        }


    }

    // Movement Method, checks for input through WASD and Spacebar keys and moves players accordingly
    void Move()
    {
        if (Input.GetKey("w"))         // if the W keys is pushed down
        {
            gameObject.GetComponent<CharacterBehaviour>().Forward();        // Calls method in the CharacterBehaviour script to move forward
        }
        if (Input.GetKey("a"))          // If the A key is pushed down
        {
            gameObject.GetComponent<CharacterBehaviour>().Left();           // Calls method in the CharacterBehaviour script to move to the left
        }
        if (Input.GetKey("s"))          // If the S key is pushed down
        {
            gameObject.GetComponent<CharacterBehaviour>().Backward();       // Calls method in the CharacterBehaviour script to move to backwards
        }
        if (Input.GetKey("d"))          // If the D key is pushed down
        {
            gameObject.GetComponent<CharacterBehaviour>().Right();          // Calls method in the CharacterBehaviour script to move to the right
        }
        if (Input.GetKey("space"))      // If the Spacebar is pushed down
        {
            gameObject.GetComponent<CharacterBehaviour>().Jump();           // Calls method in the CharacterBehaviour script to move jump
        }
    }
   
    // Method to Fire a Snowball
    void Fire()
    {
        FiringPoint.GetComponent<GunBehaviour>().Shoot(snowSize);       // Finds Player's Firing Point and calls a shoot method in the GunBehaviour script with the parameter of the snowball's size
    }


    // Camera Movement Method, controls how the camera moves in responce to input from the player's mouse

    float lrSpeed = 3;      // Speed in which the camera rotates left and right
    float udSpeed = 4;      // Speed in which the camera rotates up and down
    float pitch = 0.0f;     // Amount of Rotation up and down
    float yaw = 0.0f;       // Amount of Rotation left and right
    float edge = 40.0f;     // Amount of space from the left and right edge of the screen

    void CameraMovement()
    {
        yaw += lrSpeed * Input.GetAxis("Mouse X");                          // Adds the speed of camera rotation multiplied by the movement of the mouse to the yaw variable (left and right)
        pitch -= udSpeed * Input.GetAxis("Mouse Y");                        // Adds the speed of camera rotation multiplied by the movement of the mouse to the pitch variable (up and down)

        if (Input.mousePosition.x < 0 + edge)                               // If the mouse is near the left edge of the screen
        {
            yaw -= lrSpeed * 30 * Time.deltaTime;                           // Decrease the Yaw Variable
        }
        if (Input.mousePosition.x > Screen.width - edge)                    // If the mouse is near the right edge of the screen
        {
            yaw += lrSpeed * 30 * Time.deltaTime;                           // Increase the Yaw Variable 
        }
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);               // Rotate the Player to the left and right according to the yaw variable
        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);       // Rotate the Camera right, left, up, and down according to the yaw and pitch variables

    }
}
