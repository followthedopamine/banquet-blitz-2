using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

  [HideInInspector] public Camera cam;
  [HideInInspector] public LevelManager levelManager;

  private void Start() {
    cam = Camera.main;
    EventManager.LevelLoaded += GetCurrentLevelManager;
  }

  private void GetCurrentLevelManager(LevelManager level) {
    levelManager = level;
  }
}
