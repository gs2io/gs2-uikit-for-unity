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
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Friend.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Friend/Profile/View/Localization/Gs2FriendProfileLocalizationVariables")]
    public partial class Gs2FriendProfileLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["userId"] = new StringVariable {
                    Value = _fetcher?.Profile?.UserId ?? "",
                };
                target.StringReference["publicProfile"] = new StringVariable {
                    Value = _fetcher?.Profile?.PublicProfile ?? "",
                };
                target.StringReference["followerProfile"] = new StringVariable {
                    Value = _fetcher?.Profile?.FollowerProfile ?? "",
                };
                target.StringReference["friendProfile"] = new StringVariable {
                    Value = _fetcher?.Profile?.FriendProfile ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendProfileLocalizationVariables
    {
        private Gs2FriendOwnProfileFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2FriendOwnProfileFetcher>() ?? GetComponentInParent<Gs2FriendOwnProfileFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendProfileFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FriendOwnProfileFetcher>() ?? GetComponentInParent<Gs2FriendOwnProfileFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendProfileLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendProfileLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendProfileLocalizationVariables
    {

    }
}

#endif