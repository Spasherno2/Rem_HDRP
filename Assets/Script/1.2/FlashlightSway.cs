using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    [Header("Position sway")]
    [SerializeField] private float posSwayAmount = 0.015f;
    [SerializeField] private float posMax = 0.03f;

    [Header("Rotation sway")]
    [SerializeField] private float rotSwayAmount = 2.0f;
    [SerializeField] private float rotMax = 6.0f;

    [Header("Smoothing")]
    [SerializeField] private float smooth = 10f;

    private Quaternion lastCamRot;
    private Vector3 defaultLocalPos;
    private Quaternion defaultLocalRot;

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        defaultLocalPos = transform.localPosition;
        defaultLocalRot = transform.localRotation;

        if (cameraTransform != null)
            lastCamRot = cameraTransform.rotation;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Rotation delta since last frame
        Quaternion delta = cameraTransform.rotation * Quaternion.Inverse(lastCamRot);
        lastCamRot = cameraTransform.rotation;

        Vector3 deltaEuler = delta.eulerAngles;
        deltaEuler.x = NormalizeAngle(deltaEuler.x);
        deltaEuler.y = NormalizeAngle(deltaEuler.y);

        // Convert look delta into sway
        float swayX = Mathf.Clamp(-deltaEuler.y * posSwayAmount, -posMax, posMax);
        float swayY = Mathf.Clamp(deltaEuler.x * posSwayAmount, -posMax, posMax);

        float rotX = Mathf.Clamp(deltaEuler.x * rotSwayAmount, -rotMax, rotMax);
        float rotY = Mathf.Clamp(deltaEuler.y * rotSwayAmount, -rotMax, rotMax);

        Vector3 targetPos = defaultLocalPos + new Vector3(swayX, swayY, 0f);
        Quaternion targetRot = defaultLocalRot * Quaternion.Euler(rotX, rotY, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smooth);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}