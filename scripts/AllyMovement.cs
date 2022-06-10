using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AllyMovement : MonoBehaviour
{
    public float basemovespeed = 5f;
    public float movespeed;
    public float basehealth = 500f;
    public float health;
    public float dps = 30f;

    public float damagecd = 0.416f;

    public Transform endpoint;

    public GameObject enemybase;

    private Animator anim;

    public Image healthbar;

    public GameObject healthbarcanvas;

    void Awake()
    {
        healthbarcanvas.SetActive(false);

        health = basehealth;
        movespeed = basemovespeed;
        enemybase = GameObject.FindGameObjectWithTag("enemybase");

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * movespeed;

        if (transform.position.x >= (enemybase.transform.position.x) - 8)
        {
            Debug.Log("AngriffBase");
            movespeed = 0f;
            damageBase(dps);
        }
        else
        {
            CheckAhead();
        }        
    }

    public bool takeDamage(float damage)
    {
        healthbarcanvas.SetActive(true);

        health -= damage;

        healthbar.fillAmount = health / basehealth;

        if (damage > health)
        {
            Die();
            return true;
        }
        return false;
    }

    public void dealDamage(GameObject toShoot, float damage)
    {
        //angepasst an animation: schlag bei 00:25 und fertig bei 00:43
        bool kill = false;

        if (damagecd <= 0)
        {
            if (toShoot.tag == "enemy")
            {
                kill = toShoot.GetComponent<EnemyMov>().takeDamage(damage);
            }
            else if (toShoot.tag == "footenemy")
            {
                kill = toShoot.GetComponent<FootEnemyMovement>().takeDamage(damage);
            }
            damagecd = 0.716f;
        }
        else
        {
            damagecd -= Time.deltaTime;
        }

        if (kill == true)
        {
            anim.Play("Pferd");
            movespeed = basemovespeed;
            gameManager.manager.money += 30;

            damagecd = 0.416f;
        }
    }

    public void damageBase(float damage)
    {
        if (damagecd <= 0)
        {
            enemybase.GetComponent<GameBase>().takeDamage(damage);

            damagecd = 0.716f;
        }
        else
        {
            damagecd -= Time.deltaTime;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void CheckAhead()
    {
        float shortestdist = Mathf.Infinity;
        GameObject nearest = null;

        GameObject[] allies = GameObject.FindGameObjectsWithTag("ally");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] ahead1 = allies.Concat(enemies).ToArray();

        GameObject[] footallies = GameObject.FindGameObjectsWithTag("footally");
        GameObject[] ahead2 = ahead1.Concat(footallies).ToArray();

        GameObject[] footenemies = GameObject.FindGameObjectsWithTag("footenemy");
        GameObject[] ahead = ahead2.Concat(footenemies).ToArray();

        foreach (GameObject thingahead in ahead)
        {
            if (thingahead != gameObject)
            {
                if (transform.position.x < thingahead.transform.position.x)
                {
                    float dist = Vector3.Distance(transform.position, thingahead.transform.position);
                    if (dist < shortestdist)
                    {
                        shortestdist = dist;
                        nearest = thingahead;
                    }
                }
            }
        }
        if (shortestdist <= 15f)
        {
            movespeed = 0f;
            if (nearest.tag == "enemy"||nearest.tag=="footenemy")
            {
                anim.Play("attack");
                dealDamage(nearest, dps);
            }
            else
            {
                anim.Play("wait");
            }
        }
        else
        {
            anim.Play("Pferd");
            movespeed = basemovespeed;
        }
    }
}