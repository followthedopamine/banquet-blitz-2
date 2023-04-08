using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragTiles : MonoBehaviour {

  private void Start() {
    EventManager.TilemapClicked += PrintClickedTile;
  }

  private void PrintClickedTile(Vector3 mousePosition) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Debug.Log(levelTilemap.WorldToCell(mousePosition));
  }


}
