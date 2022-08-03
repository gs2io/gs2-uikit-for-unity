using System.Linq;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Consume
{
    [AddComponentMenu("GS2 UIKit/Core/ConsumeActionEnabler")]
    public partial class ConsumeActionEnabler : MonoBehaviour
    {
        public void Update()
        {
            var active = false;
            foreach (var consumeActionHolder in _consumeActionHolders)
            {
                switch (consumeActionHolder.action)
                {
                    case "Gs2Enhance:DeleteProgressByUserId":
                        if (gs2EnhanceDeleteProgressByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Exchange:DeleteAwaitByUserId":
                        if (gs2ExchangeDeleteAwaitByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inbox:OpenMessageByUserId":
                        if (gs2InboxOpenMessageByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:ConsumeItemSetByUserId":
                        if (gs2InventoryConsumeItemSetByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:VerifyReferenceOfByUserId":
                        if (gs2InventoryVerifyReferenceOfByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Limit:CountUpByUserId":
                        if (gs2LimitCountUpByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Mission:ReceiveByUserId":
                        if (gs2MissionReceiveByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Money:WithdrawByUserId":
                        if (gs2MoneyWithdrawByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Money:RecordReceipt":
                        if (gs2MoneyRecordReceipt)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Quest:DeleteProgressByUserId":
                        if (gs2QuestDeleteProgressByUserId)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:ConsumeStaminaByUserId":
                        if (gs2StaminaConsumeStaminaByUserId)
                        {
                            active = true;
                        }
                        break;
                }
            }
            target.SetActive(active);
        }
    }
    
    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class ConsumeActionEnabler
    {
        private ConsumeActionHolder[] _consumeActionHolders;

        public void Awake()
        {
            _consumeActionHolders = GetComponents<ConsumeActionHolder>().Concat(GetComponentsInParent<ConsumeActionHolder>()).ToArray();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class ConsumeActionEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class ConsumeActionEnabler
    {
        public bool gs2EnhanceDeleteProgressByUserId;
        public bool gs2ExchangeDeleteAwaitByUserId;
        public bool gs2InboxOpenMessageByUserId;
        public bool gs2InventoryConsumeItemSetByUserId;
        public bool gs2InventoryVerifyReferenceOfByUserId;
        public bool gs2LimitCountUpByUserId;
        public bool gs2MissionReceiveByUserId;
        public bool gs2MoneyWithdrawByUserId;
        public bool gs2MoneyRecordReceipt;
        public bool gs2QuestDeleteProgressByUserId;
        public bool gs2StaminaConsumeStaminaByUserId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class ConsumeActionEnabler
    {
        
    }
}