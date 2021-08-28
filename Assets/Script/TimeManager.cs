using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text m_timerText;
    public static Text m_timerTextCopy,m_scoreTextCopy;
    [SerializeField] public float m_time = 60;
    [SerializeField] public static int m_seconds = 1,m_minutes = default;
    bool isOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        m_timerTextCopy = m_timerText;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_seconds == 0) 
        {
            return;
        }
        m_time -= Time.deltaTime;
        m_seconds = (int)m_time;
        
        if(m_seconds >= 60)
        {
            m_minutes = 1;
            m_seconds -= 60;
        }
        m_timerText.text = m_minutes + ":" + m_seconds.ToString();

        if(m_seconds <= 0 && isOnce ==false)
        {
            SceneManager.LoadScene("Result");
            isOnce = true;
        }
    }
}
