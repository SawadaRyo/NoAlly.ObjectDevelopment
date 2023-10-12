using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�̕\����؂�ւ���N���X
/// </summary>
public class ObjectBase : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g��Collider")]
    protected Collider[] _objectCollider = null;
    [SerializeField, Header("�I�u�W�F�N�g��Renderer")]
    protected Renderer[] _objectRenderer = null;
    [SerializeField, Header("�I�u�W�F�N�g��Animator")]
    protected Animator _objectAnimator = null;

    protected bool _isActive = true;

    public Animator ObjectAnimator => _objectAnimator;

    public bool IsActive => _isActive;

    public virtual void ActiveObject(bool stats)
    {
        if (_objectAnimator != null)
        {
            _objectAnimator.enabled = stats;
        }
        Array.ForEach(_objectRenderer, x => x.enabled = stats);
        Array.ForEach(_objectCollider, x => x.enabled = stats);
    }
    public virtual void ActiveCollider(bool stats)
    {
        Array.ForEach(_objectCollider, x => x.enabled = stats);
    }
}
