using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource mainMenuSource; // AudioSource для музыки главного меню
    public AudioSource inGameSource;   // AudioSource для музыки игровой сцены

    void Awake()
    {
        // Создаем синглтон AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        // Находим AudioSource компоненты в дочерних объектах
      
    }

    void OnEnable()
    {
        // Подписываемся на событие смены сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Отписываемся от события смены сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        print(("Start"));
        // Включаем разную музыку в зависимости от сцены
        switch (scene.name)
        {
            case "Main_Menu":
                PlayMusic(mainMenuSource);
                StopMusic(inGameSource);
                print(("1"));

                break;
            case "In_Game":
                PlayMusic(inGameSource);
                StopMusic(mainMenuSource);
                print(("2"));

                break;
        }
    }

    void PlayMusic(AudioSource source)
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
    void StopMusic(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
}