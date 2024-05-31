using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") 
        {
            AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
            Destroy(gameObject);
            Debug.Log("coin picked up");
        }
        
    }
}
