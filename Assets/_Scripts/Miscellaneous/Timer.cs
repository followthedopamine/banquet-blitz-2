using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

  //TODO: Might wanna move timer into this script, not sure if there's much overhead fetching from singleton every frame

  private bool shouldTimerRun = false;
  private float lastEvent;

  private void Start() {
    EventManager.LevelLoaded += StartTimer;
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
      // TODO: Goal failed
      EventManager.LevelLost();
    }
  }

  private void StartTimer(LevelManager levelManager) {
    if (levelManager.timeRemaining == -1) return;
    lastEvent = levelManager.timeRemaining + 1; // +1 to correct timer ticking two seconds on first tick
    shouldTimerRun = true;
  }


}
