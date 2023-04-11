using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMoves : MonoBehaviour {
  private void Start() {
    EventManager.TilesSwitched += SubtractMove;
  }

  private void SubtractMove() {
    GameManager.Instance.levelManager.movesRemaining--;
    EventManager.MovesUpdated();
  }
}
