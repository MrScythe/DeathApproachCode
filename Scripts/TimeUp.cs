using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private AudioSource m_Time;
    private Animator m_Animation;

    // Start is called before the first frame update
    void Start()
    {
        m_Animation = GetComponent<Animator>();
    }

    public void AddTime(int seconds)
    {
        m_Text.SetText("+" + seconds + " sec");
        m_Animation.SetTrigger("Time");
        m_Time.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
