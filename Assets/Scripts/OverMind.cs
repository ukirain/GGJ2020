using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverMind : MonoBehaviour
{
    // public GameObject bunker;
    public Transform brick;
    public GameObject bunker;
    public float radius_a = 8.0f;
    public float radius_b = 6.0f;
    public float awaitingTime = 1f;
    public float timerSpawn = 1.5f;
    public float timerGrow = 2.8f;
    public int countAliens = 0;
    public int maxAliens = 50;
    bool doTick = true;
    Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        // following code
        StartCoroutine(Ticker2(timerGrow));    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Ticker(float period)
    {
         while(doTick)
         {
           Debug.Log("Corutine1: Ticker spawn " + period);
           Spawn();
           yield return new WaitForSeconds(period);
         }
    }

    IEnumerator Ticker2(float period)
    {
        while (doTick)
        {
            Debug.Log("Corutine2: Ticker grow");
            Grow();
            yield return new WaitForSeconds(period);
        }
    }

    private void Spawn(){    
        Debug.Log("OverMind: Ticker spawn");  
        float x =  Random.Range(-radius_a, radius_a);
        float rb_2 = radius_b * radius_b;
        float ra_2 = radius_a * radius_a;
        float rbra = rb_2 / ra_2;
        float y =  Mathf.Sign(Random.Range(-1.0f, 1.0f)) * Mathf.Sqrt(rb_2 - rbra * (x - transform.position.x) * (x - transform.position.x));  
        if (x != 0.0f && y != 0 && countAliens < maxAliens)
        {
            Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity);
            countAliens++;
        }
    }

    private void Grow()
    {
        if (timerSpawn > 0.1f)
            timerSpawn -= 0.1f;
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Ticker(timerSpawn));
    }
}
