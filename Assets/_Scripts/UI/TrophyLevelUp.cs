using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyLevelUp : MonoBehaviour {
  // Display firework animation on reaching trophy score
  // Change sprite in trophy bar

  private int previousScore = 0;
  private int currentTrophyIndex = 0;
  private GameObject trophyBar;
  [SerializeField] private Sprite[] trophySprites;

  private void OnEnable() {
    trophyBar = GameObject.Find("Trophy Bar");
    EventManager.ScoreUpdated += CheckForLevelUp;
  }

  private void OnDisable() {
    EventManager.ScoreUpdated -= CheckForLevelUp;
  }

  private void CheckForLevelUp() {
    int currentScore = GameManager.Instance.levelManager.score;
    // int lastTrophyScore = GameManager.Instance.levelManager.trophyScores[^1];
    int trophyCount = GameManager.Instance.levelManager.trophyScores.Count;
    if (currentTrophyIndex == trophyCount) return; // Can't level up if already on final trophy
    int currentTrophyScore = GameManager.Instance.levelManager.trophyScores[currentTrophyIndex];
    if (currentScore >= currentTrophyScore && previousScore < currentTrophyScore) {
      currentTrophyIndex += 1;
      LevelUp();
    }
    previousScore = currentScore;
  }

  private void LevelUp() {
    PlayFireworksAnimation();
    ChangeSprite();
  }

  private void PlayFireworksAnimation() {
    Transform trophyBarImage = trophyBar.transform.GetChild(currentTrophyIndex);
    GameObject trophyBarImageParticles = trophyBarImage.GetChild(0).GetChild(0).gameObject;
    trophyBarImageParticles.SetActive(true);
  }

  private void ChangeSprite() {
    // Ignore first child because it's the slider
    Image trophyBarImage = trophyBar.transform.GetChild(currentTrophyIndex).GetComponent<Image>();
    trophyBarImage.sprite = trophySprites[currentTrophyIndex - 1];
  }
}
