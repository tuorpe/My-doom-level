using UnityEngine;

public class Keycard : MonoBehaviour, IInteractable
{
    [SerializeField] private Door linkedDoor;
  
    public void Interact()
    {
        // Not applicable for Keycard
    }
    public void ExitInteractArea()
    {
        // Not applicable for Keycard
    }

    public void PickUp()
    {
        // Unlock the linked door when the keycard is picked up
        linkedDoor?.Unlock();
        // Destroy the keycard after picking it up
        Destroy(gameObject);
    }
}
