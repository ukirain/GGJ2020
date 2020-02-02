using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{

    private bool itTime = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ticker(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && itTime)
        {           
            Debug.Log("Pressed any key");
            SceneManager.LoadScene("Menu");
        }
    }
    
    void OnGUI()
    {               
        GUI.Box(new Rect(235, 40, 265, 45), "Your time:" + DataManager.timer.ToString("#.##"));
    }

    IEnumerator Ticker(float period)
    {
        
        Debug.Log("Corutine1: Ticker spawn " + period);
        itTime = true;        
        yield return new WaitForSeconds(period);

    }
}
