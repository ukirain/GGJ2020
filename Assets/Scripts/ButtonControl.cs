using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonAuthors;
    public Button buttonTutorial;
    public Button buttonExit;
    public Image TutorialImage;
    public Image AuthorsImage;
    public AudioSource music;
    public bool vision = false;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.timer = 0.0f;
        buttonPlay.GetComponent<Button>().onClick.AddListener(Play);
        buttonAuthors.GetComponent<Button>().onClick.AddListener(Authors);
        buttonTutorial.GetComponent<Button>().onClick.AddListener(Tutorial);
        buttonExit.GetComponent<Button>().onClick.AddListener(Exit);
        DontDestroyOnLoad(music);
    }

    // Update is called once per frame
    void Update()
    {
        if(vision && Input.anyKey){
            AuthorsImage.gameObject.SetActive(false);
            TutorialImage.gameObject.SetActive(false);
        }
    }

    public void Play() {
        Debug.Log("Menu: Play");
        SceneManager.LoadScene("SampleScene");
    }

    public void Authors() {
        Debug.Log("Menu: Authors");
        AuthorsImage.gameObject.SetActive(true);
        vision = true;
    }

    public void Tutorial() {
        Debug.Log("Menu: Tutorial");        
        TutorialImage.gameObject.SetActive(true);
        vision = true;
    }

    public void Exit() {
        Debug.Log("Menu: Exit");
        Application.Quit();
    }
}
