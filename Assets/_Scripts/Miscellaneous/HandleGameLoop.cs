using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameLoop : MonoBehaviour {
  private void OnEnable() {
    EventManager.GameLoopStarted += GameLoopIsRunning;
    EventManager.GameLoopFinished += GameLoopIsNotRunning;
    EventManager.GameLoopFinished += CheckIfGameIsWonOrLost;
  }

  private void OnDisable() {
    EventManager.GameLoopStarted -= GameLoopIsRunning;
    EventManager.GameLoopFinished -= GameLoopIsNotRunning;
    EventManager.GameLoopFinished -= CheckIfGameIsWonOrLost;
  }

  private void GameLoopIsRunning() {
    GameManager.Instance.levelManager.gameLoopRunning = true;
  }

  private void GameLoopIsNotRunning() {
    GameManager.Instance.levelManager.gameLoopRunning = false;
  }

  private void CheckIfGameIsWonOrLost() {
    if (GameManager.Instance.levelManager.levelIsWon) {
      EventManager.LevelWon();
    } else if (GameManager.Instance.levelManager.levelIsLost) {
      EventManager.LevelLost();
    }
  }
}
