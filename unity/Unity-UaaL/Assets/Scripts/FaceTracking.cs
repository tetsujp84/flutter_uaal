using UnityEngine;
using VRM;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceTracking : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] private bool enableUpdate;
    [SerializeField] GameObject avatar;
    [SerializeField] private IOSBlendShape iosBlendShape;

    private Transform neck;
    private Vector3 headOffset;

    private void Start()
    {
        // 必要な首の関節のTransformを取得
        var animator = avatar.GetComponent<Animator>();
        neck = animator.GetBoneTransform(HumanBodyBones.Neck);

        // アバターの原点座標は足元のため、顔の高さを一致させるためにオフセットを取得
        var head = animator.GetBoneTransform(HumanBodyBones.Head);
        headOffset = new Vector3(0f, head.position.y, 0f);

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

    private Vector3 latePosition;
    private Quaternion lateRotation;

    private void UpdateAvatarPosition(ARFace arFace)
    {
        // アバターの位置と顔の向きを更新
        //latePosition = arFace.transform.position - headOffset;
        
        // lateRotationを更新、前後の傾きが異なるのでそこは逆にする
        var eulerAngles = arFace.transform.rotation.eulerAngles;

        // オイラー角で回転を補正
        // ピッチ（x）軸を反転
        eulerAngles.x = -eulerAngles.x;
        eulerAngles.y = -eulerAngles.y;
        // VRMアバターの回転を更新
        lateRotation = Quaternion.Euler(eulerAngles);

        //lateRotation = Quaternion.Inverse(arFace.transform.rotation);
    }

    private void LateUpdate()
    {
        // アバターの位置と顔の向きを更新
        //avatar.transform.position = latePosition;
        neck.localRotation = lateRotation;
    }
}