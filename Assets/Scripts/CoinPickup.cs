using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    [SerializeField] int coinScore = 10;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().ScoreUpdate(coinScore);
            AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("coin picked up");
        }
        
    }
}
