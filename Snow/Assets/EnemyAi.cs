using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour {
    public enum State { Patrolling, Attacking, Fleeing };
    public State state = State.Patrolling;
    public GameObject enemy;
    Vector3 playerMove;
    Vector3 oldPos;
    Vector3 center = new Vector3(59.8f, 0, 50.6f);
    float distance;
    float attackdist = 15;
    bool hit = false;
    Vector3 direction = new Vector3(-24.98f, 0, 2.28f);
    Transform thisEnemy;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        thisEnemy = gameObject.transform;
        oldPos = player.transform.position;
        distance = (player.transform.position - transform.position).magnitude;

        //InvokeRepeating("ChangeDirection", 0.0f, 2.0f);
        //InvokeRepeating("fire", 0.0f, 3.0f);
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
                    break;
                }
        }
    }

    void ChangeDirection()
    {
        switch (state)
        {
            case State.Patrolling:
                {
                    do
                    {
                        direction = Random.insideUnitSphere;
                    } while (Vector3.Dot(direction, center - transform.position) < 0);
                    break;
                }
            case State.Attacking:
                {
                    do
                    {
                        direction = Random.insideUnitSphere;
                    } while (Vector3.Dot(direction, player.transform.position - transform.position) < 0);
                    break;
                }
            case State.Fleeing:
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
        if (player == null)
        {
            player = GameObject.Find("Player");
            thisEnemy = gameObject.transform;
        }

        playerMove = player.transform.position - oldPos;
        oldPos = player.transform.position;

        //GetComponent<CharacterBehaviour>().Forward();
        distance = (player.transform.position - transform.position).magnitude;

        Debug.Log(state);

        switch (state)
        {
            case State.Patrolling:
                {
                    if (distance <= attackdist)
                    {
                        RaycastHit rayHit;
                        Physics.Raycast(transform.position + transform.up, player.transform.position - (transform.position + transform.up), out rayHit);
                        Debug.Log(rayHit.collider);
                        if (rayHit.collider.tag == "Player")
                        {
                            state = State.Attacking;
                        }
                    }
                    break;
                }

            case State.Attacking:
                {
                    RaycastHit rayHit;
                    Physics.Raycast(transform.position + transform.up, player.transform.position - (transform.position + transform.up), out rayHit);

                    Vector3 cannonDir = player.transform.position - transform.position;
                    direction = player.transform.position - transform.position;
                    transform.forward = direction;
                    if (distance <= attackdist)
                    {
                        hit = false;
                    }
                    if (distance >= attackdist || (rayHit.collider.tag != "Player" && rayHit.collider.tag != "Enemy"))
                    {
                        state = State.Patrolling;
                    }
                    break;
                }

            case State.Fleeing:
                {
                    hit = false;
                    if (distance >= attackdist)
                    {
                        state = State.Patrolling;
                    }
                    break;
                }
        }
    }
}
