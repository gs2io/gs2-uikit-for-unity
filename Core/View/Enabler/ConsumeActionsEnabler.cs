/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Core.Model;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Consume.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/View/Enabler/ConsumeActionsEnabler")]
    public partial class ConsumeActionsEnabler : MonoBehaviour
    {
        private bool _satisfy;
        
        public IEnumerator Process() {
            while (true) {
                yield return new WaitForSeconds(0.1f);
                
                if (_clientHolder.Initialized && _sessionHolder.Initialized) {
                    bool satisfy = true;
                    foreach (var consumeAction in _fetcher.ConsumeActions()) {
                        var future = consumeAction.Satisfy(
                            _clientHolder.Gs2,
                            _sessionHolder.GameSession
                        );
                        yield return future;
                        if (future.Error != null) {
                            this.onError.Invoke(new CanIgnoreException(future.Error), null);
                        }
                        if (!future.Result) {
                            satisfy = false;
                            break;
                        }
                    }
                    _satisfy = satisfy;
                }
            }
        }

        public void OnEnable() {
            StartCoroutine(nameof(Process));
        }

        public void Update()
        {
            if (this._satisfy) {
                this.target.SetActive(this.satisfy);
            }
            else {
                this.target.SetActive(this.notSatisfy);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private IConsumeActionsFetcher _fetcher;
        
        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IConsumeActionsFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
            Update();
        }
        
        public bool HasError()
        {
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        public bool notSatisfy;
        public bool satisfy;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class ConsumeActionsEnabler
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}
