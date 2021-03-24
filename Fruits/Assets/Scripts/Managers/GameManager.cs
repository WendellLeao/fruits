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

    private void Awake()
    {
        canFlipOnStart = false;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isStart = true;
        fruitQtd = 0;

        if (startFacingRight)
        {
            canFlipOnStart = true;
        }

        numInt = Random.Range(0, mainCharacters.Length);
        var projectile = Instantiate(mainCharacters[numInt], spawnPlayer.transform.position, Quaternion.identity);

    }
    void Update()
    {
        //Until finish the project
        /*if (Input.anyKey && isMenu)
        {
            isStart = true;
            Player.isAlive = true;
            SceneManager.LoadScene("SampleScene");
            isMenu = false;
        }*/

        if (fruitQtd >= howManyNeeds && Player.isAlive)
        {
            StartCoroutine(NextLevel());
            fruitQtd = 0;

        }

        if(!isStart && !Player.isAlive)
        {
            Invoke("ReloadLevel", 1.3f);
            fruitQtd = 0;
           // howManyNeeds = 0;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        fruitQtd = 0;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
