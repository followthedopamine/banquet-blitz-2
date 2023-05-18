using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveScaling : MonoBehaviour {
  private enum Operation {
    ScaleToFit,
    ScaleToFitWidth,
  }

  [SerializeField] private Operation operation;
  private float trueImageHeight;
  private float trueImageWidth;

  private void OnEnable() {
    Image image = gameObject.GetComponent<Image>();
    trueImageHeight = image.sprite.rect.height;
    trueImageWidth = image.sprite.rect.width;
    Debug.Log(trueImageWidth);
    if (operation == Operation.ScaleToFitWidth) ScaleToFitWidth();
  }

  private void ScaleToFitWidth() {
    RectTransform rect = gameObject.GetComponent<RectTransform>();
    float scaleFactor = Screen.width / trueImageWidth;
    rect.sizeDelta = new Vector2(Screen.width, trueImageHeight * scaleFactor);
  }
}
