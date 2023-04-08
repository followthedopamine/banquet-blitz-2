using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapClicks : MonoBehaviour {

  private void OnMouseDown() {
    Vector3 mousePositionInWorld = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    EventManager.TilemapClicked(mousePositionInWorld);
  }
}
