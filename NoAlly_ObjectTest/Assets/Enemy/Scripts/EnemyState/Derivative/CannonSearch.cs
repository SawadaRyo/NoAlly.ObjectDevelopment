//日本語コメント可
using DG.Tweening;
using UnityEngine;

public class CannonSearch : EnemySearch
{
    bool _isRotatedLeft;
    float _time = 0;
    float _intervalRotate = 3;
    float _turnDuration = 0.5f;

    protected override void SearchBehaviour()
    {
        base.SearchBehaviour();
    }

    protected void EnemyRotate()
    {
        _time += Time.deltaTime;
        if (_time >= _intervalRotate)
        {
            _isRotatedLeft = !_isRotatedLeft;
            if (_isRotatedLeft)
            {
                Owner.transform.DORotate(new Vector3(0f, 0, 0f), _turnDuration);
            }
            else
            {
                Owner.transform.DORotate(new Vector3(0f, 180f, 0f), _turnDuration);
            }
            _time = 0;
        }
    }
}
