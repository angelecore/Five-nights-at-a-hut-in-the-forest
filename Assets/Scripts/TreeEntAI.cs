using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeEntAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    public PhaseSwitchTrigger phaseSwitch;

    private bool ImActive = false;
    private bool ActivationInitiated = false;

    // Start is called before the first frame update
    void Start()
    {
        //target - the child object for AI to move to
        //targetBoardSpawner - parent object, should contain whatever is needed
        GameObject targetBoardSpawner;
        target = GetTarget(out targetBoardSpawner);
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseSwitch.IsItNight)
        {
            if (ImActive)
            {
                float distanceToTarget = Vector3.Distance(target.position, transform.position);

                agent.SetDestination(target.position);

                if (distanceToTarget <= agent.stoppingDistance)
                {
                    //attack the barricade
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

    IEnumerator DelayedActivate()
    {
        //random spawn delay between 3 - 15 seconds
        int delay = Mathf.RoundToInt(Random.Range(1f, 15f));
        yield return new WaitForSeconds(delay);

        ImActive = true;
    }

    private Transform GetTarget(out GameObject targetBoardSpawner)
    {
        Transform target = BoardSpawnerTracker.instance.boardSpawnerTarget.transform;
        float distToTarget = Vector3.Distance(target.position, transform.position);
        targetBoardSpawner = BoardSpawnerTracker.instance.boardSpawner;

        Transform target1 = BoardSpawnerTracker.instance.boardSpawnerTarget1.transform;
        float distToTarget1 = Vector3.Distance(target1.position, transform.position);
        GameObject tBS1 = BoardSpawnerTracker.instance.boardSpawner1;

        Transform target2 = BoardSpawnerTracker.instance.boardSpawnerTarget2.transform;
        float distToTarget2 = Vector3.Distance(target2.position, transform.position);
        GameObject tBS2 = BoardSpawnerTracker.instance.boardSpawner2;

        Transform target3 = BoardSpawnerTracker.instance.boardSpawnerTarget3.transform;
        float distToTarget3 = Vector3.Distance(target3.position, transform.position);
        GameObject tBS3 = BoardSpawnerTracker.instance.boardSpawner3;

        Transform target4 = BoardSpawnerTracker.instance.boardSpawnerTarget4.transform;
        float distToTarget4 = Vector3.Distance(target4.position, transform.position);
        GameObject tBS4 = BoardSpawnerTracker.instance.boardSpawner4;

        Transform target5 = BoardSpawnerTracker.instance.boardSpawnerTarget5.transform;
        float distToTarget5 = Vector3.Distance(target5.position, transform.position);
        GameObject tBS5 = BoardSpawnerTracker.instance.boardSpawner5;

        if (distToTarget1 < distToTarget)
        {
            target = target1;
            distToTarget = distToTarget1;
            targetBoardSpawner = tBS1;
        }
        if (distToTarget2 < distToTarget)
        {
            target = target2;
            distToTarget = distToTarget2;
            targetBoardSpawner = tBS2;
        }
        if (distToTarget3 < distToTarget)
        {
            target = target3;
            distToTarget = distToTarget3;
            targetBoardSpawner = tBS3;
        }
        if (distToTarget4 < distToTarget)
        {
            target = target4;
            distToTarget = distToTarget4;
            targetBoardSpawner = tBS4;
        }
        if (distToTarget5 < distToTarget)
        {
            target = target5;
            targetBoardSpawner = tBS5;
        }
        return target;
    }
}
