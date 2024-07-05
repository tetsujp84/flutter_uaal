using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
#if UNITY_IOS
using UnityEngine.XR.ARKit;
#endif
using VRM;

// 参考：https://qiita.com/amachi0/items/d830bd89a18b78745b84
public class IOSBlendShape : MonoBehaviour
{
#if UNITY_IOS
    [SerializeField] private ARFaceManager faceManager;
#endif
    private VRMBlendShapeProxy blendShapeProxy;

    public void Initialize(VRMBlendShapeProxy proxy)
    {
        blendShapeProxy = proxy;
    }

    public void UpdateBlendShape(ARFace arFace)
    {
#if UNITY_IOS
        var faceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
        // 瞬きと口の開き具合を取得して、アバターに反映
        var blendShapesVrm = new Dictionary<BlendShapeKey, float>();
        using var blendShapesARKit = faceSubsystem.GetBlendShapeCoefficients(arFace.trackableId, Allocator.Temp);

        foreach (var featureCoefficient in blendShapesARKit)
        {
            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.EyeBlinkLeft)
            {
                blendShapesVrm.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_L), featureCoefficient.coefficient);
            }

            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.EyeBlinkRight)
            {
                blendShapesVrm.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_R), featureCoefficient.coefficient);
            }

            if (featureCoefficient.blendShapeLocation == ARKitBlendShapeLocation.JawOpen)
            {
                blendShapesVrm.Add(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), featureCoefficient.coefficient);
            }
        }

        blendShapeProxy.SetValues(blendShapesVrm);
#endif
    }
}