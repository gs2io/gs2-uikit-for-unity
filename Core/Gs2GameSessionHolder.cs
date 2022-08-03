using UnityEngine;
using System;

namespace Gs2.Unity.UiKit.Core
{
    [Obsolete]
    public class Gs2GameSessionHolder : Util.Gs2GameSessionHolder
    {
        public static Gs2GameSessionHolder Instance
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
    }
}