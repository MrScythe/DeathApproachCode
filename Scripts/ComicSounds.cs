using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicSounds : MonoBehaviour
{
    [SerializeField] private AudioSource m_Whoosh, m_Bang;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Whoosh()
    {
        m_Whoosh.Play();
    }
    public void Bang()
    {
        m_Bang.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
