using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour {

  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject winScreen;
  [SerializeField] private GameObject loseScreen;
  [SerializeField] private GameObject mainMenu;

  public void HomeButton() {
    // Show home screen and disable all other screens
    DisableAllUI();
    mainMenu.SetActive(true);
  }

  public void RetryButton() {
    SceneController.Instance.ReloadCurrentLevel();
  }

  public void NextLevelButton() {
    SceneController.Instance.LoadNextLevel();
  }

  private void DisableAllUI() {
    gameUI.SetActive(false);
    winScreen.SetActive(false);
    loseScreen.SetActive(false);
    mainMenu.SetActive(false);
  }
}
