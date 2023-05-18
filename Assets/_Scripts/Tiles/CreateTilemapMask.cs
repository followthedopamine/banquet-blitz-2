using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateTilemapMask : MonoBehaviour {

  private void OnEnable() {
    EventManager.LevelLoaded += CreateMask;
  }

  private void OnDisable() {
    EventManager.LevelLoaded -= CreateMask;
  }

  private void CreateMask(LevelManager levelManager) {
    GameObject containerTilemapObject = levelManager.containerTilemap.gameObject;
    GameObject container = new();
    container.name = "Tilemap Mask Container";
    container.transform.SetParent(containerTilemapObject.transform);
    Tilemap containerTilemap = levelManager.containerTilemap;
    List<Vector3Int> containerTiles = levelManager.containerTilePositions;
    foreach (Vector3Int tilePosition in containerTiles) {
      GameObject copiedTile = TilemapHelper.CopyTileAsSpriteMask(containerTilemap, tilePosition);
      copiedTile.name = "Copied Tile Mask";
      copiedTile.transform.SetParent(container.transform);
    }
  }
}
