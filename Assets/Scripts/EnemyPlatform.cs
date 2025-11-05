using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemyPlatform : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0.1f, 5f)] private float descentTime = 1f;
    private float t;
    private bool isActivated;
    private float targetHeight;
    private float startHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var platform = GetComponentInChildren<PolyShape>();
        var targetPosition = transform.Find("TargetHeight").position;
        startHeight = transform.position.y;
        targetHeight = startHeight - (platform.extrude - targetPosition.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isActivated) 
            return;
        t += Time.deltaTime / descentTime;
        var position = transform.position;
        position.y = Mathf.Lerp(startHeight, targetHeight, t);
        transform.position = position;
        if(t >= 1f)
            isActivated = false;

    }

    public void Interact()
    {
        isActivated = true;
    }

    // Not used for EnemyPlatform
    public void UnInteract() { }

    // Not used for EnemyPlatform
    public void ExitInteractArea() { }
}
