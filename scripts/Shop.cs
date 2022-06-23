using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("General")]
    public GameObject buyStatus;
    private bool showing = false;
    private static float showtime = 1f;
    private float shown = showtime;
    public Transform allyspawnpoint;
    public float timebetweenally = 0f;
    public Image cooldownbar;

    [Header("Ally")]
    public Transform standardAllyPrefab;
    public float standardAllyCost = 0f;

    [Header("FootAlly")]
    public Transform footAllyPrefab;
    public float footAllyCost = 0f;

    [Header("Kanone")]
    public GameObject Kanone;
    public float kanoneCost = 0f;

    public void Awake()
    {
        cooldownbar.fillAmount = 1;

        Kanone.SetActive(false);

        GameObject.Find("buyKanone").GetComponentInChildren<Text>().text = kanoneCost.ToString();
        GameObject.Find("spawnDefaultAlly").GetComponentInChildren<Text>().text = standardAllyCost.ToString();
        GameObject.Find("spawnFootAlly").GetComponentInChildren<Text>().text = footAllyCost.ToString();

    }

    public void Update()
    {
        timebetweenally -= Time.deltaTime;

        if (timebetweenally <= 0)
            cooldownbar.fillAmount = 1;
        else
            cooldownbar.fillAmount = timebetweenally / 1;

        if (showing == true)
        {
            if (shown < 0f)
            {
                buyStatus.GetComponent<UnityEngine.UI.Text>().text = "";
                showing = false;
                shown = showtime;
            }
            shown -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            buyStandardAlly();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            buyFootAlly();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buyKanone();
        }
    }

    public void buyStandardAlly()
    {
        if (buyable(standardAllyCost,standardAllyPrefab) == true)
        {
            Debug.Log("Standard ally gekauft");
            spawnAlly();
            timebetweenally = 1f;
            cooldownbar.fillAmount = timebetweenally;
            gameManager.manager.money -= standardAllyCost;
        }
    }

    public void buyFootAlly()
    {
        if (buyable(footAllyCost,footAllyPrefab) == true)
        {
            Debug.Log("Foot ally gekauft");
            spawnAlly();
            timebetweenally = 1f;
            cooldownbar.fillAmount = timebetweenally;
            gameManager.manager.money -= footAllyCost;
        }
    }
    
    public void buyKanone()
    {
        if(gameManager.manager.money >= kanoneCost)
        {
            Kanone.SetActive(true);
            gameManager.manager.money -= kanoneCost;
            Debug.Log("Kanone gekauft");
            GameObject.Find("buyKanone").SetActive(false);
        }
        else
        {
            buyStatus.GetComponent<UnityEngine.UI.Text>().text = "Not Enough Money!";
        }
    }

    public bool buyable(float price, Transform prefab)
    {
        if (gameManager.manager.money >= price)
        {
            BuyManager.manager.SetAllyToSpawn(prefab);
            if (timebetweenally <= 0f)
            {
                return true;
            }
            else
            {
                Debug.Log("Kaufen noch auf cd");
                buyStatus.GetComponent<UnityEngine.UI.Text>().text = "Wait For Cooldown!";
                showing = true;
                return false;
            }
        }
        else
        {
            Debug.Log("kein Geld");
            buyStatus.GetComponent<UnityEngine.UI.Text>().text = "Not Enough Money!";
            showing = true;
            return false;
        }
    }

    public void spawnAlly()
    {
        Vector3 footallyspawnpoint = allyspawnpoint.position;
        footallyspawnpoint.y += 2;

        if(BuyManager.manager.GetAllyToSpawn() == footAllyPrefab)
        {
            Instantiate(BuyManager.manager.GetAllyToSpawn(), footallyspawnpoint, allyspawnpoint.rotation);
        }
        else
        {
            Instantiate(BuyManager.manager.GetAllyToSpawn(), allyspawnpoint.position, allyspawnpoint.rotation);
        }
        
    }
}
