using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death_Approaching : MonoBehaviour
{
    [SerializeField] private Slider m_DeathBar;
    [SerializeField] private float m_DeathTime;
    [SerializeField] private Animator m_Animation;
    [SerializeField] private Player_Stats m_Player;
    [SerializeField] private TimeUp m_Time;

    private float m_CurrentTime;
    private Player_Stats m_Stats;
    private bool m_IsDead;

    // Start is called before the first frame update
    void Start()
    {
        m_Stats = FindObjectOfType<Player_Stats>();
        m_DeathBar.maxValue = m_DeathTime;

        m_CurrentTime = PlayerPrefs.GetFloat("Death");
    }

    public void Kill(int delay)
    {
        m_CurrentTime -= delay;
        PlayerPrefs.SetFloat("Death", m_CurrentTime);

        if (m_CurrentTime < 0)
        {
            m_CurrentTime = 0;
        }

        m_DeathBar.value = m_CurrentTime;

        m_Time.AddTime(delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Player.m_Dead)
        {
            if (m_CurrentTime < m_DeathTime)
            {
                m_CurrentTime += 1 * Time.deltaTime;
                m_DeathBar.value = m_CurrentTime;

                PlayerPrefs.SetFloat("Death", m_CurrentTime);
            }
            else if (!m_IsDead)
            {
                m_Stats.Dead();
                m_IsDead = true;
            }

            if (m_CurrentTime > m_DeathTime - (m_DeathTime / 3))
            {
                m_Animation.SetBool("Shake", true);
            }
            else
            {
                m_Animation.SetBool("Shake", false);
            }
        }
    }
}
