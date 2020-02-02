using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealControl : MonoBehaviour
{
       public Transform heal;
    public float timer = 20.0f;
    private bool doTick = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ticker(timer));    
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

    private void Spawn(){    
        Debug.Log("OverMind: Ticker spawn");  
        float x =  Random.Range(-4.0f, 4.0f);        
        float y =  x;
        if (x != 0.0f && y != 0)
        {
            Instantiate(heal, new Vector3(x, y, 0), Quaternion.identity);            
        }
    }
}
