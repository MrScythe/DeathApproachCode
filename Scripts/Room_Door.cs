using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Door : MonoBehaviour
{
    [SerializeField] private GameObject m_Button;
    public GameObject m_Stuff;
    public Transform m_TargetDoor;
    [HideInInspector] public GameObject m_StuffOther;
    private bool m_InRange;

    private Transform m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<Player_Movement>().transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
            m_Player.position = m_TargetDoor.position;
            m_Stuff.SetActive(false);
            m_StuffOther.SetActive(true);
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
