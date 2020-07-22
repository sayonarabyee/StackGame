using Interfaces;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    public Camera gameCamera;
    public bool isGameOver;

    public IPlatformManager PlatformManager { get; private set; }
    public ICameraManager CameraManager { get; private set; }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        PlatformManager = new PlatformManager();
        CameraManager = new CameraManager(gameCamera);
        isGameOver = false;
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
            isGameOver = true;
            return;
        }
        
        PlatformManager.CutPlatform();
        CameraManager.MoveUp(Constants.MovingPlatform.InitialScaleY);
        PlatformManager.CreatePlatform(prefab);
    }
}