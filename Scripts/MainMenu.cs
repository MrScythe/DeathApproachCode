using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_ComicButton;
    [SerializeField] private TextMeshProUGUI m_Score;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Play") > 0)
        {
            m_ComicButton.SetActive(true);
        }

        m_Score.SetText("Lowest Floor: " + PlayerPrefs.GetInt("LowestFloor"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
