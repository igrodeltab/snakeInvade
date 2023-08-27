using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private int _currentScore; // Текущий счет

    [SerializeField] private TextMeshProUGUI _scoreText; // Ссылка на TextMeshProUGUI компонент

    private void Awake()
    {
        _currentScore = 0;
    }

    private void Start()
    {
        UpdateScoreText(); // Инициализируем начальный текст счета
    }

    // Метод для добавления очков
    public void AddPoints(int points)
    {
        _currentScore += points;
        UpdateScoreText();
    }

    // Метод для обновления текста счета в TextMeshProUGUI
    private void UpdateScoreText()
    {
        _scoreText.text = _currentScore.ToString();
    }

    // Метод для получения текущего счета
    public int GetCurrentScore()
    {
        return _currentScore;
    }

    // Метод для сброса счета
    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreText();
    }
}