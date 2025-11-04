using System;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IDoor, IInteractable
{
    private Vector3 openPosition;
    private Vector3 closePosition;
    private Vector3 targetPosition;
    private Vector3 currentVelocity = new Vector3(0,1,0);
    private enum DoorDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private DoorDirection doorOpeningDirection;
    [SerializeField] private bool isLocked;
    [SerializeField] private float closingTimeout = 5f;
    [SerializeField] private float movementSpeed = 1f;

    private bool blockedClosing = false;
    private Transform doorTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        doorTransform = transform.GetChild(0).GetComponent<Transform>();
        closePosition = doorTransform.position;
        targetPosition = doorTransform.position;
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        var size = meshRenderer.bounds.size;
        openPosition = doorOpeningDirection switch
        {
            DoorDirection.Up => closePosition + doorTransform.up * size.y,
            DoorDirection.Down => closePosition + doorTransform.up * -size.y,
            DoorDirection.Left => closePosition + doorTransform.forward * size.z,
            DoorDirection.Right => closePosition + doorTransform.forward * -size.z,
            _ => openPosition
        };
    }

    public void Interact()
    {
        if (isLocked)
            return;
        Open();
        Debug.Log("Let's open the door!");
    }

    // Update is called once per frame
    private void Update()
    {
        doorTransform.position = Vector3.SmoothDamp(doorTransform.position, targetPosition, ref currentVelocity, 1/movementSpeed);
    }
    
    public void Open()
    {
        targetPosition = openPosition;
        blockedClosing = true;
    }
    public void Close()
    {
        if (blockedClosing)
            Invoke(nameof(Close), closingTimeout);
        targetPosition = closePosition;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door collided with " + other.gameObject.name);
        if(!other.CompareTag("Player"))
            return;
        blockedClosing = true;
    }

    //Player exited the area of effect
    public void ExitInteractArea()
    {
        blockedClosing = false;
        Invoke(nameof(Close), closingTimeout);
    }
}
