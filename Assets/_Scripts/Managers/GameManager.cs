using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

  [HideInInspector] public Camera cam;
  [HideInInspector] public LevelManager levelManager;
  public const int MAX_LIVES = 5;
  [HideInInspector] public int lives = MAX_LIVES;

  private void Start() {
    cam = Camera.main;
    EventManager.LevelLoaded += SetCurrentLevelManager;
  }

  private void SetCurrentLevelManager(LevelManager level) {
    levelManager = level;
  }
}
