using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    public GameObject camera;  // Assign your camera in the Inspector
    public LayerMask pickableLayer;  // Assign the pickable layer in the Inspector
    public Transform holdPosition;   // Position where you want to hold items (assign an empty GameObject as child of player)
    [Range(1f, 20f)] // Optional: This creates a slider in the Inspector for easy adjustment
    public float maxPickupDistance = 10f;  // Max distance for picking up items
    private GameObject itemCurrentlyHolding;
    private bool isHolding = false;
    private Vector3 originalScale; // Store the original scale of the item

    private void Update()
    {
        // Press 'E' to pick up items
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }

        // Press 'Q' to drop the currently held item
        if (Input.GetKeyDown(KeyCode.Q) && isHolding)
        {
            Drop();
        }
    }

    public void Pickup()
    {
        RaycastHit hit;
        // Visualize the raycast in the Scene view for debugging
        Debug.DrawRay(camera.transform.position, camera.transform.forward * maxPickupDistance, Color.red, 2f);

        // Perform a raycast to detect objects in the pickable layer within a certain distance
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxPickupDistance, pickableLayer))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);  // Log the object hit by the raycast

            // Check if the hit object is tagged as "Pickable"
            if (hit.transform.CompareTag("Pickable"))
            {
                // Drop the currently held object if the player is already holding one
                if (isHolding) Drop();

                // Assign the hit object as the item currently being held
                itemCurrentlyHolding = hit.transform.gameObject;

                // Store the original scale of the object
                originalScale = itemCurrentlyHolding.transform.localScale;

                // Disable colliders and physics on the picked-up object
                foreach (var c in hit.transform.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = false;
                foreach (var r in hit.transform.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = true;

                // Attach the object to the hold position
                itemCurrentlyHolding.transform.SetParent(holdPosition, false); // Keep local position and rotation
                itemCurrentlyHolding.transform.localPosition = Vector3.zero; // Adjust this if needed
                itemCurrentlyHolding.transform.localRotation = Quaternion.Euler(0, 90, 0); // Set desired rotation

                // Optionally, set the scale to match the desired size
                // itemCurrentlyHolding.transform.localScale = Vector3.one; // Uncomment if you want to reset the scale to 1

                isHolding = true;
            }
            else
            {
                Debug.Log("Hit object is not tagged as 'Pickable'.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything!");
        }
    }

    public void Drop()
    {
        if (itemCurrentlyHolding == null) return;

        // Detach the item from the player
        itemCurrentlyHolding.transform.SetParent(null);

        // Re-enable colliders and physics on the dropped object
        foreach (var c in itemCurrentlyHolding.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = true;
        foreach (var r in itemCurrentlyHolding.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = false;

        // Reset the scale of the item to its original scale
        itemCurrentlyHolding.transform.localScale = originalScale;

        // Optionally apply a small force to give a natural drop feeling
        Rigidbody rb = itemCurrentlyHolding.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);  // Push the object forward slightly
        }

        isHolding = false;
        itemCurrentlyHolding = null;
    }
}
