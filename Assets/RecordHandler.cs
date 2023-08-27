using UnityEngine;

public class RecordHandler : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HighScore"; // Ключ для сохранения и извлечения рекорда из PlayerPrefs

    // Установка нового рекорда, если текущий счет больше максимального рекорда
    public void CheckAndSetHighScore(int currentScore)
    {
        int highScore = GetHighScore();

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, currentScore);
            PlayerPrefs.Save(); // Сохранение изменений
        }
    }

    // Получение максимального рекорда
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0); // Если рекорд не найден, возвращаем 0
    }
}