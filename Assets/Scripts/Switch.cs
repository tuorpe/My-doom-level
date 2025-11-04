using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class Switch : MonoBehaviour, IInteractable
{
    private bool isOn = false;
    [SerializeField] private bool automaticSwitch = false;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject[] interactableGameobjects;
    private List<IInteractable> interactable = new List<IInteractable>();
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        if (interactableGameobjects.Length >= 1)
        {
            foreach (var go in interactableGameobjects)
            {
                if (go == null)
                    continue;
                var component = go.GetComponent<IInteractable>();
                if (component != null)
                    interactable.Add(go.GetComponent<IInteractable>());
                else
                    Debug.LogWarning($"Assigned GameObject '{go.name}' does not have an IInteractable component.");
            }
        }
    }
    public void Interact()
    {
        if(automaticSwitch)
            return;
        isOn = !isOn;
        if(spriteRenderer != null)
            spriteRenderer.sprite = isOn ? onSprite : offSprite;
        InteractAll(true);
    }
    
    // Not used for Switch
    public void UnInteract() { }
    public void ExitInteractArea()
    {
        // Not applicable for Switch
    }

    private void InteractAll(bool state)
    {
        foreach (var interacting in interactable)
        {
            if (state)
                interacting?.Interact();
            else
                interacting?.UnInteract();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered switch area.");
        if (!other.CompareTag("Player")) 
            return;
        if(automaticSwitch)
            InteractAll(true);
    }
}
