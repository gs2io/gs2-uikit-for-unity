using System;
using System.Collections;
using System.Linq;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Gs2Limit.Request;
using Gs2.Gs2Money.Request;
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Limit
{
    [AddComponentMenu("GS2 UIKit/Core/Consume/Limit/Gs2LimitCountUpByUserIdEnabler")]
    public partial class Gs2LimitCountUpByUserIdEnabler : MonoBehaviour
    {
        private CountUpByUserIdRequest _request;
        private EzCounter _counter;
        private bool _exit;
        
        public void Start()
        {
            var consumeActionHolders = GetComponentsInParent<ConsumeActionHolder>()
                .Where(v => v.action == "Gs2Limit:CountUpByUserId")
                .ToArray();
            if (consumeActionHolders.Length > 1)
            {
                Debug.LogError("duplicate consume action");
            }

            if (consumeActionHolders.Length > 0)
            {
                var consumeActionHolder = consumeActionHolders.First();
                _request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(consumeActionHolder.request));
#if GS2_ENABLE_UNITASK
                async UniTask Fetch()
                {
                    while (!_exit)
                    {
                        _counter = await Gs2ClientHolder.Instance.Gs2.Limit.Namespace(
                            _request.NamespaceName
                        ).Me(
                            Gs2GameSessionHolder.Instance.GameSession
                        ).Counter(
                            _request.LimitName,
                            _request.CounterName
                        ).ModelAsync();
                        await UniTask.Delay(TimeSpan.FromSeconds(1));
                    }
                }

                StartCoroutine(Fetch().ToCoroutine());
#else
                IEnumerator Fetch()
                {
                    while (!_exit)
                    {
                        var future = Gs2ClientHolder.Instance.Gs2.Limit.Namespace(
                            _request.NamespaceName
                        ).Me(
                            Gs2GameSessionHolder.Instance.GameSession
                        ).Counter(
                            _request.LimitName,
                            _request.CounterName
                        ).Model();
                        yield return future;
                        _counter = future.Result;
                        yield return new WaitForSeconds(1);
                    }
                }

                StartCoroutine(Fetch());
#endif
            }
        }

        public void OnDestroy()
        {
            _exit = true;
        }

        public void Update()
        {
            if (_request == null)
            {
                target.SetActive(notLimit);
            }
            else if (_counter == null)
            {
                target.SetActive(fetching);
            }
            else
            {
                if (_request.MaxValue - _counter.Count == 0)
                {
                    target.SetActive(reachMax);
                }
                else
                {
                    target.SetActive(notReachMax);
                }
            }
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2LimitCountUpByUserIdEnabler
    {
        public bool notLimit;
        public bool fetching;
        public bool notReachMax;
        public bool reachMax;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountUpByUserIdEnabler
    {
        
    }
}