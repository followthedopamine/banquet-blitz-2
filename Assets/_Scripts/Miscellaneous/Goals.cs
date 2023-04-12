using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Goals : MonoBehaviour {
  public enum Type {
    Score,
    Count
  }

  private void Start() {
    EventManager.MatchesFound += TrackGoalCompletion;
  }

  private void TrackGoalCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;
    if (levelManager.goalRemaining.Count - levelManager.goalTiles.Count == 1) {
      // If goal required list is 1 larger than goal tiles list then the last element in the list is the score goal
      TrackScoreCompletion(matches);
      if (levelManager.goalTiles.Count == 0) return; // Only track score if there are no goal tiles
    }
    TrackCountCompletion(matches);
  }

  private void TrackScoreCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;
    // ^1 is shorthand for last element in List
    if (levelManager.goalRemaining[^1] == 0) return;
    int tileCount = 0;
    foreach (Match match in matches) {
      tileCount += match.tilePositions.Count;
    }
    levelManager.goalRemaining[^1] -= tileCount * Score.SCORE_MULTIPLIER; // TODO: Consider attaching this to actual score
    if (levelManager.goalRemaining[^1] < 0) levelManager.goalRemaining[^1] = 0;
  }

  private void TrackCountCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;

    for (int i = 0; i < levelManager.goalTiles.Count; i++) {
      if (levelManager.goalRemaining[i] == 0) return;
      foreach (Match match in matches) {
        if (match.tileId == levelManager.goalTiles[i].id) {
          levelManager.goalRemaining[i] -= match.tilePositions.Count;
          if (levelManager.goalRemaining[i] < 0) {
            levelManager.goalRemaining[i] = 0;
          }
        }
      }
    }
  }
}
