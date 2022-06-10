using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyTurretScript : MonoBehaviour
{
    [Header("Werte")]
    public float Range;
    public float cooldown;
    public float basecooldown = 5f;
    
    [Header("Komponenten")]
    GameObject Target;
    public GameObject Gun;
    public GameObject bullet;
    public Transform FirePoint;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        setTarget();
        setRotation();

        if (cooldown <= 0)
        {
            shoot();
            cooldown = basecooldown;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

    }

    void shoot()
    {
        if (Target != null)
        {
            GameObject bullet1 = Instantiate(bullet, FirePoint.position, Quaternion.identity);
            bullet1.GetComponent<BulletScript>().setTarget(Target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    public void setTarget()
    {
        float shortestdist = Mathf.Infinity;
        GameObject shortest = null;

        GameObject[] allies = GameObject.FindGameObjectsWithTag("ally");
        GameObject[] footallies = GameObject.FindGameObjectsWithTag("footally");
        GameObject[] allallies = allies.Concat(footallies).ToArray();


        foreach (GameObject thingahead in allallies)
        {
            if (thingahead != gameObject)
            {
                float dist = Vector2.Distance(transform.position, thingahead.transform.position);

                if (dist < shortestdist)
                {
                    shortestdist = dist;
                    shortest = thingahead;
                }

            }
        }
        if (shortestdist <= Range)
        {
            //Debug.Log("Target Set");
            Target = shortest;
        }
    }

    void setRotation()
    {    
        if (Target != null)
        {
            Vector3 pos = Target.transform.position;
            pos.y += 5;
            Gun.transform.right = - pos + Gun.transform.position;
        }
    }

}