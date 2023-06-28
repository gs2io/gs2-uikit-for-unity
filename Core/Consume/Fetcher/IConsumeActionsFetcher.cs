using System.Collections.Generic;
using Gs2.Unity.Core.Model;

namespace Gs2.Unity.UiKit.Gs2Core.Fetcher
{
    public interface IConsumeActionsFetcher
    {
        List<EzConsumeAction> ConsumeActions(string context = "default");
    }
}
