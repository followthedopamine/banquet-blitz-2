using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
  public const int SCORE_MULTIPLIER = 10;

  private void OnEnable() {
    EventManager.DestroyedTiles += IncreaseScoreForDestroyedTiles;
  }

  private void OnDisable() {
    EventManager.DestroyedTiles -= IncreaseScoreForDestroyedTiles;
  }

  private void IncreaseScoreForDestroyedTiles(List<Vector3Int> destroyedTiles) {
    Debug.Log("Score increased " + destroyedTiles.Count);
    GameManager.Instance.levelManager.score += destroyedTiles.Count * SCORE_MULTIPLIER;
    EventManager.ScoreUpdated();
  }

}
