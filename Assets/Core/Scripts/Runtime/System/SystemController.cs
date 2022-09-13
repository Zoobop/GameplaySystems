using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UI;

public class SystemController : MonoBehaviour
{
    public static SystemController Instance { get; private set; }

    private const int ManagersScene = 1;
    private const int UIScene = 2;
    private const int MainMenuScene = 3;
    private const int DemoScene = 4;
    private const int RenderScene = 5;
    private const int DialogueScene = 6;
    
    #region UnityEvents

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    #endregion

    #region Setup

    private static IEnumerator Initialize()
    {
        // Managers Scene (must load first)
        yield return SceneManager.LoadSceneAsync(ManagersScene, LoadSceneMode.Additive);
        // Rendering Scene (must load first)
        yield return SceneManager.LoadSceneAsync(RenderScene, LoadSceneMode.Additive);
        // UI Scene
        var uiScene = SceneManager.LoadSceneAsync(UIScene, LoadSceneMode.Additive);
        // Main Menu
        var mainMenuScene = SceneManager.LoadSceneAsync(MainMenuScene, LoadSceneMode.Additive);

        // Await scenes completed
        while (!uiScene.isDone || !mainMenuScene.isDone)
        {
            yield return null;
        }
        
        // Set active scene to be main menu scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(MainMenuScene));
    }
    
    private static IEnumerator LoadCoreGameScenes()
    {
        // Transition in
        yield return TransitionController.StartTransition();
        
        // Enable UI
        UIController.EnableUI();
        UIController.ShowHUD();

        // Demo Scene
        var demoScene = SceneManager.LoadSceneAsync(DemoScene, LoadSceneMode.Additive);
        // Dialogue Handling Scene
        var dialogueScene = SceneManager.LoadSceneAsync(DialogueScene, LoadSceneMode.Additive);

        // Await scenes completed
        while (!demoScene.isDone || !dialogueScene.isDone)
        {
            yield return null;
        }
        
        // Set active scene to be demo scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(DemoScene));

        // Unload main menu scene
        var mainMenuScene = SceneManager.UnloadSceneAsync(MainMenuScene);
        while (!mainMenuScene.isDone)
        {
            yield return null;
        }
        
        // Transition out
        yield return TransitionController.StopTransition();
    }

    #endregion
    
    #region Load/Unload Helpers

    private static IEnumerator LoadSceneAsync(int sceneIndex, LoadSceneMode loadSceneMode, bool unloadActiveScene = false, bool transition = false)
    {
        // Transition in
        if (transition)
            yield return TransitionController.StartTransition();
        
        // Set the current Scene to be able to unload it later
        var currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        var asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // Unload the previous Scene
        if (unloadActiveScene)
        {
            var asyncUnload = SceneManager.UnloadSceneAsync(currentScene);
            
            // Wait until the last operation fully loads to return anything
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }
        
        // Transition out
        if (transition)
            yield return TransitionController.StopTransition();
    }
    
    private static IEnumerator UnloadSceneAsync(int sceneIndex, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None)
    {
        // The Application unloads the Scene in the background at the same time as the current Scene.
        var asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex, unloadSceneOptions);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    #endregion

    #region Utility
    
    public static void LoadScene(int sceneIndex, LoadSceneMode loadSceneMode, bool unloadActiveScene = false, bool transition = false)
    {
        Instance.StartCoroutine(LoadSceneAsync(sceneIndex, loadSceneMode, unloadActiveScene, transition));
    }

    public static void UnloadScene(int sceneIndex, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None)
    {
        Instance.StartCoroutine(UnloadSceneAsync(sceneIndex, unloadSceneOptions));
    }

    public static void LoadScenesOnPlay()
    {
        Instance.StartCoroutine(LoadCoreGameScenes());
    }

    #endregion
}
