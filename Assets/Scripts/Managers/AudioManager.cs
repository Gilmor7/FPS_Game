using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _ambianceSFXAudioSource;
        [SerializeField] private List<SoundObject> _soundObjects;
        
        private Dictionary<SoundType, AudioClip> _soundObjectsLookup;

        public static AudioManager Instance { get; private set; }
        
        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                _soundObjectsLookup =
                    _soundObjects.ToDictionary(key => key.SoundType, value => value.AudioClip);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySoundEffect(AudioSource audioSource, SoundType? soundType)
        {
            var audioClip = GetSound(soundType);
            if (audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        private AudioClip GetSound(SoundType? soundType)
        {
            SoundType keyToLookFor = soundType ?? SoundType.None;
  
            _soundObjectsLookup.TryGetValue(keyToLookFor, out var audioClip);
            return audioClip;
        }

        public void PlaySoundEffect(SoundType? soundType)
        {
            var audioClip = GetSound(soundType);
            if (audioClip != null)
            {
                _ambianceSFXAudioSource.PlayOneShot(audioClip);
            }
        }
    }
}
