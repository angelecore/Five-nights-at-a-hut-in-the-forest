using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public Text enemyhp;
    public HealthBar healthBar;
    // Update is called once per frame
    void Update()
    {
        Collider[] spiders = Physics.OverlapSphere(Attackpoint.position, AttackRange, spiderlayer);
        string line = "";
        foreach (Collider c in spiders)
        {
            //Component spider = c.GetComponent<SpiderAI>();
            line = line + $"{c.GetComponent<SpiderAI>().name} health: {c.GetComponent<SpiderAI>().GetHealth()} ";
            healthBar.SetHealth(c.GetComponent<SpiderAI>().GetHealth());
        }
        
        enemyhp.text = line;

        Collider[] Ents = Physics.OverlapSphere(Attackpoint.position, AttackRange, EntLayer);
        foreach (Collider c in Ents)
        {
            //Component spider = c.GetComponent<SpiderAI>();
            line = line + $"{c.GetComponent<TreeEntAI>().name} health: {c.GetComponent<TreeEntAI>().GetHealth()} ";
            healthBar.SetHealth(c.GetComponent<TreeEntAI>().GetHealth());
        }
        if (Ents.Length <= 0 && spiders.Length <= 0)
            healthBar.SetHealth(0);
        enemyhp.text = line;

        if (Input.GetKeyDown(KeyCode.Space))
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
                //c.GetComponent<SpiderAI>().setcounterto0();
        }
        Collider[] Ents = Physics.OverlapSphere(Attackpoint.position, AttackRange, EntLayer);
        foreach (Collider c in Ents)
        {
            if (!c.GetComponent<TreeEntAI>().GetStunValue())
            { 
                c.GetComponent<TreeEntAI>().TakeDamage(1); 
            }
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
