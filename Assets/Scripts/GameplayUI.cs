using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    public void RestartGame(){
        SceneManager.LoadScene("Gameplay");

    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("Main Menu");

    }
}
