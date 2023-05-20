using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedBoulder : Obstacle {
  // TODO: If tile falls really far should be damaged?

  private void Awake() {
    tileId = 101;
    nextDamagedTileId = 102;
  }
}
