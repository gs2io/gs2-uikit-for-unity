using System;
using System.Linq;
using Gs2.Gs2Money.Request;
using Gs2.Unity.Gs2Money.Model;
using Gs2.Util.LitJson;
using UnityEngine;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Money
{
    [AddComponentMenu("GS2 UIKit/Core/Consume/Money/Gs2MoneyWithdrawByUserIdEnabler")]
    public partial class Gs2MoneyWithdrawByUserIdEnabler : MonoBehaviour
    {
        private StampSheetActionFetcher _fetcher;

        private WithdrawByUserIdRequest _request;
        private EzWallet _wallet;
        private bool _exit;

        public void Awake()
        {
            _fetcher = GetComponentInParent<StampSheetActionFetcher>();
        }

        public void Start()
        {
            
#if GS2_ENABLE_UNITASK
            async UniTask Fetch()
            {
                while (!_exit)
                {
                    if (_fetcher.Fetched)
                    {
                        var acquireActionHolders = GetComponentInParent<StampSheetActionFetcher>()
                            .ConsumeActions
                            .Where(v => v.action == "Gs2Limit:CountUpByUserId")
                            .ToArray();
                        if (acquireActionHolders.Length > 0)
                        {
                            _request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                        }
                    }

                    if (_request != null)
                    {
                        _wallet = await Gs2.Unity.Util.Gs2ClientHolder.Instance.Gs2.Money.Namespace(
                            _request.NamespaceName
                        ).Me(
                            Gs2.Unity.Util.Gs2GameSessionHolder.Instance.GameSession
                        ).Wallet(
                            _request.Slot.Value
                        ).ModelAsync();
                        await UniTask.Delay(TimeSpan.FromSeconds(1));
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(1));
                }
            }

            StartCoroutine(Fetch().ToCoroutine());
#else
            IEnumerator Fetch()
            {
                while (!_exit)
                {
                    var future = Gs2.Unity.Util.Gs2ClientHolder.Instance.Gs2.Money.Namespace(
                        _request.NamespaceName
                    ).Me(
                        Gs2.Unity.Util.Gs2GameSessionHolder.Instance.GameSession
                    ).Wallet(
                        _request.Slot.Value
                    ).Model();
                    yield return future;
                    _wallet = future.Result;
                    yield return new WaitForSeconds(1);
                }
            }

            StartCoroutine(Fetch());
#endif
        }

        public void OnDestroy()
        {
            _exit = true;
        }

        public void Update()
        {
            if (_request == null)
            {
                target.SetActive(notMoney);
            }
            else if (_wallet == null)
            {
                target.SetActive(fetching);
            }
            else
            {
                if (_request.PaidOnly.HasValue && _request.PaidOnly.Value && _wallet.Paid > _request.Count)
                {
                    target.SetActive(forSale);
                }
                else if ((!_request.PaidOnly.HasValue || !_request.PaidOnly.Value) && _wallet.Free + _wallet.Paid > _request.Count)
                {
                    target.SetActive(forSale);
                }
                else
                {
                    target.SetActive(notForSale);
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MoneyWithdrawByUserIdEnabler
    {
        public bool notMoney;
        public bool fetching;
        public bool forSale;
        public bool notForSale;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdrawByUserIdEnabler
    {
        
    }
}