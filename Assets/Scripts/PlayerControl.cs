using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 objPosition;
    private bool move;
    public float speedMove;

    void Start()
    {
        objPosition = transform.position;
        move = true;
    }

    void Update()
    {
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
            float temp = transform.position.x + 0.05f * speedMove * Mathf.Sign(Input.GetAxis("Horizontal"));
            transform.position = new Vector3(temp, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            move = false;
            // движение гг
            // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            // objPosition = transform.position;
            float temp  = transform.position.y + 0.05f * speedMove * Mathf.Sign(Input.GetAxis("Vertical"));                       
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
        }

        if (move == true)
        {
            float step = speedMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(objPosition.x, objPosition.y, transform.position.z), step);
        }

        // нажали пробел, чиним бункер
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Save Bunker!!!");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //move = false;
    }
}
