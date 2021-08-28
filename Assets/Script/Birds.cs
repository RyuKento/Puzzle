using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Birds : MonoBehaviour
{
    public GameObject[] birdPrefabs,allBirdsList,allStarList;
    public GameObject m_star,m_starCopy;
    public Text m_allDepop;
    public int allBirdsCount;
    public float duration = 0.5f, currentTime = 0f;
    public static int m_score;

    [SerializeField] private float removeBirdMinCount = 3,birdDistance = 1.6f;
    [SerializeField] Text m_scoreBox;

    private GameObject firstBird,lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>(),ExplosionList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        TouchManager.Began += (info) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider) 
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.tag =="Bird")
                {
                    firstBird = hitObj;
                    lastBird = hitObj;
                    currentName = hitObj.name;
                    removableBirdList = new List<GameObject>();
                    PushToBirdList(hitObj);
                }
                else if(hitObj.tag == "Star")
                {
                    firstBird = hitObj;
                    lastBird = hitObj;
                    ExplosionList = new List<GameObject>();
                    m_star = hitObj;
                    currentName = hitObj.name;
                    PushToStar(hitObj);

                }
            } 
        };

        TouchManager.Moved += (info) =>
        {
            if (!firstBird)
            {
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider)
            {
                GameObject hitObj = hit.collider.gameObject;
                if(hitObj.tag == "Bird" && hitObj.name==currentName && hitObj!=lastBird&&0> removableBirdList.IndexOf(hitObj))
                {
                    float distance = Vector2.Distance(hitObj.transform.position,
                        lastBird.transform.position);
                    if(distance > birdDistance)
                    {
                        return;
                    }
                    lastBird = hitObj;
                    PushToBirdList(hitObj);
                }else if(hitObj.tag == "Star" && hitObj.name == currentName && hitObj != lastBird && 0 > removableBirdList.IndexOf(hitObj))
                {
                    float distance = Vector2.Distance(hitObj.transform.position, lastBird.transform.position);
                    if(distance > birdDistance)
                    {
                        return;
                    }
                    lastBird = hitObj;
                    PushToStar(hitObj);
                }
            }

        };
        TouchManager.Ended += (info) =>
        {
            int removeCount = removableBirdList.Count;
            int starCount = ExplosionList.Count; 
            if(removeCount >= removeBirdMinCount && 7 > removeCount)
            {
                AddtionalPoints(removeCount);
                foreach(GameObject obj in removableBirdList)
                {
                    Destroy(obj);
                }
                StartCoroutine(DropBirds(removeCount));
            }
            else if(removeCount >= 7)
            {
                AddtionalPoints(removeCount);
                foreach(GameObject obj in removableBirdList)
                {
                    Destroy(obj);
                }
                StartCoroutine(DropStar(1));
                StartCoroutine(DropBirds(removeCount));
            }
            else if (starCount >= 2)
            {

                foreach (GameObject obje in allBirdsList)
                {
                    Destroy(obje);
                }
                foreach (GameObject objec in ExplosionList)
                {
                    Destroy(objec);
                }
                m_star = m_starCopy;
                StartCoroutine(PopText());
                StartCoroutine(DropBirds(40));
                StartCoroutine(DepopText());
            }
            foreach (GameObject obj in removableBirdList)
            {
                ChangeColor(obj, 1.0f);
            }
            foreach (GameObject obj in ExplosionList)
            {
                ChangeColor(obj, 1.0f);
            }
            removableBirdList = new List<GameObject>();
            ExplosionList = new List<GameObject>();

            
            firstBird = null;
            lastBird = null;
        };
        StartCoroutine(DropBirds(40));
        if (TimeManager.m_seconds >= 0)
        {
            return;
        }
    }
    private void Update()
    {
        m_scoreBox.text = m_score.ToString();
    }
    private void PushToBirdList(GameObject obj)
    {
        removableBirdList.Add(obj);
        ChangeColor(obj, 0.8f);
    }
    private void PushToStar(GameObject obj)
    {
        ExplosionList.Add(obj);
        allBirdsList = GameObject.FindGameObjectsWithTag("Bird");
        allStarList = GameObject.FindGameObjectsWithTag("Star");
        ChangeColor(obj, 0.8f);
    }
    private void ChangeColor(GameObject obj,float transparency)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            transparency);
    }

    private void AddtionalPoints(int Count)
    {
        m_score += 100 * Count;
    }

    private void AllPoints()
    {
        m_score += 5000;
    }
    IEnumerator DropBirds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-7f, 7f), 13.5f);

            int id = Random.Range(0, birdPrefabs.Length);
            Debug.Log(id,this.gameObject);
            GameObject bird = (GameObject)Instantiate(birdPrefabs[id],
                pos,
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
            bird.name = "Bird" + id;

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator DropStar(int count)
    {
        Vector2 pos = new Vector2(Random.Range(-7f, 7f), 13.5f);

        GameObject star = (GameObject)Instantiate(m_star, pos, Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
        star.name = "StarBall";

        yield return new WaitForSeconds(0.05f); 
    }
    IEnumerator PopText()
    {
        float alpha = 1;
        Color color = new Color(0, 0, 0, alpha);
        currentTime = 0f;
        m_allDepop.color = color;
        m_allDepop.text = "ナイスぜんけし！";
        do
        {
            yield return null;

            currentTime += Time.unscaledDeltaTime;
            alpha = 1 - (currentTime / duration);
            if (alpha < 0)
            {
                alpha = 0;
            }
            color.a = alpha;
            m_allDepop.color = color;
        }
        while (currentTime <= duration);
    }
    IEnumerator DepopText() 
    {
        yield return new WaitForSeconds(0.5f);
        float alpha = 0;
        Color color = new Color(0, 0, 0, alpha);
        currentTime = 0;
        m_allDepop.color = color;
        do
        {
            yield return null;
            currentTime += Time.unscaledDeltaTime;
            alpha = currentTime / duration;
            if (alpha > 1)
            {
                alpha = 1;
            }
            color.a = alpha;
            m_allDepop.color = color;
        }
        while (currentTime <= duration);
        m_allDepop.text = "";
    }
}

   
    
