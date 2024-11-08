using System;
using UnityEngine;

/// <summary>
/// Container for sound data
/// </summary>
[CreateAssetMenu(fileName = "SoundContainer", menuName = "ScriptableObjects/SoundContainer")]
public class SoundContainer : ScriptableObject
{
    public SoundType soundType;
    public AudioClip clip;

    [Range(0, 1)] public float volume = 1;
}
