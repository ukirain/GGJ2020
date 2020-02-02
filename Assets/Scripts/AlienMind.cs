using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienMind : MonoBehaviour
{
    public int hp = 7;
    public int maxhp = 7;
    public GameObject[] target;
    public GameObject currentPoint;
    public GameObject player;
    public int attackLow = 1;
    public int attackHigh = 4;
    public float targetDistance=0.1f;
    public float speed=0.001f;
    private bool bunkerNear = false;
    public bool canAttack = true;
    public float speedAttack = 0.5f;
    public float awaitingTime = 1.5f;
    public float senseRadius = 4.0f;
    private float scale = 5.0f;
    bool doTick = true;
    bool gg = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {        
        player = GameObject.Find("Player");
        target = GameObject.FindGameObjectsWithTag("Bunker");

        yield return new WaitForSeconds(awaitingTime);

        // following code
        StartCoroutine(Ticker(speedAttack));
    }

    IEnumerator Ticker(float period)
    {
        while (doTick)
        {
            if (canAttack)
                Attack();
            yield return new WaitForSeconds(period);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < target.Length; i++)
        {
            if(target[i].GetComponent<BunkerMind>().hp < 0 && ((i > 0 && gg) || (i == 0)))
            {
                gg = true;
            }
        }

        if (gg)
        {
            Debug.Log("Game Over");
            //SceneManager.LoadScene("GameOver");
        }

        SelectEnemy();


        if (!bunkerNear)
        {
            canAttack = false;
            Walking();
        }
        else
            canAttack = true;

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
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        if (other.collider.tag == "Player")
            bunkerNear = true;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bunkerNear = false;
    }

    void Walking()
    {        
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, step); 
    }

    void SelectEnemy()
    {

        if (senseRadius > Vector3.Distance(player.transform.position, transform.position))
        {
            currentPoint = player;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            return;
        } else
        {

        }
        float delta;
        float minDelta = Vector3.Distance(target[0].transform.position, transform.position);
        int minIndex = 0;
        for(int i = 0; i < target.Length; i++)
        {
            delta = Vector3.Distance(target[i].transform.position, transform.position);
            if (minDelta > delta && target[i].GetComponent<BunkerMind>().hp > 0)
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

    public void Hit(int attack)
    {
        hp -= attack;
        Transform gohp = gameObject.transform.Find("hp");
        gohp.localScale = new Vector3(scale * hp / maxhp, 0.5f, 1.0f);
    }

    void Attack()
    {
       
        SelectEnemy();
        if (currentPoint != null)
        {
            if (currentPoint.tag == "Bunker")
            {
                Debug.Log("Attack " + currentPoint.GetComponent<BunkerMind>().hp);
                currentPoint.GetComponent<BunkerMind>().Hit(Random.Range(attackLow, attackHigh));
            }
            if (currentPoint.name == "Player")
            {
                Debug.Log("Attack player" + currentPoint.GetComponent<PlayerControl>().curHealth);
                currentPoint.GetComponent<PlayerControl>().Hit(Random.Range(attackLow, attackHigh));
            }

        }
       
    }
}
