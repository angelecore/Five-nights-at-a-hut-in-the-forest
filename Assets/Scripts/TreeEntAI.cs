using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeEntAI : MonoBehaviour
{
    //targetBoardSpawner - parent object, should contain whatever is needed
    barricadeScript targetBoardSpawner;
    Transform targetWindow;
    Transform targetPlayer;
    Transform currentTarget;
    Transform insideTarget;
    bool ImInside = false;
    NavMeshAgent agent;

    public PhaseSwitchTrigger phaseSwitch;
    public float attackTimer;

    public float distanceToTarget;
    public float test;

    private bool ImActive = false;
    private bool ActivationInitiated = false;

    // how many times it was hit currently
    private int health = 3;
    private int maxHealth = 3;
    private bool stun = false;

    public bool attack = false;

    public Player player;
    private float DamageValue = 1f;
    // Start is called before the first frame update

    AudioSource damaged;
    void Start()
    {
        //target - the child object for AI to move to
        targetWindow = GetTarget(out targetBoardSpawner, out insideTarget);
        targetPlayer = GameObject.Find("Player").transform;
        currentTarget = targetWindow;
        agent = GetComponent<NavMeshAgent>();
        damaged = GetComponentInChildren<AudioSource>();
    }

    public float GetHealth()
    {
        return health;
    }

    // Update is called once per frame
    void Update()
    {
        if (stun == false)
        {
            if (phaseSwitch.IsItNight)
            {
                if (ImActive)
                {
                    if (!ImInside)
                    {
                        if (targetBoardSpawner.boards > 0)
                        {
                            currentTarget = targetWindow;
                        }
                        else
                        {
                            if (Vector3.Distance(insideTarget.position, transform.position) - 2 <= agent.stoppingDistance)
                            {
                                currentTarget = targetPlayer;
                                agent.stoppingDistance = 2;
                                ImInside = true;
                            }
                            else
                            {
                                currentTarget = insideTarget;
                            }
                        }
                    }

                    distanceToTarget = Vector3.Distance(currentTarget.position, transform.position);

                    agent.SetDestination(currentTarget.position);

                    test = agent.stoppingDistance;

                    if (distanceToTarget-1 <= agent.stoppingDistance)
                    {
                        FaceTarget();
                        attackTimer -= Time.deltaTime;
                        if (attackTimer <= 0)
                        {
                            attack = true;
                            attackTimer = 4;
                            if(currentTarget == targetPlayer)
                            {
                                player.TakeDamage(DamageValue);
                            }
                        }

                    }

                }
                else if (!ActivationInitiated)
                {
                    ActivationInitiated = true;
                    StartCoroutine(DelayedActivate());
                }
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            //if stunned stayes in one postion
            agent.SetDestination(gameObject.transform.position);
        }
    }
    void OnTriggerStay(Collider collide)
    {
        if (attack == true)
        {
            if (collide.gameObject.tag == "boardSpawner")
            {
                if (collide.gameObject.GetComponent<Renderer>().enabled)
                {                   
                    if (attack)
                    {
                        collide.GetComponent<Collider>().SendMessageUpwards("removeBarricade", SendMessageOptions.RequireReceiver);
                        attack = false;
                    }
                }
                else
                {
                    attack = false;
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        damaged.Play();
        if (health <= 0)
        {
            stun = true;
            player.Entonkill();
            StartCoroutine(Stun());
        }
    }

    public bool GetStunValue()
    {
        return stun;
    }
    IEnumerator DelayedActivate()
    {
        //random spawn delay between 3 - 15 seconds
        int delay = Mathf.RoundToInt(Random.Range(1f, 15f));
        yield return new WaitForSeconds(delay);

        ImActive = true;
    }

    IEnumerator Stun()
    {
        //random stun duration between 1 - 5 seconds
        int delay = Mathf.RoundToInt(Random.Range(1f, 5f));
        yield return new WaitForSeconds(delay);
        health = maxHealth;
        stun = false;

    }

    private Transform GetTarget(out barricadeScript targetBoardSpawner, out Transform insideTarget)
    {
        Transform target = BoardSpawnerTracker.instance.boardSpawnerTarget.transform;
        float distToTarget = Vector3.Distance(target.position, transform.position);
        targetBoardSpawner = BoardSpawnerTracker.instance.boardSpawner;
        insideTarget = BoardSpawnerTracker.instance.insideTarget.transform;

        Transform target1 = BoardSpawnerTracker.instance.boardSpawnerTarget1.transform;
        float distToTarget1 = Vector3.Distance(target1.position, transform.position);
        barricadeScript tBS1 = BoardSpawnerTracker.instance.boardSpawner1;

        Transform target2 = BoardSpawnerTracker.instance.boardSpawnerTarget2.transform;
        float distToTarget2 = Vector3.Distance(target2.position, transform.position);
        barricadeScript tBS2 = BoardSpawnerTracker.instance.boardSpawner2;

        Transform target3 = BoardSpawnerTracker.instance.boardSpawnerTarget3.transform;
        float distToTarget3 = Vector3.Distance(target3.position, transform.position);
        barricadeScript tBS3 = BoardSpawnerTracker.instance.boardSpawner3;

        Transform target4 = BoardSpawnerTracker.instance.boardSpawnerTarget4.transform;
        float distToTarget4 = Vector3.Distance(target4.position, transform.position);
        barricadeScript tBS4 = BoardSpawnerTracker.instance.boardSpawner4;

        Transform target5 = BoardSpawnerTracker.instance.boardSpawnerTarget5.transform;
        float distToTarget5 = Vector3.Distance(target5.position, transform.position);
        barricadeScript tBS5 = BoardSpawnerTracker.instance.boardSpawner5;

        if (distToTarget1 < distToTarget)
        {
            target = target1;
            distToTarget = distToTarget1;
            targetBoardSpawner = tBS1;
            insideTarget = BoardSpawnerTracker.instance.insideTarget1.transform;
        }
        if (distToTarget2 < distToTarget)
        {
            target = target2;
            distToTarget = distToTarget2;
            targetBoardSpawner = tBS2;
            insideTarget = BoardSpawnerTracker.instance.insideTarget2.transform;
        }
        if (distToTarget3 < distToTarget)
        {
            target = target3;
            distToTarget = distToTarget3;
            targetBoardSpawner = tBS3;
            insideTarget = BoardSpawnerTracker.instance.insideTarget3.transform;
        }
        if (distToTarget4 < distToTarget)
        {
            target = target4;
            distToTarget = distToTarget4;
            targetBoardSpawner = tBS4;
            insideTarget = BoardSpawnerTracker.instance.insideTarget4.transform;
        }
        if (distToTarget5 < distToTarget)
        {
            target = target5;
            targetBoardSpawner = tBS5;
            insideTarget = BoardSpawnerTracker.instance.insideTarget5.transform;
        }
        return target;
    }

    void FaceTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
