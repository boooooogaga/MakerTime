using UnityEngine;
using Cinemachine; // Если у вас старая версия Unity, используйте: using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _noise; // В старых версиях: CinemachineBasicMultiChannelPerlin
    private float _shakeTimer;

    void Start()
    {
        // Получаем доступ к настройкам шума камеры
        var vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            _noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void Shake(float intensity,float frequency, float time)
    {
        if (_noise == null) return;

        // Включаем тряску с заданной силой
        _noise.m_AmplitudeGain = intensity;
        _noise.m_FrequencyGain = frequency;
        _shakeTimer = time;
        Debug.Log("Camera shake triggered with intensity: " + intensity + " for duration: " + time);
    }

    void Update()
    {
        // Плавное затухание тряски со временем
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                // Время вышло — выключаем тряску
                _noise.m_AmplitudeGain = 0f;
            }
        }
    }
}