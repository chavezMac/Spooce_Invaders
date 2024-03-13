using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
   public void RestartGame()
   {
       SceneManager.LoadScene("Menu");
   }

   void Update()
   {
        if(transform.position.y > 720.0f) 
        {
            RestartGame();
        }
   }
}
