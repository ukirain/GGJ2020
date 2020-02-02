using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunkerMind : MonoBehaviour
{
    public int hp = 100;
    public int maxhp = 100;
    GameObject[] spawnPoints;
    int index;
    GameObject currentPoint;
    public float awaitingTime = 1.5f;
    bool doTick = true;
    public int attackLow = 1;
    public int attackHigh = 4;
    public float speedAttack = 0.5f;
    public bool canAttack = true;
    public Slider slider;
    public Sprite[] spritesBunker;
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
        
        if (hp < 1)
        {
            Debug.Log("Game Over");
            //SceneManager.LoadScene("GameOver");
        }
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
                currentPoint.GetComponent<AlienMind>().Hit(Random.Range(attackLow, attackHigh));

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

    public void Hit(int attack)
    {
        hp -= attack;
        Destruction();
        slider.value = (hp * 100/ maxhp); 
    }

    public void Destruction()
    {
        if (maxhp >= hp && hp >= 0.84 * maxhp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[0];
        }

        if (0.84 * maxhp > hp && hp >= 0.68 * maxhp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[1];
        }

        if (0.68 * maxhp > hp && hp >= 0.52 * maxhp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[2];
        }

        if (0.52 * maxhp > hp && hp >= 0.36 * maxhp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[3];
        }

        if (0.20 * maxhp > hp && hp >= 0.1 * maxhp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[4];
        }

        if (0.1 * maxhp > hp)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spritesBunker[5];
        }
    }

}
