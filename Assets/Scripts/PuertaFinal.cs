using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaFinal : MonoBehaviour
{
    [SerializeField] float loadDelay = 0.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(finalDelay());
        }
    }

    IEnumerator finalDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("PantallaFinal");
    }
}
