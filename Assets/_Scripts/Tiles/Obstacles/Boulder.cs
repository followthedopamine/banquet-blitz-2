using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Obstacle {
  // TODO: If tile falls really far should be damaged?

  private void Awake() {
    tileId = 100;
    nextDamagedTileId = 101;
  }
}
