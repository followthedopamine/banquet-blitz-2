using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDebugger : MonoBehaviour {
  private void OnMouseDown() {
    PrintClickedTile();

  }

  private void PrintClickedTile() {
    Debug.Log(GetTilePositionUnderMouse());
  }

  private Vector3Int GetTilePositionUnderMouse() {
    Tilemap containerTilemap = GameManager.Instance.levelManager.containerTilemap;
    Vector3 mousePositionInWorld = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    return containerTilemap.WorldToCell(mousePositionInWorld);
  }
}
