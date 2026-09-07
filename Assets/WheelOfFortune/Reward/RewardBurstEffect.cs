using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;

namespace WheelOfFortune.Reward
{
    public class RewardBurstEffect : MonoBehaviour
    {
        [SerializeField] private Image _particlePrefab;
        [SerializeField] private Transform _particleContainer;
        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private RectTransform _targetPoint;

        [SerializeField] private int _particleCount = 4;
        [SerializeField] private float _burstRadius = 80f;
        [SerializeField] private float _burstDuration = 0.25f;
        [SerializeField] private float _flyDuration = 0.5f;
        [SerializeField] private float _staggerDelay = 0.05f;

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardWonEvent>(OnRewardWon);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardWonEvent>(OnRewardWon);
        }

        private void OnRewardWon(RewardWonEvent e)
        {
            if (e.Icon == null) return;

            for (int i = 0; i < _particleCount; i++)
                SpawnParticle(e.Icon, i);
        }

        private void SpawnParticle(Sprite icon, int index)
        {
            var particle = Instantiate(_particlePrefab, _particleContainer);
            particle.sprite = icon;

            var rect = particle.rectTransform;
            rect.position = _spawnPoint.position;

            float angle = (360f / _particleCount * index) + Random.Range(-15f, 15f);
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 burstPos = _spawnPoint.position + (Vector3)(dir * _burstRadius);

            var sequence = DOTween.Sequence();
            sequence.SetDelay(index * _staggerDelay);
            sequence.Append(rect.DOMove(burstPos, _burstDuration).SetEase(Ease.OutBack));
            sequence.Append(rect.DOMove(_targetPoint.position, _flyDuration).SetEase(Ease.InQuad));
            sequence.Join(rect.DOScale(0.3f, _flyDuration));
            sequence.OnComplete(() => Destroy(particle.gameObject));
        }
    }
}