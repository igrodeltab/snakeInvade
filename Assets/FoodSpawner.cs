using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _borderOffset; // Отступ от границы экрана
    [SerializeField] private SnakeController _snakeController;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private float _cellSize; // Размер клетки сетки

    private void Start()
    {
        _cellSize = _snakeController.GetMoveSpeed();

        _minBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Учтем отступ от границы экрана
        _minBounds += new Vector2(_borderOffset, _borderOffset);
        _maxBounds -= new Vector2(_borderOffset, _borderOffset);

        SpawnFood();
    }

    public void SpawnFood()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        if (spawnPosition != Vector2.zero) // Если получена корректная позиция
        {
            Instantiate(_foodPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetSpawnPosition()
    {
        int maxAttempts = 1000; // Максимальное количество попыток
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            int randomX = Mathf.FloorToInt(Random.Range(_minBounds.x, _maxBounds.x) / _cellSize);
            int randomY = Mathf.FloorToInt(Random.Range(_minBounds.y, _maxBounds.y) / _cellSize);

            Vector2 potentialPosition = new Vector2(randomX * _cellSize, randomY * _cellSize);

            if (!_snakeController.IsPositionOnTail(potentialPosition))
            {
                return potentialPosition; // Позиция подходит для спавна
            }

            currentAttempt++;
        }

        return Vector2.zero; // Не удалось найти подходящую позицию после всех попыток
    }
}