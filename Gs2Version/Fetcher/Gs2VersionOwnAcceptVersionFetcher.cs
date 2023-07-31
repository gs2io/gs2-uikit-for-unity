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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using System.Collections;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Version.Model;
using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Version.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Version.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/AcceptVersion/Fetcher/Gs2VersionOwnAcceptVersionFetcher")]
    public partial class Gs2VersionOwnAcceptVersionFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            Gs2Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized &&
                    _clientHolder != null && _clientHolder.Initialized &&
                    Context != null)
                {
                    
                    var domain = this._clientHolder.Gs2.Version.Namespace(
                        this.Context.AcceptVersion.NamespaceName
                    ).Me(
                        this._gameSessionHolder.GameSession
                    ).AcceptVersion(
                        this.Context.AcceptVersion.VersionName
                    );
                    var future = domain.Model();
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is BadRequestException || future.Error is NotFoundException)
                        {
                            onError.Invoke(e = future.Error, null);
                            break;
                        }

                        onError.Invoke(new CanIgnoreException(future.Error), null);
                    }
                    else
                    {
                        AcceptVersion = future.Result;
                        Fetched = true;
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }

            var transform1 = transform;
            var builder = new StringBuilder(transform1.name);
            var current = transform1.parent;

            while (current != null)
            {
                builder.Insert(0, current.name + "/");
                current = current.parent;
            }

            Debug.LogError(e);
            Debug.LogError($"{GetType()} の自動更新が停止されました。 {builder}");
            Debug.LogError($"Automatic update of {GetType()} has been stopped. {builder}");
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

    public partial class Gs2VersionOwnAcceptVersionFetcher
    {
        protected Gs2ClientHolder _clientHolder;
        protected Gs2GameSessionHolder _gameSessionHolder;
        public Gs2VersionOwnAcceptVersionContext Context { get; private set; }

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            Context = GetComponent<Gs2VersionOwnAcceptVersionContext>() ?? GetComponentInParent<Gs2VersionOwnAcceptVersionContext>();

            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2VersionOwnAcceptVersionContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            Context = GetComponent<Gs2VersionOwnAcceptVersionContext>() ?? GetComponentInParent<Gs2VersionOwnAcceptVersionContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionOwnAcceptVersionFetcher
    {
        public EzAcceptVersion AcceptVersion { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionOwnAcceptVersionFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionOwnAcceptVersionFetcher
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