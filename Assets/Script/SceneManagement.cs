using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnClick()
    {
        SceneManager.LoadScene("Title");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
