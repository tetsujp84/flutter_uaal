using UnityEngine;
using VRM;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceTracking : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] GameObject avatar;
    [SerializeField] private IOSBlendShape iosBlendShape;

    private Transform neck;
    private Quaternion lateRotation;

    private void Start()
    {
        // 必要な首の関節のTransformを取得
        var animator = avatar.GetComponent<Animator>();
        neck = animator.GetBoneTransform(HumanBodyBones.Neck);

        var blendShapeProxy = avatar.GetComponent<VRMBlendShapeProxy>();
        iosBlendShape.Initialize(blendShapeProxy);
    }

    private void OnEnable()
    {
        faceManager.facesChanged += OnFaceChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFaceChanged;
    }

    private void OnFaceChanged(ARFacesChangedEventArgs eventArgs)
    {
        if (eventArgs.updated.Count != 0)
        {
            var arFace = eventArgs.updated[0];
            if (arFace.trackingState == TrackingState.Tracking && (ARSession.state > ARSessionState.Ready))
            {
                UpdateAvatarPosition(arFace);
                iosBlendShape.UpdateBlendShape(arFace);
            }
        }
    }

    private void UpdateAvatarPosition(ARFace arFace)
    {
        var eulerAngles = arFace.transform.rotation.eulerAngles;

        // オイラー角で回転を補正
        // NOTE:iOSとAndroidで回転が異なる？
        eulerAngles.x = -eulerAngles.x;
        eulerAngles.y = -eulerAngles.y;
        // VRMアバターの回転を更新
        lateRotation = Quaternion.Euler(eulerAngles);
    }

    private void LateUpdate()
    {
        neck.localRotation = lateRotation;
    }
}