using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour {
  public GameObject livesContainer;
  public List<Image> livesImages;
  [SerializeField] private Sprite filledSprite;
  [SerializeField] private Sprite emptySprite;


  private void OnEnable() {
    CreateLivesObjects();
    EventManager.LivesUpdated += ColourLives;
  }

  private void OnDisable() {
    EventManager.LivesUpdated -= ColourLives;
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
        life.sprite = emptySprite;
      } else {
        // Remaining lives
        life.sprite = filledSprite;
      }
    }
  }
}
