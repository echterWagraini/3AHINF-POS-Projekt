using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FootEnemyMovement : MonoBehaviour
{
    public float basemovespeed = 3f;
    public float movespeed;
    public float basehealth = 500f;
    public float health;
    public float dps = 50f;
    public float footEnemyKillReward = 50f;

    public float damagecd = 0.766f;

    public Transform endpoint;

    public GameObject allybase;

    private Animator anim;

    public Image healthbar;

    public GameObject healtbarcanvas;

    void Awake()
    {
        healtbarcanvas.SetActive(false);


        health = basehealth;
        movespeed = basemovespeed;
        allybase = GameObject.FindGameObjectWithTag("allybase");

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * movespeed;

        if (transform.position.x <= (allybase.transform.position.x) + 5)
        {
            anim.Play("AttackSword");
            movespeed = 0f;
            damageBase(dps);
        }

        CheckAhead();
    }

    public bool takeDamage(float damage)
    {
        healtbarcanvas.SetActive(true);

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
        //angepasst an animation: schlag bei 00:46 und schlag bei 01:24 und fertig bei 1:53
        bool kill = false;
        bool secondhit = false;

        if (damagecd <= 0)
        {
            if (toShoot.tag == "ally")
            {
                kill = toShoot.GetComponent<AllyMovement>().takeDamage(damage);
            }
            else if (toShoot.tag == "footally")
            {
                kill = toShoot.GetComponent<FootAllyMovement>().takeDamage(damage);
            }

            if (secondhit)
            {
                damagecd = 1.25f;
                secondhit = true;
            }
            else
            {
                damagecd = 0.633f;
                secondhit = false;
            }
            
        }
        else
        {
            damagecd -= Time.deltaTime;
        }

        if (kill == true)
        {
            movespeed = basemovespeed;
            anim.Play("Sword");

            damagecd = 0.766f;
        }
    }

    public void damageBase(float damage)
    {
        bool secondhit = false;

        if (damagecd <= 0)
        {
            allybase.GetComponent<GameBase>().takeDamage(damage);

            if (secondhit)
            {
                damagecd = 1.25f;
                secondhit = true;
            }
            else
            {
                damagecd = 0.633f;
                secondhit = false;
            }
        }
        else
        {
            damagecd -= Time.deltaTime;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        gameManager.manager.money += footEnemyKillReward;
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
                if (transform.position.x > thingahead.transform.position.x)
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
            movespeed = 0;
            if (nearest.tag == "ally" || nearest.tag == "footally")
            {
                anim.Play("AttackSword");
                dealDamage(nearest, dps);
            }
            else
            {
                anim.Play("waitSword");
            }
        }
        else
        {
            anim.Play("Sword");
            movespeed = basemovespeed;
        }
    }
}