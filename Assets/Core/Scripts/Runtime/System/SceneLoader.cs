using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _sceneIndex;

    public void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    
    private IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        var currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        var asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
