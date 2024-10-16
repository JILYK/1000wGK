using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Slider loadingSlider;  // Указатель на ваш Slider
    public string sceneName = "NextScene";  // Название сцены, которую нужно загрузить

    void Start()
    {
        // Начать загрузку уровня
        StartCoroutine(FakeLoadLevel());
    }

    IEnumerator FakeLoadLevel()
    {
        float elapsedTime = 0f;
        float waitTime = 0.45f; //TODO Change it to 3
        float progress = 0f;

        while (progress < 1.0f)
        {
            elapsedTime += Time.deltaTime;

            // Используем более широкий диапазон для speedModifier для заметных изменений скорости
            float speedModifier = Random.Range(0.2f, 2.0f); // Больший диапазон значений

            // Применяем модификатор скорости с учетом максимальной величины прогресса
            progress += Time.deltaTime / waitTime * speedModifier;
            progress = Mathf.Min(progress, 1f);  // Убедимся, что прогресс не превышает 1

            // Обновляем слайдер
            loadingSlider.value = progress;

            // Ожидаем следующий кадр
            yield return null;
        }

        // После заполнения слайдера загружаем нужную сцену
        SceneManager.LoadScene(sceneName);
    }
}