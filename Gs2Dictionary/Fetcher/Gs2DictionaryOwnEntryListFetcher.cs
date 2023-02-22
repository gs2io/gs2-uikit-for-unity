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
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/Entry/Fetcher/Gs2DictionaryOwnEntryListFetcher")]
    public partial class Gs2DictionaryOwnEntryListFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            Gs2Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized && 
                    _clientHolder != null && _clientHolder.Initialized &&
                    _context != null)
                {
                    
                    var domain = this._clientHolder.Gs2.Dictionary.Namespace(
                        this._context.Namespace.NamespaceName
                    ).Me(
                        this._gameSessionHolder.GameSession
                    );
                    var it = domain.Entries();
                    var items = new List<Gs2.Unity.Gs2Dictionary.Model.EzEntry>();
                    while (it.HasNext())
                    {
                        yield return it.Next();
                        if (it.Error != null)
                        {
                            if (it.Error is BadRequestException || it.Error is NotFoundException)
                            {
                                onError.Invoke(e = it.Error, null);
                            }
                            else {
                                onError.Invoke(new CanIgnoreException(it.Error), null);
                            }
                            yield return new WaitForSeconds(retryWaitSecond);
                            retryWaitSecond *= 2;
                        }
                        else {
                            if (it.Current != null)
                            {
                                items.Add(it.Current);
                            } else {
                                break;
                            }
                        }
                    }

                    retryWaitSecond = 1;
                    Entries = items;
                    Fetched = true;
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
    
    public partial class Gs2DictionaryOwnEntryListFetcher
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2DictionaryNamespaceContext _context;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _context = GetComponentInParent<Gs2DictionaryNamespaceContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryNamespaceContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DictionaryOwnEntryListFetcher
    {
        public List<Gs2.Unity.Gs2Dictionary.Model.EzEntry> Entries { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryOwnEntryListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryOwnEntryListFetcher
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