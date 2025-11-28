using System.Collections;
using UnityEngine;

public class RotatingHand : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private Transform pointOfRotation;

    [SerializeField] private float rotationStart = 0;
    [SerializeField] private float rotationEnd = 0;
    [SerializeField] private float period = 4;

    private Coroutine rotatingCoroutine;

    private void OnEnable()
    {
        pointOfRotation.rotation = Quaternion.Euler(new (0, 0, rotationStart));
        rotatingCoroutine = StartCoroutine(RotateHand());
    }

    private void OnDisable()
    {
        StopCoroutine(rotatingCoroutine);
    }

    private (float, float) GetRotationJourney(float start, float finish)
    {
        float begin = start % 360;
        float end = finish % 360;
        if (end == 0)
        {
            end = 360;
        }

        return (begin, end);
    }

    private IEnumerator RotateHand()
    {
        float time = 0;

        (float start, float targetRotation) = GetRotationJourney(rotationStart, rotationEnd);

        while (true)
        {
            float rotation = Mathf.Lerp(start, targetRotation, time / period);

            if (time >= period)
            {
                rotation = targetRotation % 360;
                (start, targetRotation) = GetRotationJourney(targetRotation, start);

                time -= period;
            }

            pointOfRotation.rotation = Quaternion.Euler(new (0, 0, rotation));
            time += Time.deltaTime;
            yield return null;
        }
    }
}
