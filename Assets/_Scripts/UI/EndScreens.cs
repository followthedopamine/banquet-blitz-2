using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreens : MonoBehaviour {
  [SerializeField] private GameObject winScreen;
  [SerializeField] private GameObject loseScreen;
  [SerializeField] private Sprite[] winScreenTrophies;
  [SerializeField] private GameObject winScreenTrophy;
  [SerializeField] private GameObject[] fireworksObjects;
  private Image winScreenTrophyImage;

  private void Awake() {
    winScreenTrophyImage = winScreenTrophy.GetComponent<Image>();
  }

  private void OnEnable() {
    EventManager.LevelWon += DisplayWinScreen;
    EventManager.LevelLost += DisplayLoseScreen;
  }

  private void OnDisable() {
    EventManager.LevelWon -= DisplayWinScreen;
    EventManager.LevelLost -= DisplayLoseScreen;
  }

  private void DisplayWinScreen() {
    SetWinScreenTrophyAndFireworks();
    winScreen.SetActive(true);
  }

  private void DisplayLoseScreen() {
    loseScreen.SetActive(true);
  }

  private void SetWinScreenTrophyAndFireworks() {
    int currentTrophy = GameManager.Instance.levelManager.currentTrophy;
    if (currentTrophy > -1 && currentTrophy < winScreenTrophies.Length) {
      winScreenTrophyImage.sprite = winScreenTrophies[currentTrophy];
      fireworksObjects[currentTrophy].SetActive(true);
    }
    // TODO: Consider making empty trophy image
    if (currentTrophy == -1) winScreenTrophy.SetActive(false);
  }
}
