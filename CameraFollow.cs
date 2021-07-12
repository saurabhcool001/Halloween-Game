using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    internal static CameraFollow Instance;

    Transform target;

    public float smoothSpeed = 0.125f;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    //Vector3 velocity = Vector3.zero;
    public Vector3 focusedPos;
    public Vector3 crushedPos;
    public Vector3 finishPos;
    public bool stopFollow = true;
    public bool isTriggerCrushCam;

    public Vector3 currOffset;

    //private float jumpIter = 9.5f;
    public float shakeAmt =  4f; // the degrees to shake the camera
    public float shakePeriodTime = 0.25f; // The period of each shake
    public float dropOffTime = 0.5f; // How long it takes the shaking to settle down to nothing

    private void Awake()
    {
        Instance = this;
        currOffset = offset;
    }

    private void Start() {
        MakePlayerTarget();
    }

    void FixedUpdate()
    {
        if (!stopFollow && target!= null)
        {
            Vector3 desiredPosition = target.position + currOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }

    internal void MoveToFocusOffset()
    {
        currOffset = Vector3.Lerp(currOffset, focusedPos, 2 * Time.fixedDeltaTime);
    }

    internal void MoveToOriginalOffset()
    {
        currOffset = Vector3.Lerp(currOffset, offset, 2 * Time.fixedDeltaTime);
    }

    [EasyButtons.Button]
    internal void MoveToCrushCamOffset()
    {
        currOffset = Vector3.Lerp(currOffset, crushedPos, 2 * Time.fixedDeltaTime);
    }

    internal void MoveToFinishCamOffset()
    {
        currOffset = Vector3.Lerp(currOffset, finishPos, 2 * Time.fixedDeltaTime);
    }

    [EasyButtons.Button]
    internal void CamShake()
    {
        //float height = Mathf.PerlinNoise(jumpIter, 0f) * 10f;

        LTDescr shakeTween = LeanTween.rotateAroundLocal(transform.parent.gameObject, Vector3.one, shakeAmt, shakePeriodTime/2)
        .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
        .setLoopClamp()
        .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime/2).setOnUpdate(
            (float val) =>
            {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);
    }

    internal void MakePlayerTarget()
    {
        target = Player.Instance.transform;
    }

    internal void MakeBallTarget(Transform b)
    {
        target = b;
    }
}