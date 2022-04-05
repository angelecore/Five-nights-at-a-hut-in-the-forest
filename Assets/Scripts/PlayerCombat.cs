using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public float AttackRange = 1.5f;

    public Transform Attackpoint;

    public LayerMask spiderlayer;
    public LayerMask EntLayer;
    public float damagevalue = 1;
    private float AttackCooldownTime = 1;
    private float NextAttackTime = 0;

    public float knockbackTime;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > NextAttackTime)
            {
                Attack();
                NextAttackTime = Time.time + AttackCooldownTime;
            }
        }
    }

    void Attack()
    {
        // enemy detection
        Collider[] spiders = Physics.OverlapSphere(Attackpoint.position, AttackRange, spiderlayer);
        foreach (Collider c in spiders)
        {
            /*DeadCode
            //Rigidbody enemy = c.GetComponent<Rigidbody>();
            //enemy.isKinematic = false;
            //Vector3 difference = enemy.transform.forward + Attackpoint.forward;
            //difference = difference.normalized * 4;
            //enemy.AddForce(difference, ForceMode.Impulse);
            //c.GetComponent<SpiderAI>().GoAway();
            //StartCoroutine(KnockBackCo(enemy));*/
                c.GetComponent<SpiderAI>().TakeDamage(damagevalue);
                c.GetComponent<SpiderAI>().setcounterto0();
        }
        Collider[] Ents = Physics.OverlapSphere(Attackpoint.position, AttackRange, EntLayer);
        foreach (Collider c in Ents)
        {
            c.GetComponent<TreeEntAI>().TakeDamage(1);
        }
    }

    /*public void knock_back()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Vector3 difference = rb.transform.forward - target.transform.forward;
        difference = difference.normalized * 4;
        rb.AddForce(difference, ForceMode.Impulse);

    }*/

    private void OnDrawGizmos()
    {
        if (Attackpoint == null)
            return;
        Gizmos.DrawWireSphere(Attackpoint.position, AttackRange);
    }

    private IEnumerator KnockBackCo(Rigidbody rb)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
