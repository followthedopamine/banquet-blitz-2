using System;
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

  private Vector3Int GetNearestTileInDraggedDirection() {
    Vector3Int targetTilePosition = GetTilePositionUnderMouse();
    if (draggedTilePosition == targetTilePosition) return draggedTilePosition;
    Vector3Int draggingDirection = targetTilePosition - draggedTilePosition;
    Vector3Int absDraggingDirection = new(Math.Abs(draggingDirection.x), Math.Abs(draggingDirection.y), 0);
    if (absDraggingDirection.x > absDraggingDirection.y) {
      if (draggedTilePosition.x > targetTilePosition.x) {
        return new Vector3Int(draggedTilePosition.x - 1, draggedTilePosition.y, 0);
      } else {
        return new Vector3Int(draggedTilePosition.x + 1, draggedTilePosition.y, 0);
      }
    } else {
      if (draggedTilePosition.y > targetTilePosition.y) {
        return new Vector3Int(draggedTilePosition.x, draggedTilePosition.y - 1, 0);
      } else {
        return new Vector3Int(draggedTilePosition.x, draggedTilePosition.y + 1, 0);
      }
    }
  }

  private Boolean IsPositionInBounds(Vector3Int position) {
    Tilemap containerTilemap = GameManager.Instance.levelManager.containerTilemap;
    return containerTilemap.GetTile(position) != null;
  }

  private void SwitchTiles() {
    if (GameManager.Instance.levelManager.gameLoopRunning) return;
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3Int targetTilePosition = GetNearestTileInDraggedDirection();
    GameTile targetTile = levelTilemap.GetTile<GameTile>(targetTilePosition);
    if (targetTilePosition == draggedTilePosition) return;
    if (!IsPositionInBounds(targetTilePosition)) return;
    levelTilemap.SetTile(targetTilePosition, draggedTile);
    levelTilemap.SetTile(draggedTilePosition, targetTile);
    EventManager.TilesSwitched();
  }
}
