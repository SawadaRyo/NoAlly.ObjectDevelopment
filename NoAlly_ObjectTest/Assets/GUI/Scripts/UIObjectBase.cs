using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectBase : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g��Image")]
    Image[] _objectImage = null;
    [SerializeField, Header("�I�u�W�F�N�g��Animator")]
    protected Animator _objectAnimator = null;

    protected bool _isActive = false;

    public Animator ObjectAnimator => _objectAnimator;

    public bool IsActive => _isActive;

    /// <summary>
    /// �I�u�W�F�N�g�̕\��
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveUIObject(bool isActive)
    {
        if (_objectAnimator != null)
        {
            _objectAnimator.enabled = isActive;
        }
        Array.ForEach(_objectImage, x => x.enabled = isActive);
    }
}
