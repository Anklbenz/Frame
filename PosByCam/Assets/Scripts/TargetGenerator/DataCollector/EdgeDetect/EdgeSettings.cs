using OpenCvSharp;
using UnityEngine;

[CreateAssetMenu]
public class EdgeSettings : ScriptableObject {
    public bool getRedChannelOnly = false;
    public float canny1, canny2;

    [Space]
    public bool blurEnabled;

    public int blurSize1 = 5, blurSize2 = 5;

    [Space]
    public bool thresholdEnabled;

    public float threshold, thresholdMax = 255;

    [Space]
    public bool adaptiveThresholdEnabled;

    public float adaptiveMax = 255;
    public int adaptiveBlockSize = 11, adaptiveC = 2;
    public ThresholdTypes thresholdType;
    public AdaptiveThresholdTypes adaptiveThresholdType;

    [Space]
    public bool dilateEnabled;
}


