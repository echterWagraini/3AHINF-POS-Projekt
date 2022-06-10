using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public static gameManager manager;

    void Awake()
    {
        if (manager != null)
        {
            Debug.LogError("Fehler!");
            return;
        }
        manager = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("General")]
    public GameObject notshowingwhenpaused;
    public float startmoney = 100f;
    public float money;
    public float hitpoints;
    public bool paused = false;

    [Header("Infos")]
    public GameObject moneytext;
    public GameObject hptext;
    public GameObject endInfo;
    public GameObject endInfoHeader;
    public GameObject pauseInfo;

    [Header("Buttons")]
    public GameObject resumeButton;
    public GameObject restartbutton;
    public GameObject pauseInfoHeader;
    public GameObject quitButton;
    public GameObject backToMenuButton;

    [Header("Images")]
    public Image endImage;
    public Image pauseImage;

    void Start()
    {
        money = startmoney;
        restartbutton.SetActive(false);

        endImage.enabled = false;
        pauseImage.enabled = false;
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moneytext.GetComponent<UnityEngine.UI.Text>().text = "Money: "+money.ToString();
        hptext.GetComponent<UnityEngine.UI.Text>().text = "hp: " + hitpoints.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    void PauseGame()
    {
        notshowingwhenpaused.SetActive(false);
        resumeButton.SetActive(true);
        pauseImage.enabled = true;

        Time.timeScale = 0;
        
        paused = true;
        pauseInfoHeader.GetComponent<UnityEngine.UI.Text>().text = "Pausiert!";
        pauseInfo.GetComponent<UnityEngine.UI.Text>().text = "Runde: " + baseSpawner.instance.wavecount;
        
    }
    public void ResumeGame()
    {
        notshowingwhenpaused.SetActive(true);
        pauseImage.enabled = false;
        resumeButton.SetActive(false);

        Time.timeScale = 1;

        paused = false;
        pauseInfo.GetComponent<UnityEngine.UI.Text>().text = "";
        pauseInfoHeader.GetComponent<UnityEngine.UI.Text>().text = "";
        
    }
    public void WaitForReset(string text)
    {
        notshowingwhenpaused.SetActive(false);
        quitButton.SetActive(true);
        backToMenuButton.SetActive(true);
        endImage.enabled = true;
        restartbutton.SetActive(true);

        Time.timeScale = 0;

        endInfoHeader.GetComponent<UnityEngine.UI.Text>().text = text;
        endInfo.GetComponent<UnityEngine.UI.Text>().text = "\nRunden:" + baseSpawner.instance.totalwaves + "\nRestgeld: " + money;
        
    }
    public void ResetGame()
    {
        restartbutton.SetActive(false);
        quitButton.SetActive(false);
        endImage.enabled = false;

        money = startmoney;

        Time.timeScale = 1;

        endInfo.GetComponent<UnityEngine.UI.Text>().text = "";
        endInfoHeader.GetComponent<UnityEngine.UI.Text>().text = "";
        notshowingwhenpaused.SetActive(true);

        GameObject[] allies = GameObject.FindGameObjectsWithTag("ally");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] everything = allies.Concat(enemies).ToArray();

        foreach(GameObject thing in everything)
        {
            Destroy(thing);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
