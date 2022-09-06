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
        private AcquireItemSetByUserIdRequest _request;
        
        public void Start()
        {
            var acquireActionHolders = GetComponentsInParent<AcquireActionHolder>()
                .Where(v => v.action == "Gs2Inventory:AcquireItemSetByUserId")
                .ToArray();
            if (acquireActionHolders.Length > 1)
            {
                Debug.LogError("duplicate acquire action");
            }

            if (acquireActionHolders.Length > 0)
            {
                var acquireActionHolder = acquireActionHolders.First();
                _request = AcquireItemSetByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolder.request));
            }
        }

        public void Update()
        {
            if (_request == null)
            {
                onUpdate.Invoke("");
            }
            else
            {
                onUpdate.Invoke(format.Replace(
                    "{namespaceName}", _request.NamespaceName
                ).Replace(
                    "{inventoryName}", _request.InventoryName.ToString()
                ).Replace(
                    "{itemName}", _request.ItemName.ToString()
                ).Replace(
                    "{acquireCount}", $"{_request.AcquireCount:#,0}"
                ));
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