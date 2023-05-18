using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

  private bool shouldTimerRun = false;
  private float lastEvent;

  private void OnEnable() {
    EventManager.LevelLoaded += StartTimer;
    EventManager.LevelWon += PauseTimer;
    EventManager.LevelLost += PauseTimer;
    // TODO: Add pausing/resuming timer when menu is accessed/closed
  }

  private void OnDisable() {
    EventManager.LevelLoaded -= StartTimer;
    EventManager.LevelWon -= PauseTimer;
    EventManager.LevelLost -= PauseTimer;
  }

  private void Update() {
    if (!shouldTimerRun) return;
    LevelManager levelManager = GameManager.Instance.levelManager;
    if (levelManager.timeRemaining > 0) {
      levelManager.timeRemaining -= Time.deltaTime;
      if (lastEvent - levelManager.timeRemaining >= 1) {
        lastEvent = levelManager.timeRemaining;
        EventManager.OneSecondTick(levelManager.timeRemaining);
      }
    } else {
      levelManager.timeRemaining = 0;
      shouldTimerRun = false;
      if (levelManager.gameLoopRunning) {
        levelManager.levelIsLost = true;
      } else {
        EventManager.LevelLost();
      }
    }
  }

  private void StartTimer(LevelManager levelManager) {
    if (levelManager.timeRemaining == -1) return;
    lastEvent = levelManager.timeRemaining + 1; // +1 to correct timer ticking two seconds on first tick
    shouldTimerRun = true;
  }

  private void PauseTimer() {
    shouldTimerRun = false;
  }

  private void ResumeTimer() {
    shouldTimerRun = true;
  }
}
