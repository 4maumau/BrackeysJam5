﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
	//public AudioMixer audioMixer;
	public bool audioOn = true;
	public bool musicOn = true;

	//public AudioMixerGroup managerMixerGroup;

	Sound s;

	public Sound[] sounds;

	void Awake()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clips[0];
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void PlaySound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//s.source.clip = s.clips[0]; 
		s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (s.playOneShot) s.source.PlayOneShot(s.source.clip, s.volume);
		else s.source.Play();

	}

	public void StopSound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	public Sound GetSound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}
		
		return s;
	}


}
