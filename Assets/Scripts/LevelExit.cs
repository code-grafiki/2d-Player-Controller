using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LoadLevelDelay = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel()); 
        } 
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LoadLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScreenPersist>().ResetScreenPersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
