using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
  private void Start() {
    EventManager.DestroyedTiles += IncreaseScoreForDestroyedTiles;
  }

  private void IncreaseScoreForDestroyedTiles(List<Vector3Int> DestroyedTiles) {
    GameManager.Instance.levelManager.score += DestroyedTiles.Count * 100;
    EventManager.ScoreUpdated();
  }
}
