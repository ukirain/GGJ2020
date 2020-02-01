using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerMind : MonoBehaviour
{

    GameObject[] spawnPoints;
    int index;
    GameObject currentPoint;
    public float awaitingTime = 1.5f;
    bool doTick = true;
    public int attackLow = 1;
    public int attackHigh = 4;
    public float speedAttack = 0.5f;
    public bool canAttack = true;
    // Start is called before the first frame update
    IEnumerator Start()
    {
       yield return new WaitForSeconds(awaitingTime);

       // following code
       StartCoroutine(Ticker(speedAttack));    
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Enemy");  
    }

    IEnumerator Ticker(float period)
    {
         while(doTick)
         {
           if(canAttack)
            Attack();
           yield return new WaitForSeconds(period);
         }
    }

    void Attack()
    {
        if(spawnPoints.Length > 0){
            SelectEnemy();
            if(currentPoint != null){
                Debug.Log("Attack " + currentPoint.GetComponent<AlienMind>().hp);
                currentPoint.GetComponent<AlienMind>().hp -= Random.Range(attackLow, attackHigh);
            }        
        }
    }

    //выбирает ближайшего врага
    void SelectEnemy()
    {   
        
        float delta;
        float minDelta = Vector3.Distance(spawnPoints[0].transform.position, transform.position);;
        int minIndex = 0;
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            delta = Vector3.Distance(spawnPoints[i].transform.position, transform.position);
            if (minDelta > delta)
            {
                minDelta = delta;
                minIndex = i;
            }
        }    
        Debug.Log("Index: " + minIndex);
        Debug.Log("Lenght: " + spawnPoints.Length);    
        currentPoint = spawnPoints[minIndex];                        
    }

}
