using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapClicks : MonoBehaviour {

  private void OnMouseDown() {
    EventManager.TilemapClicked();
  }
}
