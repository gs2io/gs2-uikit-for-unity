using System;
using System.Collections;
using Google;
using Gs2.Core.Exception;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/SignOut/Action/Google")]
    public partial class Gs2AccountSignOutGoogleAccount : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return null;
            try
            {
                GoogleSignIn.DefaultInstance.SignOut();
                onSignoutComplete.Invoke();
            }
            catch (Gs2Exception e)
            {
                onError.Invoke(e, null);
            }
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }
    
    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountSignOutGoogleAccount
    {
        [Serializable]
        private class SignOutCompleteEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private SignOutCompleteEvent onSignoutComplete = new SignOutCompleteEvent();
        
        public event UnityAction OnSignOutComplete
        {
            add => onSignoutComplete.AddListener(value);
            remove => onSignoutComplete.RemoveListener(value);
        }
        
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}