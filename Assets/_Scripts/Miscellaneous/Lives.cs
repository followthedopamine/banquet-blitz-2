using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour {
  private void Start() {
    EventManager.LevelLost += SubtractLife;
    InvokeRepeating("AddLife", 120, 120);
  }

  private void SubtractLife() {
    GameManager.Instance.lives--;
  }

  // TODO: Change this to work off time since last life added so that players can gain lives with the game closed
  private void AddLife() {
    if (GameManager.Instance.lives < GameManager.MAX_LIVES) {
      GameManager.Instance.lives++;
    }
  }
}
