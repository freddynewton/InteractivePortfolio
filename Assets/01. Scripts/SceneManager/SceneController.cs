using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene loading and unloading, including transitions between scenes.
/// </summary>
public class SceneController : Singleton<SceneController>
{
    [SerializeField] private TransitionManager transitionManager;

    /// <summary>
    /// Loads a scene additively and asynchronously with a transition effect.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public async Task LoadSceneAdditiveAsync(SceneName sceneName)
    {
        // Show transition before loading the new scene
        await transitionManager.ShowTransition();

        // Load the specified scene additively (keeps the current scene loaded)
        await SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Additive);

        // Hide transition after the scene is loaded
        await transitionManager.HideTransition();
    }

    /// <summary>
    /// Unloads a scene asynchronously with a transition effect.
    /// </summary>
    /// <param name="sceneName">The name of the scene to unload.</param>
    public async Task UnloadSceneAsync(SceneName sceneName)
    {
        // Show transition before unloading the scene
        await transitionManager.ShowTransition();

        // Unload the specified scene
        await SceneManager.UnloadSceneAsync((int)sceneName);

        // Hide transition after the scene is unloaded
        await transitionManager.HideTransition();
    }

    /// <summary>
    /// Switches from one scene to another asynchronously with transition effects.
    /// </summary>
    /// <param name="sceneToUnload">The name of the scene to unload.</param>
    /// <param name="sceneToLoad">The name of the scene to load.</param>
    public async Task SwitchSceneAsync(SceneName sceneToUnload, SceneName sceneToLoad)
    {
        // Show transition before switching scenes
        await transitionManager.ShowTransition();

        // Unload the specified scene
        await SceneManager.UnloadSceneAsync((int)sceneToUnload);

        // Load the specified scene additively
        await SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive);

        // Hide transition after the new scene is loaded
        await transitionManager.HideTransition();
    }

    /// <summary>
    /// Initializes the SceneController and loads the main menu scene additively at startup.
    /// </summary>
    public override async void Awake()
    {
        // Ensure the base Awake logic is called for Singleton behavior
        base.Awake();

        // Find the TransitionManager in the scene if not assigned
        transitionManager ??= FindFirstObjectByType<TransitionManager>();

        // Load the main menu scene additively on application start
        await SceneManager.LoadSceneAsync((int)SceneName.MainMenu, LoadSceneMode.Additive);

        // Hide the transition after the main menu scene is loaded
        await transitionManager.HideTransition();
    }
}
