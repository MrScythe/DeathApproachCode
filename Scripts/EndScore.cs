using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Kills, m_Delay, m_Floor;
    private int m_Seconds, m_Minutes;

    private int m_EndFloor;

    // Start is called before the first frame update
    void Start()
    {
        m_Seconds = PlayerPrefs.GetInt("Delay");

        while (m_Seconds > 60)
        {
            m_Seconds -= 60;
            m_Minutes += 1;
        }

        if (m_Seconds < 10)
        {
            if (m_Minutes < 10)
            {
                m_Delay.SetText("0" + m_Minutes + ":0" + m_Seconds);
            }
            else
            {
                m_Delay.SetText(m_Minutes + ":0" + m_Seconds);
            }
        }
        else
        {
            if (m_Minutes < 10)
            {
                m_Delay.SetText("0" + m_Minutes + ":" + m_Seconds);
            }
            else
            {
                m_Delay.SetText(m_Minutes + ":" + m_Seconds);
            }
        }

        m_EndFloor = PlayerPrefs.GetInt("Floor");

        m_Kills.SetText(PlayerPrefs.GetInt("Kill").ToString());
        m_Floor.SetText(m_EndFloor.ToString());

        if (m_EndFloor > PlayerPrefs.GetInt("LowestFloor"))
        {
            PlayerPrefs.SetInt("LowestFloor", m_EndFloor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
