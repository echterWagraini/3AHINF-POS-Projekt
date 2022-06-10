using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void returnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void doExit()
    {
        Application.Quit();
    }
}
