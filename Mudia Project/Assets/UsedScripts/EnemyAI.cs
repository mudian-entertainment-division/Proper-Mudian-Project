using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    //desiginates who the player is
    public Transform target;
    public float curHealth, maxHealth, moveSpeed, attackRange, attackSpeed, noiseRange, sense;
    public NavMeshAgent agent;
   
    //gives distances for how far away the player will be when they switch behaviour
    public float dist, sightDist;
    
    public GameObject self;
    //public GameObject playerShadow;
    public float turnSpeed;
    public float sightAngle;
    public Movement movement;
    public Transform waypointParent;
    private Transform[] points;
    public float waypointDistance;
    public int currentWayPoint = 1;
    public Rigidbody rigid;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y; 
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    void Start()
    {
        //sets the conditions at the start
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = self.GetComponent<NavMeshAgent>();
        points = waypointParent.GetComponentsInChildren<Transform>();
        sightDist = 100f;
        sightAngle = 90f;
    }
    void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, sightDist, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target
        }
        noiseRange = movement.noise * sense;
        //kills the enemy when they lose all their health
        if (curHealth <= 0)
        {
           
        }
        //moves the enemy when the player is alive
        if (PlayerHandler.isDead == false)
        {
            dist = Vector3.Distance(target.position, transform.position);

            if (curHealth == 0)
            {
                return;
            }
            //attacks the player if they get too close
            else if (dist <= attackRange)
            {
                Debug.Log("Attack");
               
            }
            //follows the player when they see him
            else if (dist <= noiseRange)
            {
                agent.destination = target.position;
               
            }


            else
            {
                if (points.Length == 0)
                    return;

                // Set the agent to go to the currently selected destination.
                agent.destination = points[currentWayPoint].position;
                //transform.position = Vector3.MoveTowards(transform.position, points[currentWayPoint].position, 1f);
                if (transform.position.x == agent.destination.x && transform.position.z == agent.destination.z)
                {
                    if (currentWayPoint < points.Length - 1)
                    {
                        currentWayPoint++;
                    }
                    else
                    {
                        //resets the waypoints to the begining
                        currentWayPoint = 0;
                    }

                }

            }
        }

    }
}
