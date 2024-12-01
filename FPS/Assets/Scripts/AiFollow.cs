using UnityEngine;

public class AiFollow : MonoBehaviour
{
    public Transform target;  // Reference to the target (Player)
    public float speed = 3.0f; // Speed at which the AI follows
    public float stoppingDistance = 1.0f; // Distance to stop at

    void Update()
    {
        // Check if the target is set
        if (target == null)
            return;

        // Move towards the target
        float step = speed * Time.deltaTime; // Calculate the step size
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
