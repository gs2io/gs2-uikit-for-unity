using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Core.Reward;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core
{
    public class StampSheetActionFetcher : MonoBehaviour
    {
        public ConsumeAction[] ConsumeActions;
        public AcquireAction[] AcquireActions;

        public bool Fetched => ConsumeActions != null || AcquireActions != null;
    }
}