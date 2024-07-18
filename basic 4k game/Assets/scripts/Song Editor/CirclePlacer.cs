using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CirclePlacer : MonoBehaviour
{
    public GameObject circlePrefab; // Reference to the circle prefab
    public GameObject audiosource;
    private AudioSource song;
    private audiosourceinfo songinfo;
    public List<GameObject> circles = new List<GameObject>(); // List to keep track of circles
    internal float[] anchorpointsbpm;

    // Define anchor points
    float[] anchorPoints = { -2f, -0.666f, 0.666f, 2f };
    private void Start()
    {
        song = audiosource.GetComponent<AudioSource>();
        songinfo = audiosource.GetComponent<audiosourceinfo>();
        
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Snap mouse position to nearest anchor point
            float nearestAnchor = FindNearestAnchor(mousePosition.x);
            float nearestbpm = SnapToBPM(mousePosition.y);
            Vector2 snappedPosition = new Vector2(nearestAnchor, nearestbpm);

            // Place a circle at the snapped position
            PlaceCircleAtPosition(snappedPosition);
        }
    }

    void PlaceCircleAtPosition(Vector2 position)
    {
        // Instantiate circle prefab at the position
        GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity, transform);
        circles.Add(circle);
        Debug.Log("Placed circle at position: " + position);
    }


    float SnapToBPM(float positionY)
    {
        float minDistance = Mathf.Infinity;
        float nearestAnchor = positionY;

        foreach (float anchorY in anchorpointsbpm)
        {
            float distance = Mathf.Abs(positionY - anchorY);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestAnchor = anchorY;
            }
        }

        return nearestAnchor;
    }

    float FindNearestAnchor(float xPosition)
    {
        // Find the nearest anchor point
        float minDistance = Mathf.Infinity;
        float nearestAnchor = xPosition;

        foreach (float anchor in anchorPoints)
        {
            float distance = Mathf.Abs(xPosition - anchor);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestAnchor = anchor;
            }
        }

        return nearestAnchor;
    }
}
