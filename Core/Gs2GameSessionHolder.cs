using System;
using System.Collections;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core
{
    [Obsolete]
    public class Gs2GameSessionHolder : Util.Gs2GameSessionHolder
    {
        public new static Gs2GameSessionHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    var clientHolders = FindObjectsOfType<Gs2GameSessionHolder>();
                    if (clientHolders.Length > 0)
                    {
                        return clientHolders[0];
                    }

                    _instance = new GameObject("Gs2GameSessionHolder").AddComponent<Gs2GameSessionHolder>();
                }
                return _instance as Gs2GameSessionHolder;
            }
        }

        public void Start() {
            IEnumerator Impl() {
                while (true) {
                    if (_instance != null && _instance.Initialized) {
#if GS2_ENABLE_UNITASK
                        yield return Util.Gs2ClientHolder.Instance.Gs2.DispatchAsync(
                            _instance.GameSession
                        );
#else
                        yield return Util.Gs2ClientHolder.Instance.Gs2.Dispatch(
                            _instance.GameSession
                        );
#endif
                    }
                
                    yield return new WaitForSeconds(1);
                }
            }

            StartCoroutine(nameof(Impl));
        }
    }
}