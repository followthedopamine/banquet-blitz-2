using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
  private const string GAME_SCENE = "Game";
  private const string UI_SCENE = "UI";
  private const string CORE_SCENE = "Core";
  private const string LEVEL_SELECT_SCENE = "Level Select";
  private readonly List<string> NON_LEVEL_SCENES = new() { GAME_SCENE, UI_SCENE, CORE_SCENE, LEVEL_SELECT_SCENE };
  private readonly List<string> LEVEL_SCENES = new() { "0001 Level", "0002 Level 2" };

  private void OnEnable() {
    // TODO: For now just load the level select as there is no main menu yet
    // LoadLevelByIndex(0);
    // LoadLevelSelect();
    EventManager.RetryButton += ReloadCurrentLevel;
    SceneManager.sceneLoaded += OnSceneLoad;
    SceneManager.sceneUnloaded += OnSceneUnload;
  }

  private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
    Debug.Log("Scene " + scene.name + " loaded");
  }

  private void OnSceneUnload(Scene scene) {
    Debug.Log("Scene " + scene.name + " unloaded");
  }

  private void LoadLevelSelect() {
    UnloadAllLevelScenes();
    UnloadPlayScenes();
    List<string> loadedScenes = GetAllLoadedScenes();

    if (!loadedScenes.Contains(LEVEL_SELECT_SCENE)) SceneManager.LoadSceneAsync(LEVEL_SELECT_SCENE, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(GAME_SCENE, LoadSceneMode.Additive);
  }

  private void LoadPlayScenes() {
    List<string> loadedScenes = GetAllLoadedScenes();
    if (!loadedScenes.Contains(GAME_SCENE)) SceneManager.LoadSceneAsync(GAME_SCENE, LoadSceneMode.Additive);
    if (!loadedScenes.Contains(UI_SCENE)) SceneManager.LoadSceneAsync(UI_SCENE, LoadSceneMode.Additive);
  }

  private void UnloadPlayScenes() {
    List<string> loadedScenes = GetAllLoadedScenes();
    if (loadedScenes.Contains(GAME_SCENE)) SceneManager.UnloadSceneAsync(GAME_SCENE);
    if (loadedScenes.Contains(UI_SCENE)) SceneManager.UnloadSceneAsync(UI_SCENE);
  }

  private void LoadLevelByIndex(int index) {
    UnloadAllLevelScenes();
    UnloadPlayScenes();
    LoadPlayScenes();
    SceneManager.LoadSceneAsync(LEVEL_SCENES[index], LoadSceneMode.Additive);
  }

  private void UnloadAllLevelScenes() {
    List<string> loadedScenes = GetAllLoadedScenes();
    foreach (string sceneName in loadedScenes) {
      if (!NON_LEVEL_SCENES.Contains(sceneName)) SceneManager.UnloadSceneAsync(sceneName);
    }
  }

  private List<string> GetAllLoadedScenes() {
    int countLoaded = SceneManager.sceneCount;
    List<string> loadedScenes = new();

    for (int i = 0; i < countLoaded; i++) {
      if (SceneManager.GetSceneAt(i).isLoaded) {
        loadedScenes.Add(SceneManager.GetSceneAt(i).name);
      }
    }
    return loadedScenes;
  }

  private void ReloadCurrentLevel() {
    int currentLevelIndex = GetCurrentLevelIndex();
    LoadLevelByIndex(currentLevelIndex);
    // UnloadPlayScenes();
    // UnloadAllLevelScenes();
    // LoadPlayScenes();
  }

  private int GetCurrentLevelIndex() {
    List<string> loadedScenes = GetAllLoadedScenes();
    for (int i = 0; i < loadedScenes.Count; i++) {
      string scene = loadedScenes[i];
      if (LEVEL_SCENES.Contains(scene)) return LEVEL_SCENES.IndexOf(scene);
    }
    return 0;
  }
}
