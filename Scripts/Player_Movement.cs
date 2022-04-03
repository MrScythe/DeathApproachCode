using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float m_WalkingSpeed, m_RunningSpeed, m_MaxStamina;
    [SerializeField] private Slider m_StaminaBar;
    [SerializeField] private Image m_StaminaColor;
    [SerializeField] private Color m_NomralColor, m_ExhaustedColor;
    private float m_Speed;

    private Animator m_Animation;
    private Player_Stats m_Stats;

    private bool m_IsRunning, m_IsAttacking;
    private float m_Delay, m_MaxDelay = 0.4f;
    private float m_Stamina;
    private float m_StaminaDelay, m_MaxStaminaDelay = 1;

    private bool m_Exhausted;

    // Use this for initialization
    void Start()
    {
        m_Animation = GetComponent<Animator>();
        m_Stats = GetComponent<Player_Stats>();

        m_Delay = m_MaxDelay;
        m_Stamina = m_MaxStamina;
        m_StaminaDelay = m_MaxStaminaDelay;

        m_StaminaBar.maxValue = m_MaxStamina;
        m_StaminaBar.value = m_Stamina;
    }

    public void Attack()
    {
        m_Delay = 0;
    }

    private void Update()
    {
        if (!m_Stats.m_Attacked && !m_Stats.m_Dead)
        {
            if (m_Delay < m_MaxDelay)
            {
                m_Delay += 1 * Time.deltaTime;
                m_IsAttacking = true;
            }
            else
            {
                m_IsAttacking = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && m_Stamina > 0 && !m_Exhausted && !Input.GetMouseButton(0))
            {
                m_IsRunning = true;
                m_Speed = m_RunningSpeed;
            }
            else
            {
                m_IsRunning = false;
                m_Speed = m_WalkingSpeed;
            }

            if (m_Stamina < m_MaxStamina && !m_IsRunning)
            {
                if (m_Stamina <= 0 && !m_Exhausted)
                {
                    m_Exhausted = true;
                    m_StaminaColor.color = m_ExhaustedColor;
                }

                if (m_StaminaDelay < m_MaxStaminaDelay)
                {
                    m_StaminaDelay += 1 * Time.deltaTime;
                }
                else
                {
                    m_Stamina += 1 * Time.deltaTime;
                    m_StaminaBar.value = m_Stamina;
                }
            }

            if (m_Stamina >= m_MaxStamina && m_Exhausted)
            {
                m_Exhausted = false;
                m_StaminaColor.color = m_NomralColor;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_IsAttacking && !m_Stats.m_Attacked && !m_Stats.m_Dead)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    transform.Translate(Vector3.right * m_Speed * Time.deltaTime, Space.World);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    transform.Translate(Vector3.left * m_Speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    transform.Translate(Vector3.up * m_Speed * Time.deltaTime, Space.World);
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    transform.Translate(Vector3.down * m_Speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (m_IsRunning)
                {
                    m_Animation.SetBool("Running", true);
                    m_Stamina -= 1 * Time.deltaTime;
                    m_StaminaBar.value = m_Stamina;
                    m_StaminaDelay = 0;
                }
                else
                {
                    m_Animation.SetBool("Running", false);
                }

                m_Animation.SetBool("Walking", true);
            }
            else
            {
                m_Animation.SetBool("Running", false);
                m_Animation.SetBool("Walking", false);
            }
        }
    }
}
