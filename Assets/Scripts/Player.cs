using System;
using System.Collections.Generic;
using System.Linq;
using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float mouseSensitivity = 30f;
    [SerializeField] private float gravityMultiplier = 30f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float verticalLookRange = 80f;

    private Vector3 moveInput;
    private Vector2 lookDeltaInput;

    private Vector3 jumpVelocity;

    //private float verticalVelocity = 0f;
    private float verticalRotation = 0f;
    private IInteractable interactable;
    private int health = 100;
    private int armor = 0;
    [SerializeField] private List<WeaponData> weaponInventory = new List<WeaponData>();
    //[SerializeField] private WeaponScriptable[] weaponInventory = new WeaponScriptable[5];
    // Load all weapon information
    WeaponData[] allWeapons; // = Resources.LoadAll<WeaponData>("Weapons");

private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue inputValue)
    {
        var rawInput = inputValue.Get<Vector2>();
        moveInput = new(rawInput.x, 0f, rawInput.y);
    }

    public void OnLook(InputValue inputValue)
    {
        lookDeltaInput = inputValue.Get<Vector2>() * mouseSensitivity;
    }

    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            jumpVelocity = (transform.forward + transform.up) * jumpForce;
            //verticalVelocity += jumpForce;
        }
    }
    /*
    public void ItemPickUp(ItemData item)
    {
        // Implement item pickup logic here
        Debug.Log("Picking up item...");
        switch (item.itemType)
        {
            case ItemData.Type.HealthPack:
                health = Mathf.Min(health + item.value, 100);
                break;
            case ItemData.Type.Armor:
                armor = Mathf.Min(armor + item.value, 100);
                break;
            case ItemData.Type.Ammo:
                var weapon = weaponInventory.FirstOrDefault(w => w != null && w.type == item.ammoType);
                if (weapon == null)
                    return;
                weapon.currentAmmo = Mathf.Min(weapon.currentAmmo + item.value, weapon.magazineSize);
                break;
            case ItemData.Type.Weapon:
                var foundWeapon = weaponInventory.FirstOrDefault(w => w != null && w.type == item.ammoType);
                if (foundWeapon == null)
                    AddNewWeapon(item);
                else 
                    foundWeapon.currentAmmo = Mathf.Min(foundWeapon.currentAmmo + item.value, foundWeapon.magazineSize);
                break;
            case ItemData.Type.Keycard:
                break;
            case ItemData.Type.Other:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }*/
    
    private void AddNewWeapon(ItemData weapon)
    {
        var newWeapon = ScriptableObject.CreateInstance<WeaponData>();
        
        weaponInventory.Add(newWeapon);
    }
    public void OnInteract()
    {
        Debug.Log("Interacting...");
        interactable?.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interacting = other.GetComponent<IInteractable>();
        if (interacting != null)
        {
            interactable = interacting;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(interactable == null)
            return;
        interactable.ExitInteractArea();
        interactable = null;
        Debug.Log("Exited interact area of " + other.name);
    }
    
    private void Update()
    {
        transform.Rotate(0f, lookDeltaInput.x * Time.deltaTime, 0f);
        if (!Mathf.Approximately(lookDeltaInput.y, 0f))
        {
            verticalRotation = Mathf.Clamp(verticalRotation - lookDeltaInput.y * Time.deltaTime, -verticalLookRange, verticalLookRange);
            if (Camera.main != null) Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
        
        var movement = (transform.forward * moveInput.z + transform.right * moveInput.x);
        if (characterController.isGrounded && jumpVelocity.y < 0f)
        {
            jumpVelocity = Vector3.down;
            //verticalVelocity = -1f;
        }
        else
        {
            //verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            jumpVelocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
        }

        movement += jumpVelocity;
        //movement.y = verticalVelocity;

        characterController.Move(Time.deltaTime * movementSpeed * movement);
    }
}
