using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace amed.utils.sound
{
    public class SoundManager : MonoBehaviour
    {
        List<AudioSource> _audioSources = new List<AudioSource>();
        
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject currentChild = transform.GetChild(i).gameObject;
                if (currentChild.TryGetComponent(out AudioSource audioS))
                {
                    _audioSources.Add(audioS);
                }
            }
        }

        public AudioSource PlaySound(AudioClip audioClip, float volume = 1, bool isLooping = false, float delay = 0)
        {
            AudioSource audioS = GetAvailableSoundSource();

            audioS.volume = volume;
            audioS.loop = isLooping;
            audioS.clip = audioClip;

            if (delay < 0) delay = 0;

            if (delay > 0)
            {
                audioS.PlayDelayed(delay);
            }
            else
            {
                audioS.Play();
            }

            return audioS;
        }

        public void StopSound(AudioSource audioS)
        {
            audioS.Stop();
        }

        public void PauseSound(AudioSource audioS)
        {
            audioS.Pause();
        }

        public void StopLoopingSounds()
        {
            foreach (var item in _audioSources)
            {
                if (item.loop)
                {
                    item.Stop();
                }
            }
        }

        public void StopNonLoopingSounds()
        {
            foreach (var item in _audioSources)
            {
                if (!item.loop)
                {
                    item.Stop();
                }
            }
        }

        public void StopAllSounds()
        {
            foreach (var item in _audioSources)
            {
                item.Stop();
            }
        }

        public void PauseAllSounds()
        {
            foreach (var item in _audioSources)
            {
                item.Pause();
            }
        }

        public void UnPauseAllSounds()
        {
            foreach (var item in _audioSources)
            {
                item.UnPause();
            }
        }

        AudioSource GetAvailableSoundSource()
        {
            foreach (var item in _audioSources)
            {
                if (item.isPlaying) continue;

                return item;
            }

            GameObject go = new GameObject();
            go.transform.parent = transform;
            AudioSource audioS = go.AddComponent<AudioSource>();
            _audioSources.Add(audioS);
            return audioS;
        }
    }
}
