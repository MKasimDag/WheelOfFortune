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
        [SerializeField] private Transform _indicatorTransform;
        [SerializeField] private Transform _sliceContainer;
        [SerializeField] private SliceView _slicePrefab;
        [SerializeField] private float _sliceRadius = 150f;
        [SerializeField] private int _slotCount = 8;
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private int _fullTurns = 5;
        [SerializeField] private float _tickAngle = 12f;
        [SerializeField] private float _tickDuration = 0.06f;
        private readonly List<SliceView> _spawnedSlices = new();
        private bool _isSpinning;
        public bool IsSpinning => _isSpinning;
        private float _lastWheelAngle;
        private float _accumulatedAngle;

        private void Awake()
        {
            SpawnSlices();
        }

        private void SpawnSlices()
        {
            for (int i = 0; i < _slotCount; i++)
            {
                var slice = Instantiate(_slicePrefab, _sliceContainer);
                var rotation = GetSliceRotation(i);

                slice.transform.localPosition = rotation * Vector3.up * _sliceRadius;
                slice.transform.localRotation = rotation;

                _spawnedSlices.Add(slice);
            }
        }

        public void SetSlices(List<SliceData> slices, int zone)
        {
            for (int i = 0; i < _spawnedSlices.Count; i++)
            {
                bool hasData = i < slices.Count;
                _spawnedSlices[i].gameObject.SetActive(hasData);
                if (hasData)
                    _spawnedSlices[i].SetData(slices[i], zone);
            }
        }

        public void SpinTo(int targetSliceIndex, Action onComplete)
        {
            if (_isSpinning) return;
            _isSpinning = true;

            float anglePerSlice = 360f / _slotCount;
            float targetAngle = (_fullTurns * 360f) - (targetSliceIndex * anglePerSlice);

            _lastWheelAngle = _wheelTransform.localEulerAngles.z;
            _accumulatedAngle = 0f; 

            _wheelTransform.DOLocalRotate(new Vector3(0, 0, -targetAngle), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnUpdate(OnWheelRotationUpdate) 
                .OnComplete(() =>
                {
                    _isSpinning = false;
                    onComplete?.Invoke();
                });
        }

        private Quaternion GetSliceRotation(int index)
        {
            float angle = index * (360f / _slotCount);
            return Quaternion.Euler(0, 0, -angle);
        }

        private void OnWheelRotationUpdate()
        {
            float currentAngle = _wheelTransform.localEulerAngles.z;
            float delta = Mathf.Abs(Mathf.DeltaAngle(_lastWheelAngle, currentAngle));
            _lastWheelAngle = currentAngle;

            _accumulatedAngle += delta;
            float anglePerSlice = 360f / _slotCount;

            while (_accumulatedAngle >= anglePerSlice)
            {
                _accumulatedAngle -= anglePerSlice;
                TickIndicator();
            }
        }

        private void TickIndicator()
        {
            _indicatorTransform.DOKill();
            _indicatorTransform.localRotation = Quaternion.identity;
            _indicatorTransform.DOPunchRotation(new Vector3(0, 0, _tickAngle), _tickDuration, 1, 0.5f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_wheelTransform == null) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_wheelTransform.position, 8f);

            Gizmos.color = Color.cyan;
            for (int i = 0; i < _slotCount; i++)
            {
                var rotation = GetSliceRotation(i);
                Vector3 worldPos = _wheelTransform.TransformPoint(rotation * Vector3.up * _sliceRadius);
                Gizmos.DrawSphere(worldPos, 6f);
                Gizmos.DrawLine(_wheelTransform.position, worldPos);
            }
        }
#endif
    }
}