using UnityEngine;

public class PilarRotator : MonoBehaviour
{
    [SerializeField]private float rotationTime = 10f;
    private Vector3 rotationPoint;
    private void Start()
    {
        var rend = GetComponent<Renderer>();
        if (rend != null)
            rotationPoint = rend.bounds.center;
    }
    private void Update()
    {
        transform.RotateAround(rotationPoint, Vector3.up, 360f / rotationTime * Time.deltaTime);
    }
}
