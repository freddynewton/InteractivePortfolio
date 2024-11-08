using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Manager for playing sounds
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    // Container for sound data
    [SerializeField] private List<SoundContainer> soundContainers;

    private Dictionary<SoundType, SoundContainer> soundContainerDictionary;

    public override void Awake()
    {
        base.Awake();
        soundContainerDictionary = soundContainers.ToDictionary(x => x.soundType);
    }

    /// <summary>
    /// Play a sound
    /// </summary>
    /// <param name="soundType">The type of sound to play</param> 
    public void PlaySound(SoundType soundType)
    {
        if (soundContainerDictionary.TryGetValue(soundType, out SoundContainer soundContainer))
        {
            AudioSource.PlayClipAtPoint(soundContainer.clip, Camera.main.transform.position, soundContainer.volume);
        }
        else
        {
            Debug.LogWarning($"SoundContainer for {soundType} not found");
        }
    }
}
