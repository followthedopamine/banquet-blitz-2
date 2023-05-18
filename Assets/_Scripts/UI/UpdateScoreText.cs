using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreText : MonoBehaviour {
  private TMP_Text scoreText;
  private void OnEnable() {
    scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    EventManager.ScoreUpdated += MatchScoreTextToScore;
  }

  private void OnDisable() {
    EventManager.ScoreUpdated -= MatchScoreTextToScore;
  }

  private void MatchScoreTextToScore() {
    scoreText.text = GameManager.Instance.levelManager.score.ToString();
  }
}
