using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    public float starthp = 20f;
    public float health;

    void Awake()
    {
        health=starthp;
    }
    void Update()
    {
        gameManager.manager.hitpoints = health;
    }

    public void takeDamage(float damage)
    {
        if (damage > health)
        {
            if (gameObject.tag == "allybase"){
                DieLoss();
            }
            else
            {
                DieWin();
            }
        }
        health -= damage;
        gameManager.manager.hitpoints = health;
    }

    public void DieWin()
    {
        reset();
        gameManager.manager.WaitForReset("Gewonnen!");
    }
    public void DieLoss()
    {
        reset();
        gameManager.manager.WaitForReset("Game Over!");
    }
    void reset()
    {
        health = starthp;
        gameManager.manager.hitpoints = health;
    }
}
