using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragTiles : MonoBehaviour {

  private GameTile draggedTile;
  private Vector3Int draggedTilePosition;

  private void Start() {
    EventManager.TilemapMouseDown += PrintClickedTile;
    EventManager.TilemapMouseDown += UpdateDraggedTile;
    EventManager.TilemapMouseUp += SwitchTiles;
  }

  private void PrintClickedTile() {
    Debug.Log(GetTilePositionUnderMouse());
  }

  private Vector3Int GetTilePositionUnderMouse() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3 mousePositionInWorld = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    return levelTilemap.WorldToCell(mousePositionInWorld);
  }

  private GameTile GetTileUnderMouse() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    return levelTilemap.GetTile<GameTile>(GetTilePositionUnderMouse());
  }

  private void UpdateDraggedTile() {
    draggedTile = GetTileUnderMouse();
    draggedTilePosition = GetTilePositionUnderMouse();
  }

  private void SwitchTiles() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    GameTile targetTile = GetTileUnderMouse();
    Vector3Int targetTilePosition = GetTilePositionUnderMouse();
    levelTilemap.SetTile(targetTilePosition, draggedTile);
    levelTilemap.SetTile(draggedTilePosition, targetTile);
  }
}
