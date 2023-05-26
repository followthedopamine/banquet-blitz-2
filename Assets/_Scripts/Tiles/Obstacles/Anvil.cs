using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : Obstacle {
  private void Awake() {
    tileId = 104;
    destroyTileInBottomRow = true;
  }
}
