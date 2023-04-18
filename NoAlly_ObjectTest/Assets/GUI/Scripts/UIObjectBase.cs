using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectBase : MonoBehaviour
{
    [SerializeField, Header("オブジェクトのImage")]
    Image[] _objectImage = null;
    [SerializeField, Header("オブジェクトのAnimator")]
    protected Animator _objectAnimator = null;

    protected bool _isActive = false;

    public Animator ObjectAnimator => _objectAnimator;

    public bool IsActive => _isActive;

    /// <summary>
    /// オブジェクトの表示
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
