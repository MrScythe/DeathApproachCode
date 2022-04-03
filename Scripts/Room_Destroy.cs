using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Destroy : MonoBehaviour
{
    private bool m_HasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Core") && !m_HasSpawned)
        {
            Destroy(gameObject);
        }
        else
        {
            m_HasSpawned = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
