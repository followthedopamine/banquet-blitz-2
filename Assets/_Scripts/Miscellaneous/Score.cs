using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
  public const int SCORE_MULTIPLIER = 100;

  private void Start() {
    EventManager.DestroyedTiles += IncreaseScoreForDestroyedTiles;
  }

  private void IncreaseScoreForDestroyedTiles(List<Vector3Int> DestroyedTiles) {
    GameManager.Instance.levelManager.score += DestroyedTiles.Count * SCORE_MULTIPLIER;
    EventManager.ScoreUpdated();
  }
}
