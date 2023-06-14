using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwitchIndicator : MonoBehaviour {
  [SerializeField] private Sprite tileSwitchSprite;
  [SerializeField] private Sprite cancelSwitchSprite;
  [SerializeField] private GameObject indicator;
  private SpriteRenderer indicatorSpriteRenderer;
  private Vector3Int draggedTilePosition;
  private static Vector3Int FAKE_TILE_POSITION = new(-999, -999, -999);

  private void Awake() {
    indicatorSpriteRenderer = indicator.GetComponent<SpriteRenderer>();
  }

  private void OnEnable() {
    // indicator = Instantiate(gameObject);
    EventManager.TilemapMouseDown += ShowTileSwitchIndicator;
    EventManager.TilemapMouseUp += HideTileSwitchIndicator;
  }

  private void OnDisable() {
    // Destroy(indicator);
    EventManager.TilemapMouseDown -= ShowTileSwitchIndicator;
    EventManager.TilemapMouseUp -= HideTileSwitchIndicator;
  }

  private void Update() {
    if (!indicator.activeSelf) return;
    PositionTileSwitchIndicator();
  }

  private float GetHalfTileSize() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    float tileSize = TilemapHelper.GetTileSize(levelTilemap);
    Debug.Log(tileSize);
    return tileSize / 2;
  }

  private void PositionTileSwitchIndicator() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3Int hoveredTilePosition = GetTilePositionUnderMouse();
    float halfTileSize = GetHalfTileSize(); // TODO: This should be moved to level load event and stored globally
    Vector3 worldPosition = levelTilemap.GetCellCenterWorld(draggedTilePosition);

    if (hoveredTilePosition == draggedTilePosition || hoveredTilePosition == FAKE_TILE_POSITION) {
      indicatorSpriteRenderer.sprite = cancelSwitchSprite;
      indicator.transform.position = new(worldPosition.x, worldPosition.y, worldPosition.z);
      return;
    }

    indicatorSpriteRenderer.sprite = tileSwitchSprite;

    TilemapHelper.Direction draggedDirection = TilemapHelper.GetDirectionOfTile(draggedTilePosition, hoveredTilePosition);

    switch (draggedDirection) {
      case TilemapHelper.Direction.Up:
        indicator.transform.localRotation = Quaternion.Euler(0, 0, 90);
        indicator.transform.position = new(worldPosition.x, worldPosition.y + halfTileSize, worldPosition.z);
        break;
      case TilemapHelper.Direction.Down:
        indicator.transform.localRotation = Quaternion.Euler(0, 0, 90);
        indicator.transform.position = new(worldPosition.x, worldPosition.y - halfTileSize, worldPosition.z);
        break;
      case TilemapHelper.Direction.Left:
        indicator.transform.localRotation = Quaternion.Euler(0, 0, 0);
        indicator.transform.position = new(worldPosition.x - halfTileSize, worldPosition.y, worldPosition.z);
        break;
      case TilemapHelper.Direction.Right:
        indicator.transform.localRotation = Quaternion.Euler(0, 0, 0);
        indicator.transform.position = new(worldPosition.x + halfTileSize, worldPosition.y, worldPosition.z);
        break;
    }
  }

  private void ShowTileSwitchIndicator() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3Int hoveredTilePosition = GetTilePositionUnderMouse();
    // if (hoveredTilePosition == )
    draggedTilePosition = hoveredTilePosition;
    Vector3 worldPosition = levelTilemap.GetCellCenterWorld(hoveredTilePosition);
    Transform grid = levelTilemap.transform.parent;
    indicator.transform.localScale = grid.localScale;
    indicator.transform.position = worldPosition;
    indicator.SetActive(true);
  }

  private void HideTileSwitchIndicator() {
    indicator.SetActive(false);
  }

  // Copy of function from DragTiles
  private Vector3Int GetTilePositionUnderMouse() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3 mousePositionInWorld = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    if (!levelTilemap.HasTile(levelTilemap.WorldToCell(mousePositionInWorld))) return FAKE_TILE_POSITION;
    return levelTilemap.WorldToCell(mousePositionInWorld);
  }
}
