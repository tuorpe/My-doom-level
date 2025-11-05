using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemyPlatform : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0.1f, 5f)] private float descentTime = 1f;
    [SerializeField] private float movementDelay = 0f;
    private float t;
    private bool isActivated;
    private float targetHeightDelta;
    private float startPosition;

    private Transform platform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var platPoly = GetComponentInChildren<PolyShape>();
        platform = platPoly.transform;
        var targetPosition = transform.Find("TargetHeight").position;
        startPosition = platform.transform.position.y;
        // Calculate the needed diffenece in Y-axis to move the object
        targetHeightDelta = targetPosition.y - platPoly.extrude;
        //targetHeight = startHeight - (platform.extrude - targetPosition.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isActivated) 
            return;
        t += Time.deltaTime / descentTime;
        var position = platform.position;
        position.y = Mathf.Lerp(startPosition, targetHeightDelta, t);
        platform.position = position;
        if(t >= 1f)
            isActivated = false;

    }

    public void Interact()
    {
        Invoke(nameof(ActivatePlatform), movementDelay);
    }

    public void ActivatePlatform()
    {
        isActivated = true;
    }
    // Not used for EnemyPlatform
    public void UnInteract() { }

    // Not used for EnemyPlatform
    public void ExitInteractArea() { }
}
