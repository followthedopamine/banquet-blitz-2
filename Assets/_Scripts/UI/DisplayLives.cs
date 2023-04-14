using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour {
  public GameObject livesContainer;
  public List<Image> livesImages;

  private void Start() {
    CreateLivesObjects();
    EventManager.LivesUpdated += ColourLives;
  }

  private void CreateLivesObjects() {
    GameObject life = livesContainer.transform.GetChild(0).gameObject;
    livesImages.Add(life.GetComponent<Image>());
    for (int i = 1; i < GameManager.MAX_LIVES; i++) {
      GameObject lifeCopy = Instantiate(life, livesContainer.transform);
      livesImages.Add(lifeCopy.GetComponent<Image>());
    }
    ColourLives();
  }

  private void ColourLives() {
    for (int i = 0; i < livesImages.Count; i++) {
      Image life = livesImages[i];
      if (i >= GameManager.Instance.lives) {
        // Spent lives
        life.color = new Color(0, 0, 0, 1);
      } else {
        // Remaining lives
        life.color = new Color(1, 1, 1, 1);
      }
    }
  }
}
