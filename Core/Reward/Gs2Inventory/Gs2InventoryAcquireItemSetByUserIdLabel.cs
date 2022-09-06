using System;
using System.Linq;
using Gs2.Gs2Inventory.Request;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Reward.Gs2Inventory
{
    [AddComponentMenu("GS2 UIKit/Core/Reward/Inventory/Gs2InventoryAcquireItemSetByUserIdLabel")]
    public partial class Gs2InventoryAcquireItemSetByUserIdLabel : MonoBehaviour
    {
        private StampSheetActionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<StampSheetActionFetcher>();
        }

        public void Update()
        {
            if (_fetcher.Fetched)
            {
                var acquireActionHolders = GetComponentInParent<StampSheetActionFetcher>()
                    .AcquireActions
                    .Where(v => v.action == "Gs2Inventory:AcquireItemSetByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    var request = AcquireItemSetByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", request.NamespaceName
                    ).Replace(
                        "{inventoryName}", request.InventoryName.ToString()
                    ).Replace(
                        "{itemName}", request.ItemName.ToString()
                    ).Replace(
                        "{acquireCount}", $"{request.AcquireCount:#,0}"
                    ));
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();
        
        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}