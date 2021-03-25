using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{
    public static TutorialMenu instance;
    [SerializeField] private GameObject container;
    private bool showedTutorial = false;
    
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(!showedTutorial)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.isStart = true;
                showedTutorial = true;
                Destroy(container.gameObject);
            }
        }

        if (SceneManager.GetActiveScene().name != "LevelTest1")
            Destroy(this.gameObject);
    }

    public bool ShowedTutorial
    {
        get{return showedTutorial;}
    }
}
