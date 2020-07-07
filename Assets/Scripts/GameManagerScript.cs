using Interfaces;
using UnityEngine;
using Zenject;

public class GameManagerScript : MonoBehaviour
{
    private IGameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Inject]
    public void Setup(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _gameManager.StopMovingPlatform();
    }
}