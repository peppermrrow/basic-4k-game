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


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<buttoninfo>();
        accuracyLogger = FindObjectOfType(typeof(AccuracyLogger)) as AccuracyLogger;
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
    private void LogAccuracy()
    {
        if (timers[0] < 1.33f)
        {
            accuracyLogger.LogAccuracy(AccuracyRating.Miss);
        }
    }
}
