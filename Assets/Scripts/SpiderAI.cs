using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public delegate void SpiderKilled();
    public static event SpiderKilled OnSpiderKilled;

    public float lookRadius = 10f;
    public float roamTargetDistance = 3f;
    Vector3 roamPoint;
    bool RoamPointSet = false;
    

    Transform target;
    NavMeshAgent agent;

    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer >= lookRadius)
        {
            Roam();
        }
        if (distanceToPlayer <= lookRadius && distanceToPlayer >= agent.stoppingDistance)
        {
            ChasePlayer();
        }
        if (distanceToPlayer <= agent.stoppingDistance)
        {
            AttackPlayer();
        }
    }
    void Roam()
    {
        if (!RoamPointSet)
        {
            float randomX = Random.Range(-roamTargetDistance, roamTargetDistance);
            float randomZ = Random.Range(-roamTargetDistance, roamTargetDistance);

            roamPoint = new Vector3(
                transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ);
            RoamPointSet = true;
        }
        if (RoamPointSet)
        {
            agent.SetDestination(roamPoint);

            Vector3 distanceToRoamPoint = transform.position - roamPoint;
            if (distanceToRoamPoint.magnitude < 1f)
            {
                RoamPointSet = false;
            }
        }


    }
    void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    void AttackPlayer()
    {
        transform.LookAt(target);
        //---enemy attacks---

        //Temporarily here for testing respawning
        //Enemy shouldn't die when it attacks
        Die();

        //player.StopGame();
    }

    void Die()
    {
        Destroy(gameObject);
        if(OnSpiderKilled != null)
        {
            OnSpiderKilled();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
