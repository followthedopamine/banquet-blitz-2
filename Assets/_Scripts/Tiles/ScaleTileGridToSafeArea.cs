using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ScaleTileGridToSafeArea : MonoBehaviour {
  private GameObject topBar;
  private GameObject bottomBar;

  private const float VERTICAL_PADDING = 15.0f;
  private const float HALF_CELL_SIZE = 0.5f;

  private Tilemap levelTilemap;

  // Goals of this script:
  // Calculate true size of combined tiles in tilemap
  // Scale tilemap to fill the safe area as best as possible

  private void OnEnable() {
    EventManager.LevelLoaded += ScaleGrid;
  }

  private void OnDisable() {
    EventManager.LevelLoaded -= ScaleGrid;
  }

  private void ScaleGrid(LevelManager levelManager) {
    topBar = GameObject.Find("Top Bar");
    bottomBar = GameObject.Find("Lives Container");
    levelTilemap = levelManager.levelTilemap;
    Vector2 safeArea = CalculateSafeArea();
    Vector2 tilemapDimensions = GetTrueDimensionsOfTilemap();
    float scalingFactor = GetScalingFactor(safeArea, tilemapDimensions);
    Debug.Log(scalingFactor);
    levelTilemap.layoutGrid.gameObject.transform.localScale = new Vector3(scalingFactor, scalingFactor, levelTilemap.layoutGrid.transform.localScale.z);
  }

  private Vector2 CalculateSafeArea() {
    RectTransform topBarRect = topBar.GetComponent<RectTransform>();
    RectTransform bottomBarRect = bottomBar.GetComponent<RectTransform>();

    // Corners = bottom left, top left, top right, bottom right
    Vector3[] topCorners = new Vector3[4];
    topBarRect.GetWorldCorners(topCorners);

    Vector3[] bottomCorners = new Vector3[4];
    bottomBarRect.GetWorldCorners(bottomCorners);

    // for (var i = 0; i < 4; i++) {
    //   Debug.Log("World Corner " + i + " : " + topCorners[i]);
    // }

    float leftBoundary = topCorners[0].x; // Bottom left
    float rightBoundary = topCorners[2].x; // Top right
    float topBoundary = topCorners[0].y - VERTICAL_PADDING; // Bottom left
    float bottomBoundary = bottomCorners[2].y + VERTICAL_PADDING; // Top right

    float safeHeight = topBoundary - bottomBoundary;
    float safeWidth = rightBoundary - leftBoundary;

    Debug.Log(safeWidth + ", " + safeHeight);
    return new Vector2(safeWidth, safeHeight);
  }

  private Vector2 GetTrueDimensionsOfTilemap() {
    Camera cam = GameManager.Instance.cam;

    List<Vector3Int> tilePositions = TilemapHelper.GetTilePositions(levelTilemap);

    float maxYWorld = 0;
    float maxXWorld = 0;
    float minYWorld = float.MaxValue;
    float minXWorld = float.MaxValue;

    foreach (Vector3Int tilePosition in tilePositions) {
      Vector3 worldPosition = levelTilemap.GetCellCenterWorld(tilePosition);
      Vector3 maxScreenPosition = cam.WorldToScreenPoint(new Vector3(worldPosition.x + HALF_CELL_SIZE, worldPosition.y + HALF_CELL_SIZE, worldPosition.z));
      Vector3 minScreenPosition = cam.WorldToScreenPoint(new Vector3(worldPosition.x - HALF_CELL_SIZE, worldPosition.y - HALF_CELL_SIZE, worldPosition.z));
      maxXWorld = Mathf.Max(maxXWorld, maxScreenPosition.x);
      maxYWorld = Mathf.Max(maxYWorld, maxScreenPosition.y);
      minXWorld = Mathf.Min(minXWorld, minScreenPosition.x);
      minYWorld = Mathf.Min(minYWorld, minScreenPosition.y);
    }

    Debug.Log(maxXWorld + ", " + maxYWorld + ", " + minXWorld + ", " + minYWorld);

    float trueHeight = maxYWorld - minYWorld;
    float trueWidth = maxXWorld - minXWorld;

    Debug.Log(trueWidth + ", " + trueHeight);

    return new Vector2(trueWidth, trueHeight);
  }

  private float GetScalingFactor(Vector2 safeArea, Vector2 tilemap) {
    return Mathf.Min(safeArea.x / tilemap.x, safeArea.y / tilemap.y);
  }
}
