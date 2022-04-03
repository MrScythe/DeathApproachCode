using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manage : MonoBehaviour
{
    [SerializeField] private GameObject m_Tutorial;
    private int m_HasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        m_HasPlayed = PlayerPrefs.GetInt("Play");
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("Floor", 1);
        PlayerPrefs.SetFloat("Death", 0);
        PlayerPrefs.SetInt("Kill", 0);
        PlayerPrefs.SetInt("Delay", 0);
        PlayerPrefs.SetInt("HP", 0);

        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Floor", 1);
        PlayerPrefs.SetFloat("Death", 0);
        PlayerPrefs.SetInt("Kill", 0);
        PlayerPrefs.SetInt("Delay", 0);
        PlayerPrefs.SetInt("HP", 0);

        if (m_HasPlayed <= 0)
        {
            PlayerPrefs.SetInt("Play", 1);
            SceneManager.LoadScene("Comic");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Comic()
    {
        PlayerPrefs.SetInt("Floor", 1);
        PlayerPrefs.SetFloat("Death", 0);
        PlayerPrefs.SetInt("Kill", 0);
        PlayerPrefs.SetInt("Delay", 0);
        PlayerPrefs.SetInt("HP", 0);

        SceneManager.LoadScene("Comic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        m_Tutorial.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
