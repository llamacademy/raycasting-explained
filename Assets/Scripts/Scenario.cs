using UnityEngine;
using UnityEngine.Animations;

public class Scenario : MonoBehaviour
{
    public Transform CameraTarget;
    public Transform RaycastOrigin;
    public Transform AnimatedObject;
    public QueryTriggerInteraction TriggerInteraction;
    public LayerMask LayerMask;
    public float RaycastLength = float.MaxValue;
    public RaycastType Type;
    [Header("Only used for RaycastType Spherecast|SpherecastAll|Capsulecast|CapsulecastAll")]
    public float Radius = 0.5f;
    [Header("Only used for RaycastType Boxcast|BoxcastAll")]
    public Vector3 HalfExtents = new Vector3(0.25f, 0.25f, 0.25f);
    [Header("Only used for RaycastType Capsulecast|CapsulecastAll")]
    public float Height = 0.25f;


    public enum RaycastType
    {
        Raycast,
        RaycastAll,
        Spherecast,
        SpherecastAll,
        Capsulecast,
        CapsulecastAll,
        Boxcast,
        BoxcastAll
    }
}
