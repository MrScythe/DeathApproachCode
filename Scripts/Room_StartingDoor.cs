using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_StartingDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Rooms;

    public Room_Manager m_Left, m_Right, m_Bottom, m_Top;

    private float m_Delay, m_MaxDelay = 0.2f;
    private int m_CurrentRoom;
    private bool m_Finished;

    // Start is called before the first frame update
    void Start()
    {
        m_Delay = m_MaxDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Finished)
        {
            if (m_Delay < m_MaxDelay)
            {
                m_Delay += 1 * Time.deltaTime;
            }
            else
            {
                m_Delay = 0;
                m_Rooms[m_CurrentRoom].SetActive(true);

                if (m_CurrentRoom < m_Rooms.Length - 1)
                {
                    m_CurrentRoom++;
                }
                else
                {
                    m_Finished = true;
                }
            }
        }
    }
}
