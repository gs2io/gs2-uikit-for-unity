using System;
using System.Linq;
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif

namespace Gs2.Unity.UiKit.Core.Consume.Gs2Stamina
{
    [AddComponentMenu("GS2 UIKit/Core/Consume/Stamina/Gs2StaminaConsumeStaminaByUserIdLabel")]
    public partial class Gs2StaminaConsumeStaminaByUserIdLabel : MonoBehaviour
    {
        private StampSheetActionFetcher _fetcher;

        private ConsumeStaminaByUserIdRequest _request;
        private EzStamina _stamina;
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
                            .Where(v => v.action == "Gs2Stamina:ConsumeStaminaByUserId")
                            .ToArray();
                        if (acquireActionHolders.Length > 0)
                        {
                            _request = ConsumeStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                        }
                    }

                    if (_request != null)
                    {
                        _stamina = await Gs2.Unity.Util.Gs2ClientHolder.Instance.Gs2.Stamina.Namespace(
                            _request.NamespaceName
                        ).Me(
                            Gs2.Unity.Util.Gs2GameSessionHolder.Instance.GameSession
                        ).Stamina(
                            _request.StaminaName
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
                    var future = Gs2ClientHolder.Instance.Gs2.Stamina.Namespace(
                        _request.NamespaceName
                    ).Me(
                        Gs2GameSessionHolder.Instance.GameSession
                    ).Stamina(
                        _request.StaminaName
                    ).Model();
                    yield return future;
                    _stamina = future.Result;
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
                    .Where(v => v.action == "Gs2Stamina:ConsumeStaminaByUserId")
                    .ToArray();
                if (acquireActionHolders.Length > 0)
                {
                    _request = ConsumeStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(acquireActionHolders.First().request));
                    onUpdate.Invoke(format.Replace(
                        "{namespaceName}", _request.NamespaceName
                    ).Replace(
                        "{limitName}", _request.StaminaName.ToString()
                    ).Replace(
                        "{staminaName}", _request.StaminaName.ToString()
                    ).Replace(
                        "{consumeValue}", _request.ConsumeValue.ToString()
                    ).Replace(
                        "{afterValue}", _stamina == null ? "" : (_stamina.Value - _request.ConsumeValue).ToString()
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
    
    public partial class Gs2StaminaConsumeStaminaByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaConsumeStaminaByUserIdLabel
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