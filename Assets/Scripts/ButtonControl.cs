using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Play);
        DontDestroyOnLoad(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        Debug.Log("Menu: Play");
        SceneManager.LoadScene("SampleScene");
    }
}
