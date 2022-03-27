using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeEntAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GetTarget();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        agent.SetDestination(target.position);

        if (distanceToTarget <= agent.stoppingDistance)
        {
            //attack the barricade
        }
    }

    private Transform GetTarget()
    {
        Transform target = BoardSpawnerTracker.instance.boardSpawnerTarget.transform;
        float distToTarget = Vector3.Distance(target.position, transform.position);
        Transform target1 = BoardSpawnerTracker.instance.boardSpawnerTarget1.transform;
        float distToTarget1 = Vector3.Distance(target1.position, transform.position);
        Transform target2 = BoardSpawnerTracker.instance.boardSpawnerTarget2.transform;
        float distToTarget2 = Vector3.Distance(target2.position, transform.position);
        Transform target3 = BoardSpawnerTracker.instance.boardSpawnerTarget3.transform;
        float distToTarget3 = Vector3.Distance(target3.position, transform.position);
        Transform target4 = BoardSpawnerTracker.instance.boardSpawnerTarget4.transform;
        float distToTarget4 = Vector3.Distance(target4.position, transform.position);
        Transform target5 = BoardSpawnerTracker.instance.boardSpawnerTarget5.transform;
        float distToTarget5 = Vector3.Distance(target5.position, transform.position);

        if (distToTarget1 < distToTarget)
        {
            target = target1;
            distToTarget = distToTarget1;
        }
        if (distToTarget2 < distToTarget)
        {
            target = target2;
            distToTarget = distToTarget2;
        }
        if (distToTarget3 < distToTarget)
        {
            target = target3;
            distToTarget = distToTarget3;
        }
        if (distToTarget4 < distToTarget)
        {
            target = target4;
            distToTarget = distToTarget4;
        }
        if (distToTarget5 < distToTarget)
        {
            target = target5;
        }
        return target;
    }
}
