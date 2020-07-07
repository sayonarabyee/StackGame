using Interfaces;
using UnityEngine;
using Zenject;

public class GameManagerScript : MonoBehaviour
{
    public GameObject prefab;
    private int platformCounter = 1;
    private IPlatformManager _platformManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Inject]
    public void Setup(IPlatformManager platformManager)
    {
        _platformManager = platformManager;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        _platformManager.StopPlatform(platformCounter);
        _platformManager.Create(prefab, platformCounter);
        platformCounter++;
    }
}