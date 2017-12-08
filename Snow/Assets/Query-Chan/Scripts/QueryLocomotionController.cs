using UnityEngine;
using System.Collections;

public class QueryLocomotionController : MonoBehaviour {

    public AudioClip jump;
    public AudioClip attacked;
    public AudioClip walk;
    float jumpTime = 0;
    float attackTime = 0;
    float walkTime =0;

    //--------------------------
    Animator animator;
    AudioSource audioSource;

    //========================================================

    void Start()
	{
		animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


	//=========================================================

	void Update()
	{
		updateMove();
	}


	void updateMove()
	{
        bool jumped = false;
        bool attack = false;
        float speed = 0;
        jumpTime -= Time.deltaTime;
        attackTime -= Time.deltaTime;
        walkTime -= Time.deltaTime;
        bool AttackPlayed = false;
        bool WalkPlayed = false;
        bool JumpPlayed = false;

        if (Input.GetKey(KeyCode.W))
        {
            if (walkTime <= .5)
            {
                WalkPlayed = true;
                walkTime = 1;
            }
            speed = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (walkTime <= .5)
            {
                WalkPlayed = true;
                walkTime = 1;
            }
            speed = -1;
        }
        if (Input.GetKey(KeyCode.LeftShift) && speed <= 1)
        {
            speed = 2;
        }
        if (Input.GetKey(KeyCode.Space) && jumpTime <= 0)
        {
           jumpTime = 2;
            JumpPlayed = true;
           jumped = true;
        }
        if (Input.GetKey(KeyCode.Mouse0) && attackTime <= 0)
        {
            attackTime = 1;
            AttackPlayed = true;
            attack = true;
        }

        //animation and sound ---------------------------------------

        animator.SetBool("Jump", jumped);
        if (jumped && JumpPlayed && jumpTime == 2)
        {
            JumpPlayed = false;
            AudioSource.PlayClipAtPoint(jump, transform.position);
        }
        animator.SetBool("Attack", attack);
        if (attack && AttackPlayed && attackTime == 1)
        {
            AttackPlayed = false;
            AudioSource.PlayClipAtPoint(attacked, transform.position);
        }
        if (jumped == false && animator.IsInTransition(0) == false) {
			animator.SetFloat("Speed", speed);
            if (speed != 0 && WalkPlayed && walkTime == 1)
            {
                WalkPlayed = false;
                AudioSource.PlayClipAtPoint(walk, transform.position);
            }
        }



    }

	//=====================================================================

	


}

