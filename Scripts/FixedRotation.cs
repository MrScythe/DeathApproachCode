using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    [SerializeField] private Quaternion m_OriginalRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_OriginalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_OriginalRotation;
    }
}
