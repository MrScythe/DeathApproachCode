using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Mouse : MonoBehaviour
{
    [SerializeField] private Transform m_Body, m_SlashPivot;
    [SerializeField] private Player_Stats m_Stats;

    private float m_Angle;

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookAt = mouseScreenPosition;
        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);

        m_Angle = (180 / Mathf.PI) * AngleRad;

        transform.rotation = Quaternion.Euler(0, 0, m_Angle);

        if (!m_Stats.m_Attacked && !m_Stats.m_Dead)
        {
            if (mouseScreenPosition.x > m_Body.transform.position.x)
            {
                m_Body.rotation = Quaternion.Euler(0, 180, 0);
                m_SlashPivot.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                m_Body.rotation = Quaternion.Euler(0, 0, 0);
                m_SlashPivot.localRotation = Quaternion.Euler(180, 180, 0);
            }
        }
    }
}
