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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using Gs2.Gs2SerialKey.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2SerialKey.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SerialKey/SerialKey/View/Label/Transaction/Gs2SerialKeyRevertUseByUserIdLabel")]
    public partial class Gs2SerialKeyRevertUseByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.SerialKey != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{code}",
                            $"{_fetcher.Request.Code}"
                        ).Replace(
                            "{userData:campaignModelName}",
                            $"{_userDataFetcher.SerialKey.CampaignModelName}"
                        ).Replace(
                            "{userData:metadata}",
                            $"{_userDataFetcher.SerialKey.Metadata}"
                        ).Replace(
                            "{userData:code}",
                            $"{_userDataFetcher.SerialKey.Code}"
                        ).Replace(
                            "{userData:status}",
                            $"{_userDataFetcher.SerialKey.Status}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{code}",
                            $"{_fetcher.Request.Code}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2SerialKeyRevertUseByUserIdLabel
    {
        private Gs2SerialKeyRevertUseByUserIdFetcher _fetcher;
        private Gs2SerialKeySerialKeyFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2SerialKeyRevertUseByUserIdFetcher>() ?? GetComponentInParent<Gs2SerialKeyRevertUseByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2SerialKeySerialKeyFetcher>() ?? GetComponentInParent<Gs2SerialKeySerialKeyFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SerialKeyRevertUseByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2SerialKeyRevertUseByUserIdFetcher>() ?? GetComponentInParent<Gs2SerialKeyRevertUseByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2SerialKeySerialKeyFetcher>() ?? GetComponentInParent<Gs2SerialKeySerialKeyFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SerialKeyRevertUseByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SerialKeyRevertUseByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SerialKeyRevertUseByUserIdLabel
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