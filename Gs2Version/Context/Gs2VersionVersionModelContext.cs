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

using Gs2.Unity.Gs2Version.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/VersionModel/Context/Gs2VersionVersionModelContext")]
    public partial class Gs2VersionVersionModelContext : MonoBehaviour
    {
        public void Start() {
            if (VersionModel == null) {
                Debug.LogError("VersionModel is not set in Gs2VersionVersionModelContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2VersionVersionModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionVersionModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionVersionModelContext
    {
        public VersionModel VersionModel;

        public void SetVersionModel(VersionModel VersionModel) {
            this.VersionModel = VersionModel;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionVersionModelContext
    {

    }
}