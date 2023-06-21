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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/AcceptVersion/View/Properties/UserId/Gs2VersionAcceptVersionUserIdEnabler")]
    public partial class Gs2VersionAcceptVersionUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcceptVersion != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableUserIds.Contains(_fetcher.AcceptVersion.UserId));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableUserIds.Contains(_fetcher.AcceptVersion.UserId));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableUserId.StartsWith(_fetcher.AcceptVersion.UserId));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableUserId.EndsWith(_fetcher.AcceptVersion.UserId));
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2VersionAcceptVersionUserIdEnabler
    {
        private Gs2VersionOwnAcceptVersionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2VersionOwnAcceptVersionFetcher>() ?? GetComponentInParent<Gs2VersionOwnAcceptVersionFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2VersionOwnAcceptVersionFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionAcceptVersionUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionAcceptVersionUserIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableUserIds;

        public string enableUserId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionAcceptVersionUserIdEnabler
    {
        
    }
}