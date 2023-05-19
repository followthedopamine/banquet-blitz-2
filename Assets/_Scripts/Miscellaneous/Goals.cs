using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Goals : MonoBehaviour {

  private void OnEnable() {
    EventManager.MatchesFound += TrackGoalCompletion;
  }

  private void OnDisable() {
    EventManager.MatchesFound -= TrackGoalCompletion;
  }

  private void TrackGoalCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;
    bool scoreCompleted = true;
    bool countCompleted = true;
    if (levelManager.goalRemaining.Count - levelManager.goalTiles.Count == 1) {
      // If goal required list is 1 larger than goal tiles list then the last element in the list is the score goal
      scoreCompleted = TrackScoreCompletion(matches);
      if (levelManager.goalTiles.Count == 0) return; // Only track score if there are no goal tiles
    }
    countCompleted = TrackCountCompletion(matches);
    EventManager.GoalUpdated(levelManager);
    if (scoreCompleted && countCompleted) {
      levelManager.levelIsWon = true;
      Debug.Log("Level is won, waiting for game loop to finish");
      Debug.Log("Goal completion: ");
      foreach (int goal in levelManager.goalRemaining) {
        Debug.Log(goal);
      }
    }
  }

  // Returns true if score goal has been completed
  private bool TrackScoreCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;
    // ^1 is shorthand for last element in List
    if (levelManager.goalRemaining[^1] == 0) return true;
    int tileCount = 0;
    foreach (Match match in matches) {
      tileCount += match.tilePositions.Count;
    }
    levelManager.goalRemaining[^1] -= tileCount * Score.SCORE_MULTIPLIER; // TODO: Consider attaching this to actual score
    if (levelManager.goalRemaining[^1] < 0) {
      levelManager.goalRemaining[^1] = 0;
      return true;
    }
    return false;
  }

  private bool TrackCountCompletion(List<Match> matches) {
    LevelManager levelManager = GameManager.Instance.levelManager;

    for (int i = 0; i < levelManager.goalTiles.Count; i++) {
      int goalTileId = levelManager.goalTiles[i].id;
      if (levelManager.goalRemaining[i] == 0) continue;
      foreach (Match match in matches) {
        if (match.tileId == goalTileId) {
          levelManager.goalRemaining[i] -= match.tilePositions.Count;
          if (levelManager.goalRemaining[i] <= 0) {
            levelManager.goalRemaining[i] = 0;
          }
        }
      }
    }
    bool countCompleted = true;
    foreach (int goal in levelManager.goalRemaining) {
      if (goal != 0) countCompleted = false;
    }
    return countCompleted;
  }
}
