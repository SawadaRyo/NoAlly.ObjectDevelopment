using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : UIObjectBase
{
    [SerializeField, Header("�؂�ւ��摜")]
    protected Image _switchImage = null;

    public Sprite SwitchImage => _switchImage.sprite;
}


