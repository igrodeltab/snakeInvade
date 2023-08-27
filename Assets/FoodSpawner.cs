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
        // Генерация случайных координат в пределах границ экрана
        int randomX = Mathf.FloorToInt(Random.Range(_minBounds.x, _maxBounds.x) / _cellSize);
        int randomY = Mathf.FloorToInt(Random.Range(_minBounds.y, _maxBounds.y) / _cellSize);

        Vector2 spawnPosition = new Vector2(randomX * _cellSize, randomY * _cellSize);

        Instantiate(_foodPrefab, spawnPosition, Quaternion.identity);
    }
}