using DG.Tweening;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light directionalLight;

    [Header("Intensity")]
    [SerializeField] private float startIntensity = 0.5f;
    [SerializeField] private float targetIntensity = 1.2f;

    [Header("Temperature (Kelvin)")]
    [SerializeField] private float startTemperature = 4500f;
    [SerializeField] private float targetTemperature = 6500f;

    [Header("Transition")]
    [SerializeField] private float transitionDuration = 3f;
    [SerializeField] private Ease easing = Ease.InOutSine;

    private Tween intensityTween;
    private Tween temperatureTween;

    private void Awake()
    {
        if (!directionalLight)
            directionalLight = GetComponent<Light>();

        directionalLight.useColorTemperature = true;
    }

    private void Start() 
    {
        StartLightTransition();
    }

    public void StartLightTransition()
    {
        // Kill any running tweens to avoid stacking
        intensityTween?.Kill();
        temperatureTween?.Kill();

        // Set start values explicitly
        directionalLight.intensity = startIntensity;
        directionalLight.colorTemperature = startTemperature;

        float durationSeconds = transitionDuration * 60f;

        // Intensity tween
        intensityTween = DOTween.To(
                () => directionalLight.intensity,
                value => directionalLight.intensity = value,
                targetIntensity,
                durationSeconds
            )
            .SetEase(easing)
            .OnComplete(() =>
            {
                directionalLight.intensity = targetIntensity;
            });

        // Temperature tween
        temperatureTween = DOTween.To(
                () => directionalLight.colorTemperature,
                value => directionalLight.colorTemperature = value,
                targetTemperature,
                durationSeconds
            )
            .SetEase(easing)
            .OnComplete(() =>
            {
                directionalLight.colorTemperature = targetTemperature;
            });
    }
}
