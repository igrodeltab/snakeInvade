﻿using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // Список объектов, которые следует активировать при начале игры
    [SerializeField] private GameObject[] _objectsToActivate;

    // Переменная, которая показывает, инициализировалась ли игра или нет
    private bool _gameInitialized = false;

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
        }
    }

    private void InitializeGame(bool isActive)
    {
        // Активируем каждый объект из списка
        foreach (GameObject obj in _objectsToActivate)
        {
            obj.SetActive(isActive);
        }

        _gameInitialized = isActive;
    }
}