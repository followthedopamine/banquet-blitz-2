using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
  private const string GAME_SCENE = "Game";
  private const string UI_SCENE = "UI";
  private const string CORE_SCENE = "Core";
  private readonly List<string> NON_LEVEL_SCENES = new() { GAME_SCENE, UI_SCENE, CORE_SCENE };
  private readonly List<string> LEVEL_SCENES = new() { "0001 Level", "0002 Level 2" };

  private void Start() {
    // TODO: For now just load the first level as there is no main menu yet
    LoadLevelByIndex(0);
  }

  private void LoadPlayScenes() {
    List<string> loadedScenes = GetAllLoadedScenes();
    if (!loadedScenes.Contains(GAME_SCENE)) SceneManager.LoadScene(GAME_SCENE, LoadSceneMode.Additive);
    if (!loadedScenes.Contains(UI_SCENE)) SceneManager.LoadScene(UI_SCENE, LoadSceneMode.Additive);
  }

  private void LoadLevelByIndex(int index) {
    UnloadAllLevelScenes();
    LoadPlayScenes();
    SceneManager.LoadScene(LEVEL_SCENES[index], LoadSceneMode.Additive);
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
      loadedScenes.Add(SceneManager.GetSceneAt(i).name);
    }
    return loadedScenes;
  }
}
