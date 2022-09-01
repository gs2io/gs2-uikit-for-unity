using System.Linq;
using Gs2.Gs2Money.Request;
using Gs2.Util.LitJson;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Money
{
    [AddComponentMenu("GS2 UIKit/Core/Consume/Money/Gs2MoneyWithdrawByUserIdParameterEnabler")]
    public partial class Gs2MoneyWithdrawByUserIdParameterEnabler : MonoBehaviour
    {
        private WithdrawByUserIdRequest _request;
        
        public void Start()
        {
            var consumeActionHolders = GetComponentsInParent<ConsumeActionHolder>()
                .Where(v => v.action == "Gs2Money:WithdrawByUserId")
                .ToArray();
            if (consumeActionHolders.Length > 1)
            {
                Debug.LogError("duplicate consume action");
            }

            if (consumeActionHolders.Length > 0)
            {
                var consumeActionHolder = consumeActionHolders.First();
                _request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(consumeActionHolder.request));
            }
        }

        public void Update()
        {
            if (_request == null)
            {
                target.SetActive(notMoney);
            }
            else
            {
                if (_request.PaidOnly.HasValue && _request.PaidOnly.Value)
                {
                    target.SetActive(paidOnly);
                }
                else
                {
                    target.SetActive(notPaidOnly);
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MoneyWithdrawByUserIdParameterEnabler
    {
        public bool notMoney;
        public bool paidOnly;
        public bool notPaidOnly;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdrawByUserIdParameterEnabler
    {
        
    }
}