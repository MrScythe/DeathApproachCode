using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Enemies;
    [SerializeField] private int m_EnemyCount;

    [SerializeField] private Transform m_MinX, m_MaxX;
    [SerializeField] private Transform m_MinY, m_MaxY;

    private Vector2 m_SpawnPos;
    private int m_CurrentFloor;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentFloor = PlayerPrefs.GetInt("Floor");
        m_EnemyCount = Random.Range(1, 3);

        for (int i = 0; i < m_EnemyCount; i++)
        {
            int type = Random.Range(0, m_Enemies.Length);
            if (m_CurrentFloor < m_Enemies.Length)
            {
                type = Random.Range(0, m_CurrentFloor);
            }
            if (m_CurrentFloor >= 5)
            {
                type = Random.Range(1, m_Enemies.Length);
            }

            float x = Random.Range(m_MinX.position.x, m_MaxX.position.x);
            float y = Random.Range(m_MinY.position.y, m_MaxY.position.y);
            m_SpawnPos = new Vector2(x, y);

            GameObject enemy = Instantiate(m_Enemies[type], m_SpawnPos, Quaternion.identity);
            enemy.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
