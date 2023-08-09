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
using Gs2.Gs2Formation.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/PropertyForm/View/Label/Transaction/Gs2FormationAcquireActionsToPropertyFormPropertiesLabel")]
    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.PropertyForm != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{formModelName}",
                            $"{_fetcher.Request.FormModelName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{acquireAction}",
                            $"{_fetcher.Request.AcquireAction}"
                        ).Replace(
                            "{config}",
                            $"{_fetcher.Request.Config}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.PropertyForm.Name}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.PropertyForm.PropertyId}"
                        ).Replace(
                            "{userData:slots}",
                            $"{_userDataFetcher.PropertyForm.Slots}"
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
                            "{formModelName}",
                            $"{_fetcher.Request.FormModelName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{acquireAction}",
                            $"{_fetcher.Request.AcquireAction}"
                        ).Replace(
                            "{config}",
                            $"{_fetcher.Request.Config}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {
        private Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher _fetcher;
        private Gs2FormationOwnPropertyFormFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>() ?? GetComponentInParent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>();
            _userDataFetcher = GetComponent<Gs2FormationOwnPropertyFormFetcher>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>() ?? GetComponentInParent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>(true);
            _userDataFetcher = GetComponent<Gs2FormationOwnPropertyFormFetcher>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
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