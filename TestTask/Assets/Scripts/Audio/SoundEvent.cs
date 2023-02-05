using FMODUnity;
using UnityEngine;

namespace IgnSDK
{
    [CreateAssetMenu(menuName = "IgnSDK/Audio/SoundEvent", fileName = "SoundEvent")]
    public class SoundEvent : ScriptableObject
    {
        public EventReference sound;

        private FMOD.Studio.EventInstance instance;

        public void Play3D(Vector3 pos)
        {
            if (!sound.IsNull)
            {
                RuntimeManager.PlayOneShot(sound, pos);
            }
            else if (App.SoundManager.enableDebugMessages)
            {
                Debug.LogError("No SoundEvent Assigned (3D)");
            }
        }

        public void Play2D()
        {
            if (!sound.IsNull)
            {
                RuntimeManager.PlayOneShot(sound);
            }
            else if (App.SoundManager.enableDebugMessages)
            {
                Debug.LogError("No SoundEvent Assigned (2D)");
            }
        }

        public void Play3DPassFloat(Vector3 pos, params (string parameter, float value)[] parameters)
        {
            if (!sound.IsNull)
            {
                instance = RuntimeManager.CreateInstance(sound);

                foreach (var (parameter, value) in parameters)
                {
                    instance.setParameterByName(parameter, value);
                }
                instance.set3DAttributes(pos.To3DAttributes());
                instance.start();
                instance.release();
            }
            else if (App.SoundManager.enableDebugMessages)
            {
                Debug.LogError("No SoundEvent Assigned (3D with params)");
            }
        }

        public void Play2DPassFloat(params (string parameter, float value)[] parameters)
        {
            if (!sound.IsNull)
            {
                instance = RuntimeManager.CreateInstance(sound);

                foreach (var (parameter, value) in parameters)
                {
                    instance.setParameterByName(parameter, value);
                }
                instance.start();
                instance.release();
            }
            else if (App.SoundManager.enableDebugMessages)
            {
                Debug.LogError("No SoundEvent Assigned (2D with params)");
            }
        }
    }
}
