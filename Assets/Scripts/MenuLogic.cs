using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuLogic : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ConsoleTest()
    {
        Debug.Log("Button Clicked");
    }

    public void StartGame()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("DemoScene");

        while(!asyncOp.isDone)
        {
            yield return null;
        }
        
    }
}
