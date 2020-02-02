﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    
    private Vector3 objPosition;
    private bool move;
    public float speedMove;
    public int maxHealth = 100;
    public int curHealth = 100;
    public int healLow = -4;
    public int healHigh = -6;
    


    public float healthBarLength;

    void Start()
    {
        healthBarLength = Screen.width / 6;
        objPosition = transform.position;
        move = true;
    }

    void Update()
    {
        AddjustCurrentHealth(0);

        if (DataManager.timer < 99999.0f) {
            DataManager.timer += Time.deltaTime;
        }

        if (curHealth < 1)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }

        // нажали правую кнопку мыши
        if (Input.GetMouseButtonDown(1))
        {
            move = true;
            // движение гг
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            move = false;
            // движение гг
            // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            // objPosition = transform.position;
            float step = speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + Time.deltaTime * speedMove * Mathf.Sign(Input.GetAxis("Horizontal")), transform.position.y, transform.position.z), step);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            move = false;
            // движение гг
            // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            // objPosition = transform.position;
            float step = speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speedMove * Mathf.Sign(Input.GetAxis("Vertical")), transform.position.z), step);
        }

        if (move == true)
        {
            float step = speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(objPosition.x, objPosition.y, transform.position.z), step);
        }

        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //move = false;
        // нажали пробел, чиним бункер
        if (Input.GetKeyDown(KeyCode.Space) && collision.collider.tag == "Bunker")
        {
            Debug.Log("Attack " + collision.gameObject.GetComponent<BunkerMind>().hp);
            collision.gameObject.GetComponent<BunkerMind>().Hit(Random.Range(healLow, healHigh));
            gameObject.GetComponent<Animator>().SetBool("repair",true);
        }

       
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Player collision " + other.collider.tag);
         if(other.collider.tag == "Heal"){
            curHealth += 10;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("repair", false);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(400, 10, 450, 20), curHealth + "/" + maxHealth);        
        GUI.Box(new Rect(400, 30, 450, 40), "Time:" + DataManager.timer.ToString("#.##"));
    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;

        healthBarLength = (Screen.width / 6) * (curHealth / (float)maxHealth);
    }

    public void Hit(int attack)
    {
        curHealth -= attack;
    }
}
