using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text m_scoreText;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(ResultPop());
    }

    IEnumerator ResultPop()
    {
        yield return new WaitForSeconds(1.0f);
        m_scoreText.text = Birds.m_score.ToString();
    }
}
