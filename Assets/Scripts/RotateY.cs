using UnityEngine;

public class RotateY : MonoBehaviour
{
    [SerializeField] private float speed; // 초당 회전 (도)
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.Self);
    }
}