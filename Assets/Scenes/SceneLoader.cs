using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Метод для загрузки сцены LoadIn_Game
    public void LoadInGameScene()
    {
        SceneManager.LoadScene("LoadIn_Game");
    }

    // Метод для загрузки сцены LoadMain_Menu1
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("LoadMain_Menu 1");
    }
}