using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    public IPlatformManager PlatformManager { get; private set; }
    public IScoreManager ScoreManager { get; private set; }
    public bool IsGameOver;

    private List<MovingPlatform> _platforms; 

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        PlatformManager = new PlatformManager();
        ScoreManager = new ScoreManager();
        IsGameOver = false;
        _platforms = new List<MovingPlatform>
        {
            PlatformManager.CreatePlatform(prefab)
        };
    }

    private void OnMouseDown()
    {
        UpdateGameState();
    }

    public void UpdateGameState()
    {
        PlatformManager.StopPlatform();
        
        if (PlatformManager.PlatformMissed())
        {
            IsGameOver = true;
            return;
        }
        
        PlatformManager.CutPlatform();
        ScoreManager.UpdateScore(_platforms.LastOrDefault(), _platforms.ElementAtOrDefault(_platforms.Count-2));
        _platforms.Add(PlatformManager.CreatePlatform(prefab));
    }
}