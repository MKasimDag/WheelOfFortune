using System;
using DG.Tweening;
using UnityEngine;

namespace WheelOfFortune.Spin
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Transform _wheelTransform;
        [SerializeField] private int _sliceCount = 8;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private int _fullTurns = 5;

        private bool _isSpinning;

        public bool IsSpinning => _isSpinning;

        public void SpinTo(int targetSliceIndex, Action onComplete)
        {
            if (_isSpinning) return;
            _isSpinning = true;

            float anglePerSlice = 360f / _sliceCount;
            float targetAngle = (_fullTurns * 360f) + (targetSliceIndex * anglePerSlice);

            _wheelTransform.DOLocalRotate(new Vector3(0, 0, -targetAngle), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    _isSpinning = false;
                    onComplete?.Invoke();
                });
        }
    }
}