using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Main Variables")]
    Animator anim;

    [Header("Instantiate Player")]
    public GameObject[] mainCharacters;
    public Transform spawnPlayer;

    public bool startFacingRight;
    public static bool canFlipOnStart;
    private int numInt;

    [Header("States")]
    public static bool isStart;
    public static bool win;
    public bool isMenu;

    [Header("Fruits")]
    public static int fruitQtd;
    public static int howManyNeeds;
    [SerializeField] private GameObject finalGameMenu;
    private bool canSpawn = true;
    private bool gameWasFinished = false;

    private void Awake()
    {
        canFlipOnStart = false;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        fruitQtd = 0;

        if (startFacingRight)
        {
            canFlipOnStart = true;
        }
    }
    void Update()
    {
        if(TutorialMenu.instance != null)
        {
            if(TutorialMenu.instance.ShowedTutorial)
                SpawnPlayer();
        }
        else
            SpawnPlayer();

        if (fruitQtd >= howManyNeeds && Player.isAlive)
        {
            StartCoroutine(NextLevel());
            fruitQtd = 0;
        }

        if(Player.instance != null)
        {
            if(Player.isDead)
            {
                Invoke("ReloadLevel", 1.3f);
                fruitQtd = 0;
                // howManyNeeds = 0;
            }
        }

        if(gameWasFinished)
        {
            Time.timeScale = 0f;
            isStart = false;
         
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("quit");
                Application.Quit();
            }
        }
    }

    private void SpawnPlayer()
    {
        if(canSpawn)
        {
            numInt = Random.Range(0, mainCharacters.Length);
            var playerClone = Instantiate(mainCharacters[numInt], spawnPlayer.transform.position, Quaternion.identity);
            canSpawn = false;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        fruitQtd = 0;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (SceneManager.GetActiveScene().name == "LevelTest4")
        {
            finalGameMenu.SetActive(true);
            
            gameWasFinished = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
