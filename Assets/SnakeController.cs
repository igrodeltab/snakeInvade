using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _timeSinceLastMoveMax;
    private Direction _direction = Direction.Up;
    private float _timeSinceLastMove;

    private Transform _transform;
    private List<Vector2> _positions = new List<Vector2>();

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        HandleInput();

        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= _timeSinceLastMoveMax / _moveSpeed)
        {
            Move();
            _timeSinceLastMove = 0f;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) SetDirection(Direction.Up);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) SetDirection(Direction.Down);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) SetDirection(Direction.Left);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) SetDirection(Direction.Right);
    }

    private void SetDirection(Direction newDirection)
    {
        if ((newDirection == Direction.Up && _direction == Direction.Down) ||
            (newDirection == Direction.Down && _direction == Direction.Up) ||
            (newDirection == Direction.Left && _direction == Direction.Right) ||
            (newDirection == Direction.Right && _direction == Direction.Left))
            return;

        _direction = newDirection;
    }

    private void Move()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 moveDirection = directions[(int)_direction];

        Vector2 newPosition = (Vector2)_transform.position + moveDirection;

        // Установка угла вращения головы змеи в зависимости от направления движения
        float angle = 0;
        switch (_direction)
        {
            case Direction.Up:
                angle = 0;
                break;
            case Direction.Right:
                angle = 270;
                break;
            case Direction.Down:
                angle = 180;
                break;
            case Direction.Left:
                angle = 90;
                break;
        }

        _transform.rotation = Quaternion.Euler(0, 0, angle);

        _positions.Insert(0, newPosition);
        _transform.position = newPosition;
    }
}