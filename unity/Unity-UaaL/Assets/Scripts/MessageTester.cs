using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTester : MonoBehaviour
{
    [SerializeField] private Button preAnimationButton;
    [SerializeField] private Button nextAnimationButton;
    [SerializeField] private AvatarMessageManager avatarMessageManager;
    
    private void Start()
    {
        preAnimationButton.onClick.AddListener(avatarMessageManager.PreviousAnimation);
        nextAnimationButton.onClick.AddListener(avatarMessageManager.NextAnimation);
    }
}
