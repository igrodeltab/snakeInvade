using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToToggle; // Объект, который мы хотим переключать
    [SerializeField] private GameInitializer _gameInitializer;
    [SerializeField] private float toggleInterval; // Интервал в секундах

    private float timer; // Счетчик времени

    private void Awake()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        // Прибавляем время, прошедшее с последнего кадра
        timer += Time.deltaTime;

        if(_gameInitializer._gameInitialized == false)
        {
            // Проверяем, прошло ли достаточно времени
            if (timer >= toggleInterval)
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf); // Переключаем активность объекта
                timer = 0.0f; // Сбрасываем счетчик времени
            }
        } else if (_gameInitializer._gameInitialized == true)
        {
            objectToToggle.SetActive(false);
        }
    }
}