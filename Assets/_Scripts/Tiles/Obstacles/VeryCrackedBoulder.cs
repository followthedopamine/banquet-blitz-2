using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeryCrackedBoulder : Obstacle {
  // TODO: If tile falls really far should be damaged?

  private void Awake() {
    tileId = 102;
    nextDamagedTileId = -1;
  }
}
