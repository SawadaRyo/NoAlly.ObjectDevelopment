using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ButtonArrayExtend : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    GridLayoutGroup _gridLayout;
    [SerializeField, Tooltip("")]
    float _tweenTime = 0.2f;
    [SerializeField, Tooltip("")]
    Vecter _tweenVecter = Vecter.None;

    [Tooltip("")]
    Vector2[] _tweenValues = new Vector2[2];

    public void Initialize()
    {
        _tweenValues[0] = _gridLayout.spacing;

        switch (_tweenVecter)
        {
            case Vecter.Horizontal:
                _tweenValues[1] = new Vector2(-_gridLayout.cellSize.x, _gridLayout.spacing.y);
                break;
            case Vecter.Vertical:
                _tweenValues[1] = new Vector2(_gridLayout.spacing.x, -_gridLayout.cellSize.y);
                break;
            default:
                break;
        }
        
    }

    public bool ExtendsButton(bool isExtend)
    {
        bool conpleted = false;
        if (isExtend)
        {
            DOTween.To(() => _gridLayout.spacing,
             n => _gridLayout.spacing = n,
             _tweenValues[0],
             duration: _tweenTime).OnComplete(() => conpleted = true);
        }
        else
        {
            DOTween.To(() => _gridLayout.spacing,
             n => _gridLayout.spacing = n,
             _tweenValues[1],
             duration: _tweenTime).OnComplete(() => conpleted = true);
        }

        return conpleted;
    }

    enum Vecter
    {
        None,
        Horizontal,
        Vertical
    }
}


