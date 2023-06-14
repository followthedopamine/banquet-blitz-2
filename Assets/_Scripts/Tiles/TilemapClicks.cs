using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapClicks : MonoBehaviour {

  private void OnMouseDown() {
    EventManager.TilemapMouseDown();
  }

  private void OnMouseUp() {
    EventManager.TilemapMouseUp();
  }

  // private void Update() {
  //   if (Input.GetMouseButtonDown(0)) {
  //     EventManager.TilemapMouseDown();
  //   }
  //   if (Input.GetMouseButtonUp(0)) {
  //     EventManager.TilemapMouseUp();
  //   }
  // }
}
