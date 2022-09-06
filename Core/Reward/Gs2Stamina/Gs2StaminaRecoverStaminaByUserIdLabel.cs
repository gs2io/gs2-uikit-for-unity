using System;
using System.Linq;
using Gs2.Gs2Stamina.Request;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Reward.Gs2Stamina
{
    [AddComponentMenu("GS2 UIKit/Core/Reward/Stamina/Gs2StaminaRecoverStaminaByUserIdLabel")]
    public partial class Gs2StaminaRecoverStaminaByUserIdLabel : MonoBehaviour
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
                    .Where(v => v.action == "Gs2Stamina:RecoverStaminaByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    var request = RecoverStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", request.NamespaceName
                    ).Replace(
                        "{staminaName}", request.StaminaName
                    ).Replace(
                        "{recoverValue}", request.RecoverValue.ToString()
                    ));
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaRecoverStaminaByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaRecoverStaminaByUserIdLabel
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