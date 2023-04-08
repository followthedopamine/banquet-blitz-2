using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragTiles : MonoBehaviour {

  private void Start() {
    EventManager.TilemapClicked += PrintClickedTile;
  }

  private void PrintClickedTile() {
    Debug.Log(GetTilePositionUnderMouse());
  }

  private Vector3 GetTilePositionUnderMouse() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3 mousePositionInWorld = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    return levelTilemap.WorldToCell(mousePositionInWorld);
  }


}
