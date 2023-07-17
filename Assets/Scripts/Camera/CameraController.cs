using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(_offset.x, _target.position.y + _offset.y, _offset.z);
    }
}