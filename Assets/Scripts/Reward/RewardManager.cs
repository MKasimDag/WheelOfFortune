using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Spin;

namespace WheelOfFortune.Reward
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardCacheData _rewardData;
        [SerializeField] private PersistentInventoryData _persistentInventory;
        [SerializeField] private RewardCatalog _catalog;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _saveSystem = new SaveSystem();
            _saveSystem.Load(_persistentInventory, _catalog);
            Debug.Log($"Load sonrası gold: {_persistentInventory.GetCount("UI_icon_gold")}");
            GameEventBus.Publish(new RewardChangedEvent());
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Subscribe<RestartConfirmedEvent>(OnRestartConfirmed);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Unsubscribe<RestartConfirmedEvent>(OnRestartConfirmed);
        }

        private void OnRewardWon(RewardWonEvent e) => Add(e.RewardId, e.Amount, e.Icon);
        private void OnRestartConfirmed(RestartConfirmedEvent e) => ResetProgress();

        public void Add(string id, int amount, Sprite icon)
        {
            _rewardData.Add(id, amount, icon);
            GameEventBus.Publish(new RewardChangedEvent());
        }

        public void ResetProgress()
        {
            _rewardData.Reset();
            GameEventBus.Publish(new RewardChangedEvent());
        }

        public void Leave()
        {
            var snapshot = new Dictionary<string, RewardCacheData.Entry>(_rewardData.Entries);
            GameEventBus.Publish(new LeaveConfirmedEvent(snapshot));
            _persistentInventory.MergeFrom(snapshot);
            _saveSystem.Save(_persistentInventory); 
            ResetProgress();
        }

        public bool TryRevive(int price)
        {
            if (!_persistentInventory.TrySpendGold(price))
                return false;

            _saveSystem.Save(_persistentInventory);
            GameEventBus.Publish(new RewardChangedEvent());
            return true;
        }

        public int GetPersistentGold() => _persistentInventory.GetCount("UI_icon_gold");

        private void OnDestroy()
        {
            _rewardData.Reset();
        }
    }
}