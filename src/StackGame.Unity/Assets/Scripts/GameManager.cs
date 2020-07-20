using Interfaces;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    public IPlatformManager PlatformManager { get; private set; }
    public bool IsGameOver;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        PlatformManager = new PlatformManager();
        IsGameOver = false;
        PlatformManager.CreatePlatform(prefab);
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
        PlatformManager.CreatePlatform(prefab);
    }
}