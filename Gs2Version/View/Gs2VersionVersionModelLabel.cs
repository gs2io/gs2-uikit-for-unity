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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Version
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/VersionModel/View/Gs2VersionVersionModelLabel")]
    public partial class Gs2VersionVersionModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                onUpdate.Invoke(
                    format.Replace(
                        "{name}", _fetcher.VersionModel.Name.ToString()
                    ).Replace(
                        "{metadata}", _fetcher.VersionModel.Metadata.ToString()
                    ).Replace(
                        "{warningVersion}", _fetcher.VersionModel.WarningVersion.ToString()
                    ).Replace(
                        "{errorVersion}", _fetcher.VersionModel.ErrorVersion.ToString()
                    ).Replace(
                        "{scope}", _fetcher.VersionModel.Scope.ToString()
                    ).Replace(
                        "{currentVersion}", _fetcher.VersionModel.CurrentVersion.ToString()
                    ).Replace(
                        "{needSignature}", _fetcher.VersionModel.NeedSignature.ToString()
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2VersionVersionModelLabel
    {
        private Gs2VersionVersionModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2VersionVersionModelFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionVersionModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionVersionModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionVersionModelLabel
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