using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WheelOfFortune.Reward
{
    public class SaveSystem
    {
        private const string FileName = "inventory.json";

        [Serializable]
        private class SaveModel
        {
            public List<string> ids = new();
            public List<int> counts = new();
        }

        private string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        public void Save(PersistentInventoryData inventory)
        {
            var model = new SaveModel();
            foreach (var pair in inventory.Entries)
            {
                model.ids.Add(pair.Key);
                model.counts.Add(pair.Value.Count);
            }

            File.WriteAllText(FilePath, JsonUtility.ToJson(model));
        }

        public void Load(PersistentInventoryData inventory, RewardCatalog catalog)
        {
            inventory.Clear();

            if (!File.Exists(FilePath)) return;

            var model = JsonUtility.FromJson<SaveModel>(File.ReadAllText(FilePath));
            if (model == null) return;

            for (int i = 0; i < model.ids.Count; i++)
            {
                var icon = catalog.GetIcon(model.ids[i]);
                inventory.LoadFromSave(model.ids[i], model.counts[i], icon);
            }
        }
    }
}