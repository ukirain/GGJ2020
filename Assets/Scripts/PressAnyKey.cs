using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {           
            Debug.Log("Pressed any key");
            SceneManager.LoadScene("Menu");
        }
    }
    
    void OnGUI()
    {               
        GUI.Box(new Rect(235, 40, 265, 45), "Your time:" + DataManager.timer.ToString("#.##"));
    }
}
