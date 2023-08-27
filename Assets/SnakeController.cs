using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private int _initialTailSize;
    [SerializeField] private float _tickDuration; // Интервал времени между каждым шагом
    [SerializeField] private FoodSpawner _foodSpawner;

    private List<Transform> _tail = new List<Transform>();
    private Vector2 _currentDirection = Vector2.up;
    private Vector2 _lastPosition;
    private float _tickCounter = 0f; // Счетчик времени для нашего "тика"

    private void Start()
    {
        for (int i = 0; i < _initialTailSize; i++)
        {
            GrowTail();
        }
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

        _tail[0].position = _lastPosition;
        for (int i = 1; i < _tail.Count; i++)
        {
            Vector2 prevPosition = _tail[i - 1].position;
            _tail[i].position = prevPosition;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        Food food = collision.GetComponent<Food>();
        if (food != null)
        {
            GrowTail();
            Destroy(collision.gameObject); // Удаляем еду после "поедания".
            _foodSpawner.SpawnFood();
        }
        /*
        else if (collision.GetComponent<Transform>() != null && _tail.Contains(collision.GetComponent<Transform>()))
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
        } */
    }
}