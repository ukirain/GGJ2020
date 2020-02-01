using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMind : MonoBehaviour
{
    // public GameObject bunker;
    public Transform brick;
    public GameObject bunker;
    public float radius = 5.0f;
    public float awaitingTime = 1f;
    public float timerSpawn = 0.8f;
    public int countAliens = 0;
    public int maxAliens = 50;
    bool doTick = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
       yield return new WaitForSeconds(awaitingTime);

       // following code
       StartCoroutine(Ticker(timerSpawn));    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Ticker(float period)
    {
         while(doTick)
         {
           Spawn();
           yield return new WaitForSeconds(period);
         }
    }

    private void Spawn(){    
        Debug.Log("OverMind: Ticker spawn");  
        float x =  Random.Range(-radius, radius);        
        float y =  Mathf.Sign(Random.Range(-1.0f, 1.0f)) * Mathf.Sqrt(radius * radius - (x - transform.position.x) * (x - transform.position.x));
        if (x != 0.0f && y != 0 && countAliens < maxAliens)
            Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity);
    }

}
