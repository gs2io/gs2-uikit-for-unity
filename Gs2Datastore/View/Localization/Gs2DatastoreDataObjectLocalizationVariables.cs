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
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Datastore.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Datastore/DataObject/View/Localization/Gs2DatastoreDataObjectLocalizationVariables")]
    public partial class Gs2DatastoreDataObjectLocalizationVariables : MonoBehaviour
    {
        private void OnFetched()
        {
            this.target.StringReference["dataObjectId"] = new StringVariable {
                Value = _fetcher?.DataObject?.DataObjectId ?? "",
            };
            this.target.StringReference["name"] = new StringVariable {
                Value = _fetcher?.DataObject?.Name ?? "",
            };
            this.target.StringReference["userId"] = new StringVariable {
                Value = _fetcher?.DataObject?.UserId ?? "",
            };
            this.target.StringReference["scope"] = new StringVariable {
                Value = _fetcher?.DataObject?.Scope ?? "",
            };
            this.target.StringReference["status"] = new StringVariable {
                Value = _fetcher?.DataObject?.Status ?? "",
            };
            this.target.StringReference["generation"] = new StringVariable {
                Value = _fetcher?.DataObject?.Generation ?? "",
            };
            this.target.StringReference["createdAt"] = new LongVariable {
                Value = _fetcher?.DataObject?.CreatedAt ?? 0,
            };
            this.target.StringReference["updatedAt"] = new LongVariable {
                Value = _fetcher?.DataObject?.UpdatedAt ?? 0,
            };
            this.target.enabled = true;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DatastoreDataObjectLocalizationVariables
    {
        private Gs2DatastoreOwnDataObjectFetcher _fetcher;

        public void Awake() {
            this.target.enabled = false;
            this._fetcher = GetComponent<Gs2DatastoreOwnDataObjectFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectFetcher>();

            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreDataObjectFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2DatastoreOwnDataObjectFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectFetcher>(true);
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

    public partial class Gs2DatastoreDataObjectLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreDataObjectLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectLocalizationVariables
    {

    }
}

#endif