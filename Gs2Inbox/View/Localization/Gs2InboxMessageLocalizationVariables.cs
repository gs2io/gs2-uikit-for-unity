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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Inbox.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inbox/Message/View/Localization/Gs2InboxMessageLocalizationVariables")]
    public partial class Gs2InboxMessageLocalizationVariables : MonoBehaviour
    {
        private void OnFetched()
        {
            this.target.StringReference["messageId"] = new StringVariable {
                Value = _fetcher?.Message?.MessageId ?? "",
            };
            this.target.StringReference["name"] = new StringVariable {
                Value = _fetcher?.Message?.Name ?? "",
            };
            this.target.StringReference["metadata"] = new StringVariable {
                Value = _fetcher?.Message?.Metadata ?? "",
            };
            this.target.StringReference["isRead"] = new BoolVariable {
                Value = _fetcher?.Message?.IsRead ?? false,
            };
            this.target.enabled = true;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InboxMessageLocalizationVariables
    {
        private Gs2InboxOwnMessageFetcher _fetcher;

        public void Awake() {
            this.target.enabled = false;
            this._fetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>();

            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxOwnMessageFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);

            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxMessageLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InboxMessageLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageLocalizationVariables
    {

    }
}

#endif