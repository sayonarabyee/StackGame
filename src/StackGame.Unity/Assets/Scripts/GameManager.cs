using Interfaces;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    
    private int _platformCounter = 1;
    private IPlatformManager _platformManager;

    [Inject]
    public void Setup(IPlatformManager platformManager)
    {
        _platformManager = platformManager;
    }

    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        
        _platformManager.StopPlatform();
        _platformManager.CreatePlatform(prefab, _platformCounter);
        _platformCounter++;
    }
}