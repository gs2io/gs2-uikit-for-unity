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

	[AddComponentMenu("GS2 UIKit/Formation/Mold/View/Label/Transaction/Gs2FormationSubMoldCapacityByUserIdLabel")]
    public partial class Gs2FormationSubMoldCapacityByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Mold != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{moldModelName}",
                            $"{_fetcher.Request.MoldModelName}"
                        ).Replace(
                            "{capacity}",
                            $"{_fetcher.Request.Capacity}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Mold.Name}"
                        ).Replace(
                            "{userData:userId}",
                            $"{_userDataFetcher.Mold.UserId}"
                        ).Replace(
                            "{userData:capacity}",
                            $"{_userDataFetcher.Mold.Capacity}"
                        ).Replace(
                            "{userData:capacity:changed}",
                            $"{_userDataFetcher.Mold.Capacity + _fetcher.Request.Capacity}"
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
                            "{moldModelName}",
                            $"{_fetcher.Request.MoldModelName}"
                        ).Replace(
                            "{capacity}",
                            $"{_fetcher.Request.Capacity}"
                        )
                    );
                }
            } else {
                onUpdate?.Invoke(
                    format.Replace(
                        "{capacity}",
                        "0"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationSubMoldCapacityByUserIdLabel
    {
        private Gs2FormationSubMoldCapacityByUserIdFetcher _fetcher;
        private Gs2FormationOwnMoldFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FormationSubMoldCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2FormationSubMoldCapacityByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2FormationOwnMoldFetcher>() ?? GetComponentInParent<Gs2FormationOwnMoldFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationSubMoldCapacityByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationSubMoldCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2FormationSubMoldCapacityByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2FormationOwnMoldFetcher>() ?? GetComponentInParent<Gs2FormationOwnMoldFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationSubMoldCapacityByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationSubMoldCapacityByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSubMoldCapacityByUserIdLabel
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