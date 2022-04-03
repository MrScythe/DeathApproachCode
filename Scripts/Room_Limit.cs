using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room_Limit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_FloorText;
    public int m_RoomLimit;

    private int m_CurrentFloor;

    private float m_Delay, m_MaxDelay = 4;
    private bool m_Started;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentFloor = PlayerPrefs.GetInt("Floor");

        if (m_CurrentFloor <= 0)
        {
            m_CurrentFloor = 1;
            PlayerPrefs.SetInt("Floor", m_CurrentFloor);
        }

        m_FloorText.SetText("Floor " + m_CurrentFloor);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += 1 * Time.deltaTime;
        }
        else if (!m_Started)
        {
            GameObject.FindGameObjectWithTag("Stairs").transform.GetChild(0).gameObject.SetActive(true);
            m_Started = true;
        }
    }
}
