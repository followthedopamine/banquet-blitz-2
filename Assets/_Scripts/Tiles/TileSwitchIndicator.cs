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

  private void PositionTileSwitchIndicator() {
    Vector3Int? hoveredTilePosition = GetTilePositionUnderMouse();
    if (hoveredTilePosition == draggedTilePosition) indicatorSpriteRenderer.sprite = cancelSwitchSprite;
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
    return levelTilemap.WorldToCell(mousePositionInWorld);
  }
}
