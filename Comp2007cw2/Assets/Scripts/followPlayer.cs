using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followPlayer : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform player;
    public Transform rotationBase;

    public Transform shootPoint;
    public Transform cannonBallParent;

    public float standingDistance;
    public float triggerDistance;

    public GameObject cannonBall;
    public pauseManager pauseSystem;

    public float shootInterval;
    public float timeSinceShot;

    public AudioSource cannonShot;

    public void shootCannonBall()
    {
        //Spawn the cannonball and play the shooting sound effect
        Instantiate(cannonBall, shootPoint.position, shootPoint.rotation, cannonBallParent);
        cannonShot.Play();
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Get objects and components at the start 
        player = GameObject.Find("Player").transform;
        cannonBallParent = GameObject.Find("cannonBalls").transform;
        pauseSystem = GameObject.Find("pauseManager").GetComponent<pauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set the enemy speed to 0 if the game is paused
        if (pauseSystem.gamePaused)
        {
            enemy.speed = 0;
        }
        else
        {
            enemy.speed = 10;
        }

        // Allow the enemy to shoot only if the interval between shots (reload) has been reached, and if the game has not been paused
        if (!pauseSystem.gamePaused)
        {
            timeSinceShot += Time.deltaTime;
            if(timeSinceShot >= shootInterval)
            {
                shootCannonBall();
                timeSinceShot = 0f;
            }
        }
        else
        {
            //Otherwise reset the reload timer
            timeSinceShot = 0f;
        }

        //Check the distance between the player and the enemy and check if to stand or keep advancing towards the player and make sure the game is not paused
        if (Vector3.Distance(transform.position, player.position) >= standingDistance && Vector3.Distance(transform.position, player.position) <= triggerDistance && !pauseSystem.gamePaused)
        {
            //Move towards the player and look at the player with the cannon
            Vector3 relativePosition = player.position - rotationBase.position;
            relativePosition.y = 0;

            float angleToPlayer = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

            angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;

            Quaternion targetRotation = Quaternion.Euler(0f, angleToPlayer, 0f);
            rotationBase.rotation = targetRotation;

            enemy.speed = 10;
            enemy.SetDestination(player.position);
        }
        else if (Vector3.Distance(transform.position, player.position) <= standingDistance && !pauseSystem.gamePaused)
        {
            //DO NOT move towards the player and look at the player with the cannon
            Vector3 relativePosition = player.position - rotationBase.position;
            relativePosition.y = 0;

            float angleToPlayer = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

            angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;

            Quaternion targetRotation = Quaternion.Euler(0f, angleToPlayer, 0f);
            rotationBase.rotation = targetRotation;

            enemy.ResetPath();
            enemy.speed = 0;
        }

        if (pauseSystem.gamePaused)
        {
            //If the game is paused then the stop the enemy and set speed to 0
            enemy.ResetPath();
            enemy.speed = 0;
        }

    }
}
