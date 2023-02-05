using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using SF = UnityEngine.SerializeField;

public class PostProcessingManager : MonoBehaviour
{
    // Serialized Field

    [SF] private Volume volume;

    private DepthOfField depthOfField;
    private LensDistortion lendDistortion;

    private Tween depthOfFieldTween;
    private Tween lendDistortionTween;

    public void Init()
    {
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out lendDistortion);
    }

    public void SetDepthOfField(float targetValue, float lerpDuration, Ease ease = Ease.OutSine) 
    {
        depthOfFieldTween?.Kill();
        depthOfFieldTween = DOTween.To(() => depthOfField.focalLength.value, x => depthOfField.focalLength.value = x, targetValue, lerpDuration).SetUpdate(true).SetEase(ease);
    }

    public void SetLensDistortion(float targetValue, float lerpDuration, Ease ease = Ease.OutSine)
    {
        lendDistortionTween?.Kill();
        lendDistortionTween = DOTween.To(() => lendDistortion.intensity.value, x => lendDistortion.intensity.value = x, targetValue, lerpDuration).SetUpdate(true).SetEase(ease);
    }
}
