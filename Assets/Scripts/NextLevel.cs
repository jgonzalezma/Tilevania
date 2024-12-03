using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 0.5f;
    [SerializeField] float[] levelTimes; // Tiempos específicos para cada nivel

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(nextLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // Configura el tiempo inicial del siguiente nivel en GameSession
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null && nextSceneIndex < levelTimes.Length)
        {
            gameSession.SetStartTime(levelTimes[nextSceneIndex]);
        }

        FindObjectOfType<ScenePersistence>().DeleteScenePersistance();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
