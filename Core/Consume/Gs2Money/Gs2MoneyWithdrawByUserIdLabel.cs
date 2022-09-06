using System;
using System.Linq;
using Gs2.Gs2Money.Request;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Stamina
{
    [AddComponentMenu("GS2 UIKit/Core/Reward/Stamina/Gs2MoneyWithdrawByUserIdLabel")]
    public partial class Gs2MoneyWithdrawByUserIdLabel : MonoBehaviour
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
                    .ConsumeActions
                    .Where(v => v.action == "Gs2Money:WithdrawByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    var request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", request.NamespaceName
                    ).Replace(
                        "{slot}", request.Slot.ToString()
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
    
    public partial class Gs2MoneyWithdrawByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdrawByUserIdLabel
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