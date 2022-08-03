using System;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core
{
    [Obsolete]
    public class Gs2ClientHolder : Util.Gs2ClientHolder
    {
        public static Gs2ClientHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    var clientHolders = FindObjectsOfType<Gs2ClientHolder>();
                    if (clientHolders.Length > 0)
                    {
                        return clientHolders[0];
                    }

                    _instance = new GameObject("Gs2ClientHolder").AddComponent<Gs2ClientHolder>();
                }
                return _instance as Gs2ClientHolder;
            }
        }
    }
}