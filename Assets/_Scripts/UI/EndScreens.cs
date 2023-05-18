using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreens : MonoBehaviour {
  [SerializeField] private GameObject winScreen;
  [SerializeField] private GameObject loseScreen;


  private void Start() {
    EventManager.LevelLoaded += AddListeners;
  }

  private void AddListeners(LevelManager levelManager) {
    EventManager.LevelWon += DisplayWinScreen;
    EventManager.LevelLost += DisplayLoseScreen;
  }

  private void DisplayWinScreen() {
    winScreen.SetActive(true);
  }

  private void DisplayLoseScreen() {
    loseScreen.SetActive(true);
  }
}
