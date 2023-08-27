using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private int _initialTailSize;
    [SerializeField] private float _tickDuration; // Интервал времени между каждым шагом
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private int _addPointsPerFood;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private float _delayTailColliderDisable;

    private List<Transform> _tail = new List<Transform>();
    private Vector2 _currentDirection = Vector2.up;
    private Vector2 _lastPosition;
    private float _tickCounter; // Счетчик времени для нашего "тика"
    private bool _shouldGrow = false;
    private bool _collisionsEnabled = false;

    private void Start()
    {
        _tickCounter = 0f;

        for (int i = 0; i < _initialTailSize; i++)
        {
            GrowTail();
        }

        // Отключаем коллайдеры на начальный момент
        foreach (Transform tailSegment in _tail)
        {
            Collider2D tailCollider = tailSegment.GetComponent<Collider2D>();
            if (tailCollider)
            {
                tailCollider.enabled = false;
            }
        }

        // Включим коллайдеры через 2 секунды
        Invoke("EnableColliders", _delayTailColliderDisable);
    }

    private void Update()
    {
        HandleInput();

        _tickCounter += Time.deltaTime;
        if (_tickCounter >= _tickDuration)
        {
            _tickCounter = 0f;
            Move();
            UpdateTail();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _currentDirection != Vector2.down)
        {
            _currentDirection = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 0); // Поворот на 0 градусов
        }

        if (Input.GetKeyDown(KeyCode.S) && _currentDirection != Vector2.up)
        {
            _currentDirection = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 180); // Поворот на 180 градусов
        }

        if (Input.GetKeyDown(KeyCode.A) && _currentDirection != Vector2.right)
        {
            _currentDirection = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 90); // Поворот на 90 градусов
        }

        if (Input.GetKeyDown(KeyCode.D) && _currentDirection != Vector2.left)
        {
            _currentDirection = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, -90); // Поворот на -90 градусов
        }
    }

    private void Move()
    {
        _lastPosition = transform.position;
        transform.Translate(_currentDirection * _moveSpeed, Space.World);
    }

    private void UpdateTail()
    {
        if (_tail.Count == 0)
            return;

        Vector2 lastTailSegmentPosition = _tail[_tail.Count - 1].position; // Сохраняем позицию последнего сегмента хвоста

        for (int i = _tail.Count - 1; i > 0; i--) // Начнем со сдвига с конца хвоста
        {
            _tail[i].position = _tail[i - 1].position;
        }

        _tail[0].position = _lastPosition;

        if (_shouldGrow)
        {
            GrowTail(lastTailSegmentPosition); // Передаем позицию последнего сегмента
            _shouldGrow = false;
        }
    }

    public void GrowTail()
    {
        Vector2 newPosition = (Vector2)transform.position;

        if (_tail.Count > 0)
        {
            newPosition = (Vector2)_tail[_tail.Count - 1].position - _currentDirection;
        }

        GameObject tailObject = Instantiate(_tailPrefab, newPosition, Quaternion.identity);
        _tail.Add(tailObject.transform);
    }


    public void GrowTail(Vector2 lastTailSegmentPosition)
    {
        GameObject tailObject = Instantiate(_tailPrefab, lastTailSegmentPosition, Quaternion.identity);
        _tail.Add(tailObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_collisionsEnabled) return;

        Food food = collision.GetComponent<Food>();
        if (food != null)
        {
            _shouldGrow = true;
            Destroy(collision.gameObject); // Удаляем еду после "поедания".
            _foodSpawner.SpawnFood();
            _scoreCounter.AddPoints(_addPointsPerFood);
        }

        Tail tail = collision.GetComponent<Tail>();
        if (tail != null)
        {
            _gameOverHandler.OnGameOver();
            Debug.Log("OnTriggerEnter2D: tail");
        }
    }

    private void EnableColliders()
    {
        foreach (Transform tailSegment in _tail)
        {
            Collider2D tailCollider = tailSegment.GetComponent<Collider2D>();
            if (tailCollider)
            {
                tailCollider.enabled = true;
            }
        }

        _collisionsEnabled = true;
    }
}