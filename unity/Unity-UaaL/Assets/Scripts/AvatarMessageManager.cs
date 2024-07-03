using System.Collections;
using System.Collections.Generic;
using FlutterUnityIntegration;
using UnityEngine;

public class AvatarMessageManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip[] animations;

    private int animationIndex = 0;
    private static readonly int Index = Animator.StringToHash("Index");

    private void Start()
    {
        UpdateAnimation();
    }

    public void NextAnimation()
    {
        animationIndex = (animationIndex + 1) % animations.Length;
        UpdateAnimation();
    }

    public void PreviousAnimation()
    {
        animationIndex = (animationIndex - 1 + animations.Length) % animations.Length;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetInteger(Index, animationIndex);
        SendAnimationName();
    }

    private void SendAnimationName()
    {
        UnityMessageManager.Instance.SendMessageToFlutter($"animation?{animations[animationIndex].name}");
    }
}