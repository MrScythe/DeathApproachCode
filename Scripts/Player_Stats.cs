using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private int m_MaxHealth;
    [SerializeField] private GameObject m_Hit;
    [SerializeField] private Slider m_Healthbar;
    [SerializeField] private Image m_HealthColor;
    [SerializeField] private Color m_Normal, m_Low;

    [SerializeField] private TextMeshProUGUI m_HealthValue;
    [SerializeField] private GameObject m_UI, m_EndScreen;
    [SerializeField] private AudioSource m_HitSound;

    public bool m_Attacked, m_Dead;

    private Animator m_Animation;
    private int m_CurrentHealth;

    private float m_Delay, m_MaxDelay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        m_Animation = GetComponent<Animator>();

        m_CurrentHealth = PlayerPrefs.GetInt("HP");
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = m_MaxHealth;
        }

        m_Delay = m_MaxDelay;

        m_Healthbar.maxValue = m_MaxHealth;
        m_Healthbar.value = m_CurrentHealth;
        m_HealthColor.color = m_Normal;

        m_HealthValue.SetText(m_CurrentHealth + "/" + m_MaxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            m_CurrentHealth--;
            PlayerPrefs.SetInt("HP", m_CurrentHealth);

            m_Healthbar.value = m_CurrentHealth;
            m_HealthValue.SetText(m_CurrentHealth + "/" + m_MaxHealth);

            m_Attacked = true;
            m_Delay = 0;

            if (m_CurrentHealth <= m_MaxHealth / 3)
            {
                m_HealthColor.color = m_Low;
            }

            if (m_CurrentHealth <= 0)
            {
                Dead();
            }
            else
            {
                m_Animation.SetTrigger("Hurt");
            }

            GameObject hit = Instantiate(m_Hit, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(hit, 0.5f);

            float pitch = Random.Range(0.5f, 1.5f);
            m_HitSound.pitch = pitch;
            m_HitSound.Play();
        }
    }

    public void Dead()
    {
        m_Animation.SetTrigger("Death");
        Destroy(gameObject, 4f);

        m_Dead = true;
        m_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += 1 * Time.deltaTime;
        }
        else
        {
            m_Attacked = false;
        }
    }

    public void EndScreen()
    {
        m_EndScreen.SetActive(true);
    }
}
