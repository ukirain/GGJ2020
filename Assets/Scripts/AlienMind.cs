using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMind : MonoBehaviour
{
    public int hp = 7;
    public GameObject[] target;
    public GameObject currentPoint;
    public GameObject player;
    public float targetDistance=0.1f;
    public float speed=0.001f;
    private bool bunkerNear = false;
    public bool canAttack = true;
    public float senseRadius = 4.0f;
    // Start is called before the first frame update
    void Start()
    {        
        player = GameObject.Find("Player");
        target = GameObject.FindGameObjectsWithTag("Bunker");
        SelectEnemy();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(senseRadius > Vector3.Distance(player.transform.position, transform.position))                   
            currentPoint = player;  
        else
            SelectEnemy();
       

        if(!bunkerNear)
            Walking();
        if(hp <= 0)
        {
            Debug.Log("Destroyed alien");
            Destroy(gameObject);
            Camera.main.GetComponent<OverMind>().countAliens -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Alien CollisionEnter: Collide");
        if (other.collider.tag == "Bunker")
            bunkerNear = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    void Walking()
    {        
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, step); 
    }

    void SelectEnemy()
    {   
        
        float delta;
        float minDelta = Vector3.Distance(target[0].transform.position, transform.position);
        int minIndex = 0;
        for(int i = 0; i < target.Length; i++)
        {
            delta = Vector3.Distance(target[i].transform.position, transform.position);
            if (minDelta > delta)
            {
                Debug.Log("Alien want to choose Index: " + minIndex  + " Delta: " + delta);
                minDelta = delta;
                minIndex = i;
            }
        }    
        Debug.Log("Alien attack Index: " + minIndex);
        Debug.Log("Alien attack Length: " + target.Length);     

        currentPoint = target[minIndex];
        
    }

    void Attack()
    {

    }
}
