using Interfaces;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    public IPlatformManager PlatformManager { get; private set; }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        PlatformManager = new PlatformManager();
        PlatformManager.CreatePlatform(prefab);
    }

    private void OnMouseDown()
    {
        CreateNewPlatform();
    }


    public void CreateNewPlatform()
    {
        PlatformManager.StopPlatform();
        PlatformManager.CutPlatform();
        PlatformManager.CreatePlatform(prefab);
    }
}