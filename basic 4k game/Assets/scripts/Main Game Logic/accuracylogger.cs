using System.Collections.Generic;
using UnityEngine;

public class AccuracyLogger : MonoBehaviour
{
    private List<AccuracyRating> accuracyLog = new List<AccuracyRating>();
    internal float accuracy = 0;
    private void Awake()
    {
        //need to change scene to show results. will work on resetting later
        DontDestroyOnLoad(gameObject); 
    }
    public void LogAccuracy(AccuracyRating rating)
    {
        accuracyLog.Add(rating);
        Debug.Log("Accuracy logged: " + rating.ToString());
    }
    public void CalculateAccuracyPercentage()
    {
        Dictionary<AccuracyRating, float> accuracyWeights = new Dictionary<AccuracyRating, float>
        {
            // accuracy percentages
            { AccuracyRating.Perfect, 1.0f },
            { AccuracyRating.Good, 0.75f },
            { AccuracyRating.OK, 0.3f },
            { AccuracyRating.Bad, 0.1f },
            { AccuracyRating.Miss, 0f }
        };

        // Calculate total weight
        float totalWeight = 0;
        foreach (AccuracyRating rating in accuracyLog)
        {
            if (accuracyWeights.ContainsKey(rating))
            {
                totalWeight += accuracyWeights[rating];
            }
        }

        // Calculate percentage
        accuracy = (totalWeight / (accuracyLog.Count * 1.0f)) * 100.0f;
    }
}