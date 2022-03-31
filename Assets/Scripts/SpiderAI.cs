using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
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

    private float AttackCooldownTime = 2;
    private float NextAttackTime = 0;
    private float DamageValue = 0.5f;

    private float health = 2f;

    private float knockbackTime = 1000f;

    private float counter = 1000f;

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
        if (counter >= knockbackTime)
        {
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
        else
        {
            GoAway();
        }
        counter += 1;
    }

    public void TakeDamage(float damage, float knockback)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        //else
            //StartCoroutine(KnockBackCo(gameObject.GetComponent<Rigidbody>()));
    }

    private IEnumerator KnockBackCo(Rigidbody rb)
    {
        if (rb != null)
        {
            GoAway();
            yield return new WaitForSeconds(knockbackTime);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            setcounterto0();
        }
    }

    public void setcounterto0()
    {
        counter = 0;
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

    public void GoAway()
    {
        Vector3 dirtoPlayer = transform.position - target.transform.position;

        Vector3 newPos = transform.position + dirtoPlayer;

        agent.SetDestination(newPos);
    }

    void AttackPlayer()
    {
        
        transform.LookAt(target);

        //---enemy attacks---
        if(Time.time > NextAttackTime)
        {
            player.TakeDamage(DamageValue);
            NextAttackTime = Time.time + AttackCooldownTime;
        }

        //Temporarily here for testing respawning
        //Enemy shouldn't die when it attacks
        //Die();

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
