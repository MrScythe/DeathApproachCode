using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_Behaviour : MonoBehaviour
{
    [SerializeField] private int m_MaxHealth;
    [SerializeField] private Slider m_Healthbar;
    [SerializeField] private Color m_LowHealth, m_NormalHealth;
    [SerializeField] private Image m_HealthColor;

    [SerializeField] private float m_Knockback;
    [SerializeField] private float m_Speed;

    [SerializeField] private GameObject m_SlashEffect, m_AttackEffect, m_Hit;
    [SerializeField] private Transform m_SlashPivot;

    [SerializeField] private TextMeshProUGUI m_HealthValue;

    [SerializeField] private int m_ReduceDelay;
    [SerializeField] private AudioSource m_Swing, m_HitSound;
    [SerializeField] private float m_AttackSpeed;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animation;
    private BoxCollider2D m_Collider;
    private Death_Approaching m_Death;

    private int m_CurrentHealth;
    private Transform m_Player;
    private bool m_IsDead;

    private float m_Distance;
    private float m_Delay, m_MaxDelay = 0.5f;
    private float m_AttackDelay;
    private bool m_InRange;
    private bool m_HasWon;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
        m_Collider = GetComponent<BoxCollider2D>();

        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Death = FindObjectOfType<Death_Approaching>();

        m_CurrentHealth = m_MaxHealth;
        m_Healthbar.maxValue = m_MaxHealth;
        m_Healthbar.value = m_MaxHealth;
        m_HealthColor.color = m_NormalHealth;
        m_HealthValue.SetText(m_CurrentHealth + "/" + m_MaxHealth);

        m_Delay = m_MaxDelay;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PAttack"))
        {
            m_CurrentHealth--;
            m_HealthValue.SetText(m_CurrentHealth + "/" + m_MaxHealth);

            m_Delay = 0;
            m_AttackDelay = 0;

            m_Healthbar.value = m_CurrentHealth;

            if (m_CurrentHealth <= m_MaxHealth / 3)
            {
                m_HealthColor.color = m_LowHealth;
            }

            if (m_CurrentHealth <= 0)
            {
                int kills = PlayerPrefs.GetInt("Kill");
                kills += 1;
                PlayerPrefs.SetInt("Kill", kills);

                int delay = PlayerPrefs.GetInt("Delay");
                delay += m_ReduceDelay;
                PlayerPrefs.SetInt("Delay", delay);

                m_Animation.SetTrigger("Dead");
                m_Collider.enabled = false;
                m_Healthbar.gameObject.SetActive(false);

                Destroy(gameObject, 1);
                m_IsDead = true;
                m_Death.Kill(m_ReduceDelay);
            }
            else
            {
                m_Rigidbody.AddForce(-collision.transform.right * m_Knockback);
                m_Animation.SetTrigger("Hurt");
                m_Animation.SetBool("Walking", false);
            }

            GameObject hit = Instantiate(m_Hit, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(hit, 0.5f);

            float pitch = Random.Range(0.5f, 1.5f);
            m_HitSound.pitch = pitch;
            m_HitSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsDead && m_Player != null && !m_Player.GetComponent<Player_Stats>().m_Dead)
        {
            if (m_Player.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            m_Distance = Vector2.Distance(transform.position, m_Player.position);

            if (m_Delay < m_MaxDelay)
            {
                m_Delay += 1 * Time.deltaTime;
            }
            else if (m_Distance > 4)
            {
                if (m_InRange)
                {
                    m_Delay = 0;
                    m_InRange = false;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, m_Player.position, m_Speed * Time.deltaTime);
                    m_Animation.SetBool("Walking", true);

                    m_AttackDelay = 0;
                }
            }
            else
            {
                m_Animation.SetBool("Walking", false);

                if (m_AttackDelay < m_AttackSpeed)
                {
                    m_AttackDelay += 1 * Time.deltaTime;
                }
                else
                {
                    m_Animation.SetTrigger("Attack0");
                    GameObject slash = Instantiate(m_SlashEffect, m_SlashPivot.position, m_SlashPivot.rotation);
                    Destroy(slash, 0.5f);

                    GameObject attack = Instantiate(m_AttackEffect, m_SlashPivot.position, m_SlashPivot.rotation);
                    Destroy(attack, 0.1f);

                    m_AttackDelay = 0;

                    float pitch = Random.Range(0.5f, 1.5f);
                    m_Swing.pitch = pitch;
                    m_Swing.Play();
                }

                m_InRange = true;
            }
        }
        else if (m_Player == null && !m_HasWon)
        {
            m_Healthbar.gameObject.SetActive(false);
            m_Animation.SetTrigger("Win");
            m_HasWon = true;
        }
    }
}
