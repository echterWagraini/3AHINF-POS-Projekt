using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject Target = null;
    public float flyspeed = 70f;
    public float damage = 0f;
    


    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null) 
        {
            Vector2 direction = Target.transform.position - transform.position;
            direction.y += 5;
            transform.Translate(direction.normalized * flyspeed * Time.deltaTime, Space.World);
            doDamage(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject t)
    {
        Target = t;
    }

    public void doDamage(Vector2 direction)
    {
        float distanceThisFrame = flyspeed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            if(Target.tag == "enemy")
            {
                Target.GetComponent<EnemyMov>().takeDamage(damage);
            }
            if (Target.tag == "ally")
            {
                Target.GetComponent<AllyMovement>().takeDamage(damage);
            }
            if (Target.tag == "footenemy")
            {
                Target.GetComponent<FootEnemyMovement>().takeDamage(damage);
            }
            if (Target.tag == "footally")
            {
                Target.GetComponent<FootAllyMovement>().takeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}