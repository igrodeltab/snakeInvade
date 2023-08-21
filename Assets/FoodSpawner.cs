using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Префаб еды, который будет спавниться на сцене
    [SerializeField] private GameObject _foodPrefab;
    private Transform _transform;

    // Координаты минимальной и максимальной границ экрана
    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private void Start()
    {
        // Получение трансформа объекта
        _transform = transform;

        // Преобразование координат экрана в координаты мирового пространства
        // для определения границ спавна еды
        _minBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Спавн первой еды
        SpawnFood();
    }

    // Функция для спавна еды на случайных координатах в пределах экрана
    public void SpawnFood()
    {
        // Генерация случайных координат в пределах границ экрана
        float randomX = Random.Range(_minBounds.x, _maxBounds.x);
        float randomY = Random.Range(_minBounds.y, _maxBounds.y);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Инстанцирование префаба еды на сгенерированных координатах
        Instantiate(_foodPrefab, spawnPosition, Quaternion.identity, _transform);
    }
}