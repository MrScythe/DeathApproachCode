using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Slides;
    [SerializeField] private Player_Attack m_Attack;

    [SerializeField] private bool m_Menu;

    private int m_CurrentSlide;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_Menu)
        {
            if (PlayerPrefs.GetInt("Tutorial") <= 0)
            {
                Time.timeScale = 0;
                m_Attack.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            m_Slides[m_CurrentSlide].SetActive(false);

            if (m_CurrentSlide < m_Slides.Length - 1)
            {
                m_CurrentSlide++;
                m_Slides[m_CurrentSlide].SetActive(true);
            }
            else
            {
                if (!m_Menu)
                {
                    Time.timeScale = 1;

                    m_Attack.enabled = true;
                    PlayerPrefs.SetInt("Tutorial", 1);
                }
                else
                {
                    m_CurrentSlide = 0;
                    m_Slides[m_CurrentSlide].SetActive(true);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
