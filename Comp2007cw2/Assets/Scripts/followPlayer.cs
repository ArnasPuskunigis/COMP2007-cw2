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

    public void shootCannonBall()
    {
        Instantiate(cannonBall, shootPoint.position, shootPoint.rotation, cannonBallParent);
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        cannonBallParent = GameObject.Find("cannonBalls").transform;
        pauseSystem = GameObject.Find("pauseManager").GetComponent<pauseManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pauseSystem.gamePaused)
        {
            enemy.speed = 0;
        }
        else
        {
            enemy.speed = 10;
        }

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
            timeSinceShot = 0f;
        }

        if (Vector3.Distance(transform.position, player.position) >= standingDistance && Vector3.Distance(transform.position, player.position) <= triggerDistance && !pauseSystem.gamePaused)
        {

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
            enemy.ResetPath();
            enemy.speed = 0;
        }

    }
}
