using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for Toggle component

public class ShowFps : MonoBehaviour {
    public TextMeshProUGUI FpsText;  // Reference to the FPS Text
    public Toggle FpsToggle;        // Reference to a UI Toggle

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    void Update() {
        // Check if the toggle is on
        if (FpsToggle != null && FpsToggle.isOn) {
            // Update time
            time += Time.deltaTime;

            // Count this frame
            frameCount++;

            if (time >= pollingTime) {
                // Update frame rate
                int frameRate = Mathf.RoundToInt((float)frameCount / time);
                FpsText.text = "FPS " + frameRate.ToString();

                // Reset time and frame count
                time -= pollingTime;
                frameCount = 0;
            }
        } else {
            // Hide the text when toggle is off
            FpsText.text = string.Empty;
        }
    }
}
