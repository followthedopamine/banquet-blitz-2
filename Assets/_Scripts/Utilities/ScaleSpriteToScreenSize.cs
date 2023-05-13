using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSpriteToScreenSize : MonoBehaviour {

  private void Start() {
    float scalingFactor = GetScalingFactor();
    gameObject.transform.localScale = new Vector3(scalingFactor, scalingFactor, gameObject.transform.localScale.z);
  }

  private float GetScalingFactor() {


    SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    float imageWidth = spriteRenderer.sprite.bounds.size.x;
    float imageHeight = spriteRenderer.sprite.bounds.size.y;
    float worldScreenHeight = Camera.main.orthographicSize * 2f; // 10f
    float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; // 10f

    Debug.Log("Image width: " + imageWidth);
    Debug.Log("Image height: " + imageHeight);
    Debug.Log("Screen width: " + worldScreenWidth);
    Debug.Log("Screen height: " + worldScreenHeight);
    Debug.Log("Scaling width: " + worldScreenWidth / imageWidth);
    Debug.Log("Scaling height: " + worldScreenHeight / imageHeight);

    return Mathf.Max(worldScreenWidth / imageWidth, worldScreenHeight / imageHeight);
  }
}