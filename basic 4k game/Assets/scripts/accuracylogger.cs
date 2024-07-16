using System.Collections.Generic;
using UnityEngine;

public class AccuracyLogger : MonoBehaviour
{
    private List<AccuracyRating> accuracyLog = new List<AccuracyRating>();
    internal float accuracy = 0;
    int total;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LogAccuracy(AccuracyRating rating)
    {
        accuracyLog.Add(rating);
        total++;
        Debug.Log("Accuracy logged: " + rating.ToString());
    }
    public void CalculateAccuracyPercentage()
    {
        // Define weights for each accuracy rating
        Dictionary<AccuracyRating, float> accuracyWeights = new Dictionary<AccuracyRating, float>
        {
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
            // Optionally handle unexpected enum values
        }

        // Calculate percentage
        accuracy = (totalWeight / (accuracyLog.Count * 1.0f)) * 100.0f;
    }
}