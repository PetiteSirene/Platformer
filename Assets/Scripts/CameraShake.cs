using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    // Camera Information
    public Transform cameraTransform;
    private Vector3 orignalCameraPos;

    // Shake Parameters
    public float shakeDuration = 0.5f;
    public float shakeAmount = 10f;

    private bool canShake = false;
    private float _shakeTimer;

    [SerializeField]private GameObject player;
 

    // Start is called before the first frame update
    void Start()
    {
        orignalCameraPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canShake)
        {
            StartCameraShakeEffect();
        }
    }
    
    public void TryDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ShakeCamera();

        }
    }

    public void ShakeCamera()
    {
        canShake = true;
        _shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            transform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            transform.position = orignalCameraPos;
            canShake = false;
        }
    }

}