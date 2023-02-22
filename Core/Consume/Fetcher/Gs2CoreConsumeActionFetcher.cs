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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Core.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/ConsumeAction/Fetcher/Gs2CoreConsumeActionFetcher")]
    public partial class Gs2CoreConsumeActionFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized &&
                    _clientHolder != null && _clientHolder.Initialized &&
                    _context != null && this._context.ConsumeActions != null) {
                    
                    ConsumeAction = this._context.ConsumeActions.FirstOrDefault(v => v.action == action);
                    Fetched = true;
                    yield return new WaitForSeconds(0.1f);
                }
                else {
                    yield return new WaitForSeconds(1);
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreConsumeActionFetcher
    {
        protected Gs2ClientHolder _clientHolder;
        protected Gs2GameSessionHolder _gameSessionHolder;
        private Gs2CoreConsumeActionsContext _context;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _context = GetComponentInParent<Gs2CoreConsumeActionsContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreConsumeActionFetcher
    {
        public ConsumeAction ConsumeAction { get; protected internal set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreConsumeActionFetcher
    {
        public string action;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreConsumeActionFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}