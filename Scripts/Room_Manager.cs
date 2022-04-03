using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Rooms;
    [SerializeField] private GameObject m_EndRoom;

    [SerializeField] private int m_Direction;
    [SerializeField] private GameObject m_Stuff;

    public Transform m_EnteringDoor;
    public Room_Door m_Door;

    private Transform m_EnteringDoor2;
    private GameObject m_Stuff2;

    private Room_Limit m_Limit;

    private float m_Delay, m_MaxDelay = 0.2f;
    private bool m_HasCreated, m_CanCreate = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Limit = FindObjectOfType<Room_Limit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Core"))
        {
            m_CanCreate = false;
        }
    }

    private void CreateRoom()
    {
        if (m_Rooms.Length > 0)
        {
            int room = Random.Range(0, m_Rooms.Length);
            GameObject door = Instantiate(m_Rooms[room], transform.position, Quaternion.identity);

            switch (m_Direction)
            {
                case 0: //Top
                    door.GetComponent<Room_StartingDoor>().m_Top.m_Door.m_TargetDoor = m_EnteringDoor;
                    door.GetComponent<Room_StartingDoor>().m_Top.m_Door.m_StuffOther = m_Stuff;
                    m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Top.m_EnteringDoor;
                    m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Top.m_Stuff;
                    break;
                case 1: //Bottom
                    door.GetComponent<Room_StartingDoor>().m_Bottom.m_Door.m_TargetDoor = m_EnteringDoor;
                    door.GetComponent<Room_StartingDoor>().m_Bottom.m_Door.m_StuffOther = m_Stuff;
                    m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Bottom.m_EnteringDoor;
                    m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Bottom.m_Stuff;
                    break;
                case 2: //Left
                    door.GetComponent<Room_StartingDoor>().m_Left.m_Door.m_TargetDoor = m_EnteringDoor;
                    door.GetComponent<Room_StartingDoor>().m_Left.m_Door.m_StuffOther = m_Stuff;
                    m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Left.m_EnteringDoor;
                    m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Left.m_Stuff;
                    break;
                case 3: //Right
                    door.GetComponent<Room_StartingDoor>().m_Right.m_Door.m_TargetDoor = m_EnteringDoor;
                    door.GetComponent<Room_StartingDoor>().m_Right.m_Door.m_StuffOther = m_Stuff;
                    m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Right.m_EnteringDoor;
                    m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Right.m_Stuff;
                    break;
            }

            m_Door.m_TargetDoor = m_EnteringDoor2;
            m_Door.m_StuffOther = m_Stuff2;
        }
    }

    private void CreateDeadEnd()
    {
        GameObject door = Instantiate(m_EndRoom, transform.position, Quaternion.identity);

        switch (m_Direction)
        {
            case 0: //Top
                door.GetComponent<Room_StartingDoor>().m_Top.m_Door.m_TargetDoor = m_EnteringDoor;
                door.GetComponent<Room_StartingDoor>().m_Top.m_Door.m_StuffOther = m_Stuff;
                m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Top.m_EnteringDoor;
                m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Top.m_Stuff;
                break;
            case 1: //Bottom
                door.GetComponent<Room_StartingDoor>().m_Bottom.m_Door.m_TargetDoor = m_EnteringDoor;
                door.GetComponent<Room_StartingDoor>().m_Bottom.m_Door.m_StuffOther = m_Stuff;
                m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Bottom.m_EnteringDoor;
                m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Bottom.m_Stuff;
                break;
            case 2: //Left
                door.GetComponent<Room_StartingDoor>().m_Left.m_Door.m_TargetDoor = m_EnteringDoor;
                door.GetComponent<Room_StartingDoor>().m_Left.m_Door.m_StuffOther = m_Stuff;
                m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Left.m_EnteringDoor;
                m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Left.m_Stuff;
                break;
            case 3: //Right
                door.GetComponent<Room_StartingDoor>().m_Right.m_Door.m_TargetDoor = m_EnteringDoor;
                door.GetComponent<Room_StartingDoor>().m_Right.m_Door.m_StuffOther = m_Stuff;
                m_EnteringDoor2 = door.GetComponent<Room_StartingDoor>().m_Right.m_EnteringDoor;
                m_Stuff2 = door.GetComponent<Room_StartingDoor>().m_Right.m_Stuff;
                break;
        }

        m_Door.m_TargetDoor = m_EnteringDoor2;
        m_Door.m_StuffOther = m_Stuff2;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += 1 * Time.deltaTime;
        }
        else if (!m_HasCreated && m_CanCreate)
        {
            if (m_Limit.m_RoomLimit > 0)
            {
                m_Limit.m_RoomLimit--;
                CreateRoom();
            }
            else
            {
                CreateDeadEnd();
            }

            m_HasCreated = true;
        }
    }
}
