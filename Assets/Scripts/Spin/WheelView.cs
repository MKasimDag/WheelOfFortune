using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Transform _wheelTransform;
        [SerializeField] private SliceView _slicePrefab;
        [SerializeField] private Transform _sliceContainer;
        [SerializeField] private float _sliceRadius = 150f;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private int _fullTurns = 5;

        private readonly List<SliceView> _spawnedSlices = new();
        private bool _isSpinning;

        public bool IsSpinning => _isSpinning;

        public void SetSlices(List<SliceData> slices)
        {
            EnsureSliceCount(slices.Count);

            float anglePerSlice = 360f / slices.Count;
            for (int i = 0; i < slices.Count; i++)
            {
                float angleRad = i * anglePerSlice * Mathf.Deg2Rad;
                var localPos = new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad)) * _sliceRadius;

                _spawnedSlices[i].transform.localPosition = localPos;
                _spawnedSlices[i].SetData(slices[i]);
                _spawnedSlices[i].gameObject.SetActive(true);
            }

            for (int i = slices.Count; i < _spawnedSlices.Count; i++)
                _spawnedSlices[i].gameObject.SetActive(false);
        }

        public void SpinTo(int targetSliceIndex, int sliceCount, Action onComplete)
        {
            if (_isSpinning) return;
            _isSpinning = true;

            float anglePerSlice = 360f / sliceCount;
            float targetAngle = (_fullTurns * 360f) + (targetSliceIndex * anglePerSlice);

            _wheelTransform.DOLocalRotate(new Vector3(0, 0, -targetAngle), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    _isSpinning = false;
                    onComplete?.Invoke();
                });
        }

        private void EnsureSliceCount(int count)
        {
            while (_spawnedSlices.Count < count)
            {
                var slice = Instantiate(_slicePrefab, _sliceContainer);
                _spawnedSlices.Add(slice);
            }
        }
    }
}