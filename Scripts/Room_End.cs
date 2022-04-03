using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room_End : MonoBehaviour
{
    [SerializeField] private GameObject m_Button;
    private bool m_InRange;
    private int m_CurrentFloor;

    private Death_Approaching m_Death;

    // Start is called before the first frame update
    void Start()
    {
        m_Death = FindObjectOfType<Death_Approaching>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player_Stats>().m_Dead)
        {
            m_InRange = true;
        }
        else
        {
            m_InRange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_InRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_InRange)
        {
            m_Death.Kill(5);
            int delay = PlayerPrefs.GetInt("Delay");
            delay += 5;
            PlayerPrefs.SetInt("Delay", delay);

            m_CurrentFloor = PlayerPrefs.GetInt("Floor");
            m_CurrentFloor++;
            PlayerPrefs.SetInt("Floor", m_CurrentFloor);

            SceneManager.LoadScene("SampleScene");
        }

        if (m_InRange)
        {
            m_Button.SetActive(true);
        }
        else
        {
            m_Button.SetActive(false);
        }
    }
}
