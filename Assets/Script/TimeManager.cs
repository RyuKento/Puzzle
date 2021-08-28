using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text m_timerText;
    public static Text m_timerTextCopy,m_scoreTextCopy;
    [SerializeField] public float m_time = 60,m_timeCopy = 60;
    [SerializeField] public static int m_seconds = 0,m_secondsTenLine = 0,m_minutes = default,m_timeClone;
    bool isOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        m_timerTextCopy = m_timerText;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_seconds < 0) 
        {
            return;
        }
        m_time -= Time.deltaTime;
        m_timeClone = (int)m_time;
        m_secondsTenLine = m_timeClone / 10;
        m_seconds = m_timeClone - (m_secondsTenLine * 10);

        if (m_timeClone >= 60)
        {
            m_minutes = 1;
            m_seconds -= 60;

        }
        m_timerText.text = m_minutes + ":" + m_secondsTenLine.ToString() + m_seconds.ToString();

        if(m_timeClone <= 0 && isOnce ==false)
        {
            m_time = m_timeCopy;
            SceneManager.LoadScene("Result");
            isOnce = true;
        }
    }
}
