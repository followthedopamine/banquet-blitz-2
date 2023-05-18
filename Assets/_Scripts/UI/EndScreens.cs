using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreens : MonoBehaviour {
  [SerializeField] private GameObject winScreen;
  [SerializeField] private GameObject loseScreen;


  private void OnEnable() {
    EventManager.LevelWon += DisplayWinScreen;
    EventManager.LevelLost += DisplayLoseScreen;
  }

  private void OnDisable() {
    EventManager.LevelWon -= DisplayWinScreen;
    EventManager.LevelLost -= DisplayLoseScreen;
  }

  private void DisplayWinScreen() {
    winScreen.SetActive(true);
  }

  private void DisplayLoseScreen() {
    loseScreen.SetActive(true);
  }
}
