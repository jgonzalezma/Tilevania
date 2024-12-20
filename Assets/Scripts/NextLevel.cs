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

        FindObjectOfType<ScenePersistence>().DeleteScenePersistance();
        SceneManager.LoadScene(nextSceneIndex);

        // Ajusta el Timer en el nuevo nivel
        yield return null; // Espera un frame para que la nueva escena cargue
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.InitializeTimer(); // Reinicia el Timer
        }
    }
}
