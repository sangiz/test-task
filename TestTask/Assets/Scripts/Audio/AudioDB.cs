
using System;
using UnityEngine;

namespace IgnSDK
{
    [CreateAssetMenu(menuName = "IgnSDK/Audio/AudioDB", fileName = "AudioDB")]
    public class AudioDB : ScriptableObject
    {
        [Header("Music")]

        public SoundEvent mainMenuMusic;

        [Header("UI Sounds")]

        public SoundEvent buttonClick;
        public SoundEvent buttonSelect;
        public SoundEvent sliderMove;
        public SoundEvent dialogShow;
    }
}
