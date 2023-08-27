using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    // Список объектов, которые следует активировать при начале игры
    [SerializeField] private GameObject[] _objectsToActivate;
    [SerializeField] private GameObject _objectToDeactivate;

    // Переменная, которая показывает, инициализировалась ли игра или нет
    public bool _gameInitialized = false;

    private void Awake()
    {
        InitializeGame(false);
    }

    private void Update()
    {
        // Если игра еще не инициализирована и была нажата любая клавиша
        if (!_gameInitialized && Input.anyKeyDown)
        {
            InitializeGame(true);
            _objectToDeactivate.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void InitializeGame(bool isActive)
    {
        // Активируем каждый объект из списка
        foreach (GameObject obj in _objectsToActivate)
        {
            obj.SetActive(isActive);
        }

        _gameInitialized = isActive;
    }
}