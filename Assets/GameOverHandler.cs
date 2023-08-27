using UnityEngine;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel; // Панель с сообщением или UI поражения
    [SerializeField] private TextMeshProUGUI _resultScoreText; // Ссылка на TextMeshProUGUI компонент
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private GameInitializer _gameInitializer;

    private void Awake()
    {
        _gameOverPanel.SetActive(false);
    }

    // Событие, которое будет вызвано при поражении
    public void OnGameOver()
    {
        // Остановить игру (пауза)
        Time.timeScale = 0;

        // Активировать панель поражения
        if (_gameOverPanel)
        {
            _resultScoreText.text = "Score: " + _scoreCounter.GetCurrentScore().ToString();
            _gameOverPanel.SetActive(true);
        }

        _gameInitializer.InitializeGame(false);
    }

    // Метод для перезапуска уровня
    public void RestartGame()
    {
        Time.timeScale = 1; // Возобновить игру
    }
}