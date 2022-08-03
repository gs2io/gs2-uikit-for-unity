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
        private WithdrawByUserIdRequest _request;
        
        public void Start()
        {
            var acquireActionHolders = GetComponentsInParent<ConsumeActionHolder>()
                .Where(v => v.action == "Gs2Money:WithdrawByUserId")
                .ToArray();
            if (acquireActionHolders.Length > 1)
            {
                Debug.LogError("duplicate acquire action");
            }

            if (acquireActionHolders.Length > 0)
            {
                var acquireActionHolder = acquireActionHolders.First();
                _request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolder.request));
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
                    "{slot}", _request.Slot.ToString()
                ).Replace(
                    "{count}", $"{_request.Count:#,0}"
                ));
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