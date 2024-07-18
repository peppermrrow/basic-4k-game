using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class buttonhandler : MonoBehaviour
{
    private buttoninfo button;
    public AccuracyLogger accuracyLogger;
    private bool trigger;
    private List<GameObject> circles = new List<GameObject>();
    private List<float> timers = new List<float>();
    private float timer;
    private bool getkeydown = false;
    private List<(float min, float max, AccuracyRating rating)> accuracyRanges;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<buttoninfo>();
        accuracyLogger = FindObjectOfType(typeof(AccuracyLogger)) as AccuracyLogger;

        // Define the accuracy ranges and corresponding ratings
        accuracyRanges = new List<(float, float, AccuracyRating)>
        {
            (float.MinValue, 1.33f, AccuracyRating.Miss),
            (1.33f, 1.5f, AccuracyRating.Bad),
            (1.5f, 1.7f, AccuracyRating.OK),
            (1.7f, 1.9f, AccuracyRating.Good),
            (1.9f, 2.1f, AccuracyRating.Perfect),
            (2.1f, 2.3f, AccuracyRating.Good),
            (2.3f, 2.5f, AccuracyRating.OK),
            (2.5f, 2.7f, AccuracyRating.Bad),
            (2.7f, float.MaxValue, AccuracyRating.Miss)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (timers.Count > 0)
        {
            Debug.Log(timers[0]);
        }
        if (Input.GetKeyDown(button.key))
        {
            Debug.Log("works");
            getkeydown = true;
            LogAccuracy(timers[0]);
            Destroy(circles[0]);
            circles.RemoveAt(0);
            timers.RemoveAt(0);
            getkeydown = false;
            
        }
        for (int i = 0; i < timers.Count; i++) 
        {
            timers[i] += Time.deltaTime * GameSettings.instance.scrollspeed;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.CompareTag("circle") && !circles.Contains(other.gameObject))
        {
            Debug.Log("collision with circle");
            circles.Add(other.gameObject);
            Debug.Log("added " + other.gameObject.name + " to list");
            timers.Add(0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("circle") && circles.Contains(other.gameObject) && getkeydown == false)
        {
            Destroy(other.gameObject);
            circles.Remove(other.gameObject);
            timers.Remove(timers[0]);
        }
    }
    public void LogAccuracy(float timer)
    {
        foreach (var range in accuracyRanges)
        {
            if (timer >= range.min && timer <= range.max)
            {
                accuracyLogger.LogAccuracy(range.rating);
                break;
            }
        }
    }
}
