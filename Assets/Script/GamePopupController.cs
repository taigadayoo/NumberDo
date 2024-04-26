using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopupController : MonoBehaviour
{
    public GameObject gamePrefab;
    private GameObject gameInstance;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowGame()
    {
        gameObject.SetActive(true);
        LoadGame();
    }

    public void ClosePopup()
    {
        UnloadGame();
        gameObject.SetActive(false);
    }

    private void LoadGame()
    {
        if (gamePrefab != null)
        {
            gameInstance = Instantiate(gamePrefab, transform);
        }
        else
        {
            Debug.LogError("Game prefab is not assigned!");
        }
    }

    private void UnloadGame()
    {
        if (gameInstance != null)
        {
            Destroy(gameInstance);
        }
        else
        {
            Debug.LogError("NO game instance to unload!");
        }
    }
}
