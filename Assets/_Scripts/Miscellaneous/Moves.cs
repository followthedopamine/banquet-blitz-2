using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {
  private void Start() {
    EventManager.TilesSwitched += SubtractMove;
  }

  private void SubtractMove() {
    GameManager.Instance.levelManager.movesRemaining--;
    EventManager.MovesUpdated();
    if (GameManager.Instance.levelManager.movesRemaining == 0) {
      EventManager.LevelLost();
    }
  }
}
