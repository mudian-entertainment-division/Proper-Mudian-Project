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
    
    public float turnSpeed;
    public float angle;
    public Movement movement;
  
    
    void Start()
    {
        //sets the conditions at the start
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = self.GetComponent<NavMeshAgent>();

       
    }
    void Update()
    {
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
              
                //  myAI.Patrol();
                //timeTillNextFire = 0;
            }
        }

    }
}
