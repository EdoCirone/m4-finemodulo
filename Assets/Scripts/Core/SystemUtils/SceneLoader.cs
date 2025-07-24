using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(ResetSceneCoroutine());
    }

    private IEnumerator ResetSceneCoroutine()
    {
        Scene current = SceneManager.GetActiveScene();
        yield return SceneManager.LoadSceneAsync(current.buildIndex);

        // Aspetta un frame per essere sicuro che tutto sia stato instanziato
        yield return null;

        UIManager.Instance?.RebindUI();
        UIManager.Instance?.ResetUI();

        // Reimposta player nella camera
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            FindObjectOfType<CameraManager>()?.SetPlayer(playerGO.transform);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCreditsMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            LoadMainMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Ho chiuso il gioco.");  // Debug per l’editor
        Application.Quit();                // Funziona in build
    }

    public void LoadLevelByIndex(int index)
    {
        Time.timeScale = 1f;

        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogWarning($"Indice scena {index} non valido!");
        }
    }
}
