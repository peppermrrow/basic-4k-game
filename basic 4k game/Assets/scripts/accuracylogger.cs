using System.Collections.Generic;
using UnityEngine;

public class AccuracyLogger : MonoBehaviour
{
    private List<AccuracyRating> accuracyLog = new List<AccuracyRating>();
    internal float accuracy = 0;
    public void LogAccuracy(AccuracyRating rating)
    {
        accuracyLog.Add(rating);
        Debug.Log("Accuracy logged: " + rating.ToString());
    }
}