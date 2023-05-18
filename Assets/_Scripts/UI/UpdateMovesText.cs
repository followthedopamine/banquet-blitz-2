using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMovesText : MonoBehaviour {
  private TMP_Text movesText;

  private void OnEnable() {
    movesText = GameObject.Find("Moves Text").GetComponent<TMP_Text>();
    EventManager.MovesUpdated += MatchMovesTextToLevelMoves;
    EventManager.LevelLoaded += MatchMovesOnLevelLoad; // UI must be loaded first for this to fire
  }

  private void OnDisable() {
    EventManager.MovesUpdated -= MatchMovesTextToLevelMoves;
    EventManager.LevelLoaded -= MatchMovesOnLevelLoad; // 
  }

  private void MatchMovesOnLevelLoad(LevelManager levelManager) {
    movesText.text = levelManager.movesRemaining.ToString();
  }

  private void MatchMovesTextToLevelMoves() {
    movesText.text = GameManager.Instance.levelManager.movesRemaining.ToString();
  }
}
