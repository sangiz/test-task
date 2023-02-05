using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using SF = UnityEngine.SerializeField;

namespace IgnSDK
{
    public class CinemachineManager : MonoBehaviour
    {
        // Serialized Fields

        [SF] private List<CinemachineVirtualCamera> virtualCameras;

        // Private fields

        public CinemachineBrain cinemachineBrain;

        private Tween shakeTween;
        public Camera Camera { get; private set; }

        // CinemachineManager

        public void Setup() 
        {
            cinemachineBrain = FindObjectOfType<CinemachineBrain>();

            Camera = cinemachineBrain.GetComponent<Camera>();
        }

        public void SetActiveCamera(VirtualCamera cameraType)
        {
            for (var i = 0; i < virtualCameras.Count; i++)
            {
                if (i == (int)cameraType)
                {
                    virtualCameras[i].Priority = 1;
                }
                else 
                {
                    virtualCameras[i].Priority = 0;
                }
            }
        }

        public void ShakeCamera(Ease inEase = Ease.InSine, float shakeIntensity = 5, float duration = 0.5f)
        {
            //TODO Make this method as base shake method and create presets for different shakes

            var currentCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
            var noiseProfile = currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            shakeTween?.Kill();
            shakeTween = DOTween.To(() => noiseProfile.m_AmplitudeGain, x => noiseProfile.m_AmplitudeGain = x, shakeIntensity, duration / 2).SetEase(inEase).OnComplete(() => 
            {
                shakeTween = DOTween.To(() => noiseProfile.m_AmplitudeGain, x => noiseProfile.m_AmplitudeGain = x, 0, duration / 2).SetEase(Ease.OutSine);
            });
        } 

        public CinemachineVirtualCamera GetCamera(VirtualCamera virtualCamera)
        {
            return virtualCameras[(int)virtualCamera];
        }
    }

    public enum VirtualCamera
    {
        // Add different cameras with presets eg: default, story, boss
        Menu,
        GamePlay
    }
}
