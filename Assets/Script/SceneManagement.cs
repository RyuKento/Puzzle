using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Title");
        Birds.m_score = default;
    }
}
