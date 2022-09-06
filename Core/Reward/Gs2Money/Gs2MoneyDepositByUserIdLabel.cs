using System;
using System.Linq;
using Gs2.Gs2Money.Request;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Reward.Gs2Money
{
    [AddComponentMenu("GS2 UIKit/Core/Reward/Money/Gs2MoneyDepositByUserIdLabel")]
    public partial class Gs2MoneyDepositByUserIdLabel : MonoBehaviour
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
                    .Where(v => v.action == "Gs2Money:DepositByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    var request = DepositByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", request.NamespaceName
                    ).Replace(
                        "{slot}", request.Slot.ToString()
                    ).Replace(
                        "{price}", $"{request.Price:#,0}"
                    ).Replace(
                        "{count}", $"{request.Count:#,0}"
                    ));
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MoneyDepositByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyDepositByUserIdLabel
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