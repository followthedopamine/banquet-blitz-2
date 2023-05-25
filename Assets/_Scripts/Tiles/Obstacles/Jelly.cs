using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Obstacle {

  private void Awake() {
    tileId = 103;
    isDestroyedByUnderTileMatch = true;
    freezesUnderTile = true;
  }
}
