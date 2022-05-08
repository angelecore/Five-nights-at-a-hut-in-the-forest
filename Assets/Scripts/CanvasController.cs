using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField]
    private RectTransform pausedMenu;

    [SerializeField]
    private RectTransform gameOverMenu;

    void Start()
    {
        Hide(pausedMenu);
        Hide(gameOverMenu);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ShowGameOverMenu()
    {
        Hide(pausedMenu);
        Show(gameOverMenu);
    }

    public void ResumeGame()
    {
        Hide(pausedMenu);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        Scenes.RestartScene();
    }

    public void ExitGame()
    {
        Scenes.LoadPreviousScene();
    }

    private void TogglePauseGame()
    {
        if (IsGameOver())
        {
            return;
        }
        if (IsGamePaused())
        {
            ResumeGame();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            PauseGame();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    private void PauseGame()
    {
        Show(pausedMenu);
        Time.timeScale = 0;
    }
    private bool IsGameOver()
    {
        return gameOverMenu.gameObject.activeInHierarchy;
    }
    private bool IsGamePaused()
    {
        return pausedMenu.gameObject.activeInHierarchy;
    }
    private static void Show(Component component)
    {
        component.gameObject.SetActive(true);
    }
    private static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }
}
