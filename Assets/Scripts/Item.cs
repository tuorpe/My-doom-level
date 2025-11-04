using System;
using Scriptables;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField, Range(0.01f, 2)] private float bobbingAmplitude = 0.5f;
    [SerializeField, Range(0.1f, 10f)] private float bobbingFrequency = 4f;
    [SerializeField, Range(0f, 5f)]private float rotationSpeed = 1f;
    
    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        // Bobbing effect
        var newY = Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude + initialPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.one, rotationSpeed * 360f * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) 
            return;
        // Assuming the player has a method to pick up items
        var player = other.GetComponent<Player>();
        //player?.ItemPickUp(itemData);
        Destroy(gameObject);
    }
}
