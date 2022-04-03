using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private int m_AttackForce;
    [SerializeField] private Transform m_RotatePivot, m_SlashPivot;
    [SerializeField] private GameObject m_SlashEffect1, m_SlashEffect2, m_SlashEffect3;
    [SerializeField] private GameObject m_AttackCollider;

    [SerializeField] private AudioSource m_Swing;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animation;
    private Player_Movement m_Movement;
    private Player_Stats m_Stats;

    private int m_AttackState;

    private float m_AttackDelay, m_MaxAttackDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
        m_Movement = GetComponent<Player_Movement>();
        m_Stats = GetComponent<Player_Stats>();

        m_AttackDelay = m_MaxAttackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AttackDelay < m_MaxAttackDelay)
        {
            m_AttackDelay += 1 * Time.deltaTime;
        }
        else if (Input.GetMouseButton(0) && !m_Stats.m_Attacked && !m_Stats.m_Dead)
        {
            m_Movement.Attack();
            m_AttackDelay = 0;

            m_Animation.SetBool("Running", false);
            m_Animation.SetBool("Walking", false);

            m_Animation.SetTrigger("Attack" + m_AttackState);

            switch (m_AttackState)
            {
                case 0:
                    {
                        GameObject slash = Instantiate(m_SlashEffect1, m_SlashPivot.position, m_SlashPivot.rotation);
                        Destroy(slash, 0.5f);
                        break;
                    }
                case 1:
                    {
                        GameObject slash = Instantiate(m_SlashEffect2, m_SlashPivot.position, m_SlashPivot.rotation);
                        Destroy(slash, 0.5f);
                        break;
                    }
                case 2:
                    {
                        GameObject slash = Instantiate(m_SlashEffect3, m_SlashPivot.position, m_SlashPivot.rotation);
                        Destroy(slash, 0.5f);
                        break;
                    }
            }

            m_AttackState++;

            if (m_AttackState > 2)
            {
                m_AttackState = 0;
            }

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                m_Rigidbody.AddForce(m_RotatePivot.right * m_AttackForce);
            }

            GameObject attack = Instantiate(m_AttackCollider, m_SlashPivot.position, m_SlashPivot.rotation);
            Destroy(attack, 0.1f);

            float pitch = Random.Range(0.5f, 1.5f);
            m_Swing.pitch = pitch;
            m_Swing.Play();
        }
    }
}
