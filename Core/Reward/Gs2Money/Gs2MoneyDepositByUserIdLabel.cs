using System;
using System.Linq;
using Gs2.Gs2Money.Request;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Reward.Gs2Stamina
{
    [AddComponentMenu("GS2 UIKit/Core/Reward/Stamina/Gs2MoneyDepositByUserIdLabel")]
    public partial class Gs2MoneyDepositByUserIdLabel : MonoBehaviour
    {
        private DepositByUserIdRequest _request;
        
        public void Start()
        {
            var acquireActionHolders = GetComponentsInParent<AcquireActionHolder>()
                .Where(v => v.action == "Gs2Money:DepositByUserId")
                .ToArray();
            if (acquireActionHolders.Length > 1)
            {
                Debug.LogError("duplicate acquire action");
            }

            if (acquireActionHolders.Length > 0)
            {
                var acquireActionHolder = acquireActionHolders.First();
                _request = DepositByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolder.request));
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
                    "{price}", $"{_request.Price:#,0}"
                ).Replace(
                    "{count}", $"{_request.Count:#,0}"
                ));
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