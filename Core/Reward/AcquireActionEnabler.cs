using System.Linq;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Reward
{
    [AddComponentMenu("GS2 UIKit/Core/AcquireActionEnabler")]
    public partial class AcquireActionEnabler : MonoBehaviour
    {
        public void Update()
        {
            var active = false;
            foreach (var acquireActionHolder in _acquireActionHolders)
            {
                switch (acquireActionHolder.action)
                {
                    case "Gs2Dictionary:AddEntriesByUserId":
                        if (gs2DictionaryAddEntries)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Enhance:DirectEnhanceByUserId":
                        if (gs2EnhanceDirectEnhance)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Enhance:CreateProgressByUserId":
                        if (gs2EnhanceCreateProgress)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Exchange:ExchangeByUserId":
                        if (gs2ExchangeExchange)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Exchange:CreateAwaitByUserId":
                        if (gs2ExchangeCreateAwait)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Experience:AddExperienceByUserId":
                        if (gs2ExperienceAddExperience)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Experience:AddRankCapByUserId":
                        if (gs2ExperienceAddRankCap)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Experience:SetRankCapByUserId":
                        if (gs2ExperienceSetRankCap)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Formation:AddMoldCapacityByUserId":
                        if (gs2FormationAddMoldCapacity)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Formation:SetMoldCapacityByUserId":
                        if (gs2FormationSetMoldCapacity)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Formation:AcquireActionsToFormProperties":
                        if (gs2FormationAcquireActionsToFormProperties)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inbox:SendMessageByUserId":
                        if (gs2InboxSendMessage)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:AddCapacityByUserId":
                        if (gs2InventoryAddCapacity)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:SetCapacityByUserId":
                        if (gs2InventorySetCapacity)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:AcquireItemSetByUserId":
                        if (gs2InventoryAcquireItemSet)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:AddReferenceOfByUserId":
                        if (gs2InventoryAddReferenceOf)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Inventory:DeleteReferenceOfByUserId":
                        if (gs2InventoryDeleteReferenceOf)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2JobQueue:PushByUserId":
                        if (gs2JobQueuePush)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Limit:DeleteCounterByUserId":
                        if (gs2LimitDeleteCounter)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Lottery:DrawByUserId":
                        if (gs2LotteryDraw)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Mission:IncreaseCounterByUserId":
                        if (gs2MissionIncreaseCounter)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Money:DepositByUserId":
                        if (gs2MoneyDeposit)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Quest:CreateProgressByUserId":
                        if (gs2QuestCreateProgress)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:RecoverStaminaByUserId":
                        if (gs2StaminaRecoverStamina)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:RaiseMaxValueByUserId":
                        if (gs2StaminaRaiseMaxValue)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:SetMaxValueByUserId":
                        if (gs2StaminaSetMaxValue)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:SetRecoverIntervalByUserId":
                        if (gs2StaminaSetRecoverInterval)
                        {
                            active = true;
                        }
                        break;
                    case "Gs2Stamina:SetRecoverValueByUserId":
                        if (gs2StaminaSetRecoverValue)
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
    
    public partial class AcquireActionEnabler
    {
        private AcquireActionHolder[] _acquireActionHolders;

        public void Awake()
        {
            _acquireActionHolders = GetComponents<AcquireActionHolder>().Concat(GetComponentsInParent<AcquireActionHolder>()).ToArray();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class AcquireActionEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class AcquireActionEnabler
    {
        public bool gs2DictionaryAddEntries;
        public bool gs2EnhanceDirectEnhance;
        public bool gs2EnhanceCreateProgress;
        public bool gs2ExchangeExchange;
        public bool gs2ExchangeCreateAwait;
        public bool gs2ExperienceAddExperience;
        public bool gs2ExperienceAddRankCap;
        public bool gs2ExperienceSetRankCap;
        public bool gs2FormationAddMoldCapacity;
        public bool gs2FormationSetMoldCapacity;
        public bool gs2FormationAcquireActionsToFormProperties;
        public bool gs2InboxSendMessage;
        public bool gs2InventoryAddCapacity;
        public bool gs2InventorySetCapacity;
        public bool gs2InventoryAcquireItemSet;
        public bool gs2InventoryAddReferenceOf;
        public bool gs2InventoryDeleteReferenceOf;
        public bool gs2JobQueuePush;
        public bool gs2LimitDeleteCounter;
        public bool gs2LotteryDraw;
        public bool gs2MissionIncreaseCounter;
        public bool gs2MoneyDeposit;
        public bool gs2QuestCreateProgress;
        public bool gs2StaminaRecoverStamina;
        public bool gs2StaminaRaiseMaxValue;
        public bool gs2StaminaSetMaxValue;
        public bool gs2StaminaSetRecoverInterval;
        public bool gs2StaminaSetRecoverValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class AcquireActionEnabler
    {
        
    }
}