using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMAnchors : MonoBehaviour
{
    public GameObject bpmMarkerPrefab;
    public CirclePlacer placer;
    public GameObject audiosource;
    private AudioSource song;
    private audiosourceinfo songinfo;
    private float[] anchorpointsbpm;
    // Start is called before the first frame update
    void Start()
    {
        placer = GetComponent<CirclePlacer>();
        song = audiosource.GetComponent<AudioSource>();
        songinfo = audiosource.GetComponent<audiosourceinfo>();
        BPMAnchor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BPMAnchor()
    {
        float songDuration = song.clip.length;
        float anchorbpm = songinfo.bpm * 4;
        float secondsperbeat = 60f / anchorbpm;
        int totalbeats = Mathf.RoundToInt(songDuration / secondsperbeat);

        anchorpointsbpm = new float[totalbeats];

        for (int i = 0; i < totalbeats; i++)
        {
            float beattime = i * secondsperbeat;
            anchorpointsbpm[i] = beattime;
        }
    }
    void InstantiateBPMMarkers()
    {
        foreach (float anchor in anchorpointsbpm)
        {
            
            Vector3 markerPosition = new Vector3(0f, anchor, 0f); 

            // Instantiate the BPM marker prefab at the calculated position
            GameObject bpmMarker = Instantiate(bpmMarkerPrefab, markerPosition, Quaternion.identity);
            bpmMarker.transform.SetParent(transform); // Set parent to this object or the timeline container
        }
    }
}
