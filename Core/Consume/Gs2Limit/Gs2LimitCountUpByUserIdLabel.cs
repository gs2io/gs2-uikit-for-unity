using System;
using System.Linq;
using Gs2.Gs2Limit.Request;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Limit
{
    [AddComponentMenu("GS2 UIKit/Core/Consume/Limit/Gs2LimitCountUpByUserIdLabel")]
    public partial class Gs2LimitCountUpByUserIdLabel : MonoBehaviour
    {
        private StampSheetActionFetcher _fetcher;

        private CountUpByUserIdRequest _request;
        private EzCounter _counter;
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
                            _request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                        }
                    }

                    if (_request != null)
                    {
                        _counter = await Gs2.Unity.Util.Gs2ClientHolder.Instance.Gs2.Limit.Namespace(
                            _request.NamespaceName
                        ).Me(
                            Gs2.Unity.Util.Gs2GameSessionHolder.Instance.GameSession
                        ).Counter(
                            _request.LimitName,
                            _request.CounterName
                        ).ModelAsync();
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
        
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                var acquireActionHolders = GetComponentInParent<StampSheetActionFetcher>()
                    .ConsumeActions
                    .Where(v => v.action == "Gs2Limit:CountUpByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    _request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", _request.NamespaceName
                    ).Replace(
                        "{limitName}", _request.LimitName.ToString()
                    ).Replace(
                        "{counterName}", _request.CounterName.ToString()
                    ).Replace(
                        "{countUpValue}", _request.CountUpValue.ToString()
                    ).Replace(
                        "{maxValue}", _request.MaxValue.ToString()
                    ).Replace(
                        "{remainValue}", _counter == null ? "" : (_request.MaxValue - _counter.Count).ToString()
                    ));
                }
            }
        }
        
        public void OnDestroy()
        {
            _exit = true;
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2LimitCountUpByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountUpByUserIdLabel
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