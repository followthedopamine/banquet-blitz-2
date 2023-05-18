using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionTrophyBarSprites : MonoBehaviour {
  private const float HORIZONTAL_PADDING = 1.2f;

  private void OnEnable() {
    EventManager.LevelLoaded += PositionSprites;
  }

  private void OnDisable() {
    EventManager.LevelLoaded -= PositionSprites;
  }

  private void PositionSprites(LevelManager levelManager) {
    // Don't forget to set x pivot to 0 of parent
    GameObject trophyBar = GameObject.Find("Trophy Bar");
    GameObject emptyTrophy = GameObject.Find("Empty Trophy");
    float trophyBarWidth = trophyBar.GetComponent<RectTransform>().sizeDelta.x - HORIZONTAL_PADDING;
    int goldTrophyScore = levelManager.trophyScores[^1];
    foreach (int trophyScore in levelManager.trophyScores) {
      GameObject trophySprite = GameObject.Instantiate(emptyTrophy, trophyBar.transform);
      float xPosition = trophyBarWidth / goldTrophyScore * trophyScore;
      trophySprite.transform.localPosition = new Vector3(xPosition, trophySprite.transform.localPosition.y, trophySprite.transform.localPosition.z);
    }
    Destroy(emptyTrophy);
  }
}
