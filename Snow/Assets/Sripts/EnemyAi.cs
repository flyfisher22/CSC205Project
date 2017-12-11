using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public enum State { Patrolling, Attacking, Fleeing };                                               //State machine
    public State state = State.Patrolling;

    Vector3 playerMove;
    Vector3 oldPos;
    Vector3[] positions = {new Vector3(108.7f, 57.88f, 116.7f), new Vector3(168.9f, 57.88f, 253.4f),    //Postions for AI patrolling
                           new Vector3(107f, 57.88f, 385.4f), new Vector3(407.6f, 57.88f, 385.4f),
                           new Vector3(348f, 57.88f, 256.4f), new Vector3(386.9f, 57.88f, 103.2f),};
    Vector3 center = new Vector3(108.7f, 57.88f, 116.7f);                                               //Begining AI patrol target

    Vector3 direction = new Vector3(-24.98f, 0, 2.28f);                                                 //Original direction

    float distance;
    float attackdist = 100;                                                                             //Attack distance
    float fleedist = 50;                                                                                //Flee distance
    float healthThresh = 30f;                                                                           //Flee health threshold

    bool shoot = false;

    float snowSize;                                                                                     //Snowball size
    float randTime;                                                                                     //Randomly chosen snowball size
    float attackTimeori = 0.5f;                                                                         //Attack cycle time
    float attackTime = 0.5f;
    bool attackAnim = false;

    Transform FiringPoint;
    GameObject player;
    public GameObject enemy;

    Animator animator;

    void Start()
    {
        player = GameObject.Find("Player");                                                             //Finds the player
        oldPos = player.transform.position;                                                             //Gets players postion from last frame
        distance = (player.transform.position - transform.position).magnitude;                          //Gets distance of player from enemy
        FiringPoint = gameObject.transform.Find("FPE");                                                 //Finds the firing point

        animator = transform.root.GetComponentInChildren<Animator>();                                   //Finds the animator
        animator.SetFloat("Speed", 1f);                                                                 //Starts walking animation

        InvokeRepeating("ChangeDirection", 0.0f, 3.0f);                                                 //Changes enemy direction every 3 seconds
    }

    public void getHit()
    {

    }

    void fire()
    {
        switch (state)
        {
            case State.Attacking:
                {
                    direction = player.transform.position - transform.position;                         //Direction of player
                    LookToward(direction);                                                              //Aims at the direction

                    Vector3 cannonDir = (player.transform.position - transform.position).normalized;    //Cannon direction
                    transform.Find("FPE").forward = cannonDir + (playerMove * (distance/40));           //Aims cannon in players direction relative to movement

                    FiringPoint.GetComponent<GunBehaviour>().Shoot(snowSize, gameObject.tag);           //Fires the cannon

                    snowSize = 0;                                                                       //Resets the snowball size
                    attackAnim = true;                                                                  //Starts the attack animation

                    break;
                }
        }
    }

    void ChangeDirection()
    {
        center = positions[Random.Range(0, 5)];
        switch (state)
        {
            case State.Patrolling:              //Sets the direction during patrolling
                {
                    do
                    {
                        direction = Random.insideUnitSphere;
                    } while (Vector3.Dot(direction, center - transform.position) < 0);
                    break;
                }
            case State.Attacking:               //Sets the direction during attacking
                {
                    do
                    {
                        direction = Random.insideUnitSphere;
                    } while (Vector3.Dot(direction, player.transform.position - transform.position) < 0);
                    break;
                }
            case State.Fleeing:                 //Sets the direction during fleeing
                {
                    do
                    {
                        direction = Random.insideUnitSphere;
                    } while (Vector3.Dot(direction, transform.position - player.transform.position) < 0);
                    break;
                }
        }
    }

    void Update()
    {
        if(animator == null)                    //Finds animator again after death
        {
            animator = transform.root.GetComponentInChildren<Animator>();
        }
        if (player == null)                     //Finds player again after player or enemy death
        {
            player = GameObject.Find("Player");
        }

        animator.SetBool("Attack", attackAnim); //Make attack animation play while attacking
        attackAnim = false;                     //Turns animation off when not attacking

        playerMove = (player.transform.position - oldPos).normalized;                   //Finds player direction for cannon aiming
        oldPos = player.transform.position;                                             //Used to find player direciton

        if (transform.root.GetComponent<CharacterBehaviour>().health <= 0)              //Behaviour for enemy death
        {
            GameObject newEnemy = Instantiate(enemy, positions[Random.Range(0, 5)], transform.rotation);
            newEnemy.name = "Enemy";
            Destroy(gameObject);
        }

        direction = new Vector3(direction.x, 0, direction.z);                           //Makes enemy not rotate in the up direction
        LookToward(direction);                                                          //Rotates enemy

        GetComponent<CharacterBehaviour>().Forward();                                   //Makes the enemy move

        distance = (player.transform.position - transform.position).magnitude;          //Finds the distance of the player from the enemy

        switch (state)
        {
            case State.Patrolling:
                {
                    if (transform.root.GetComponent<CharacterBehaviour>().health < 100) //Heals the player when health isn't full
                    {
                        transform.root.GetComponent<CharacterBehaviour>().health += 2 * Time.deltaTime;
                        if (transform.root.GetComponent<CharacterBehaviour>().health > 100)
                        {
                            transform.root.GetComponent<CharacterBehaviour>().health = 100;
                        }
                    }

                    transform.Find("FPE").forward = transform.forward;                  //Makes cannon face in the direction of the enemy
                    
                    if (distance <= attackdist)                                         //If player is in range
                    {
                        if (transform.root.GetComponent<CharacterBehaviour>().health >= healthThresh)       //If health is above flee threshold
                        {
                            RaycastHit rayHit;                                          //Raycast to find player
                            Physics.Raycast(transform.position + transform.up*10, player.transform.position - (transform.position + transform.up*10), out rayHit);
                            if (rayHit.collider.transform.root.tag == "Player")         //If the collision found is the player
                            {
                                state = State.Attacking;                                //Begin attacking
                            }
                        }
                        else if (distance <= fleedist)                                  //If health below threshold and in flee distance
                        {
                            state = State.Fleeing;                                      //Begin fleeing
                        }
                    }
                    break;
                }

            case State.Attacking:
                {

                    attackTime -= Time.deltaTime;                                       //Begins charging snowball after 0.5 seconds
                    if (attackTime <= 0)
                    {
                        randTime = Random.Range(0.5f, 3f);                              //Charges snowball between size 0.5 and 3
                        snowSize = randTime;
                        attackTime = attackTimeori + randTime;
                        shoot = true;
                    }
                    if (randTime > 0 && shoot == true)                                  //Begin charging snowball
                    {
                        randTime -= Time.deltaTime;
                    }
                    if (randTime <= 0)                                                  //Fire snowball
                    {
                        fire();
                        shoot = false;
                        randTime = 1f;
                    }

                    RaycastHit rayHit;                                                  //Raycast to check if player in sight
                    Physics.Raycast(transform.position + transform.up*10, player.transform.position - (transform.position + transform.up*10), out rayHit);

                    Vector3 cannonDir = player.transform.position - transform.position; //Direction of cannon
                    transform.Find("FPE").forward = cannonDir + (playerMove * distance);//Aims cannon in players direction relative to movement

                    if (distance >= attackdist || (rayHit.collider.transform.root.tag != "Player" && rayHit.collider.transform.root.tag != "Enemy"))
                    {
                        state = State.Patrolling;                   //Begin Patrolling if out of range or out of sight
                    }
                    if (transform.root.GetComponent<CharacterBehaviour>().health <= healthThresh)
                    {
                        state = State.Fleeing;                      //Begin fleeing if health less than flee threshold
                    }
                    break;
                }

            case State.Fleeing:
                {
                    transform.Find("FPE").forward = transform.forward;                  //Makes cannon face in direciton of enemy
                    if (distance >= fleedist)                                           //If out of flee distance
                    {
                        state = State.Patrolling;                                       //Begin patrolling
                    }
                    break;
                }
        }
    }
    public void LookToward(Vector3 dir)                                                 //Aims in the direction
    {
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
    }

    void OnCollisionExit(Collision collision)                                           //Stops enemies from flying
    {
        if(collision.collider.tag == "Terrain")
        {
            gameObject.GetComponent<Rigidbody>().mass = 10000;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            gameObject.GetComponent<Rigidbody>().mass = 1;
        }
    }
}
