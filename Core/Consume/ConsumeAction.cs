using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Consume
{
    public class ConsumeAction
    {
        public string action;
        public string request;

        public ConsumeAction(
            string action,
            string request
        )
        {
            this.action = action;
            this.request = request;
        }
    }
}