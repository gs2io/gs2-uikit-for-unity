using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Reward
{
    public class AcquireAction
    {
        public string action;
        public string request;

        public AcquireAction(
            string action,
            string request
        )
        {
            this.action = action;
            this.request = request;
        }
    }
}