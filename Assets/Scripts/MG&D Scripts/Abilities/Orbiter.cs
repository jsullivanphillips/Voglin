using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform target; // The object to orbit around
    public float speed = 50f; // The speed of orbiting

    void Update()
    {
        // Rotate around the target every frame
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
    }
}
