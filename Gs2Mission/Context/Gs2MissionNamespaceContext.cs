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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Namespace/Context/Gs2MissionNamespaceContext")]
    public partial class Gs2MissionNamespaceContext : MonoBehaviour
    {
        public void Start() {
            if (Namespace == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Namespace is not set in Gs2MissionNamespaceContext.");
            }
        }

        public virtual bool HasError() {
            if (Namespace == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionNamespaceContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionNamespaceContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionNamespaceContext
    {
        public Namespace Namespace;

        public void SetNamespace(Namespace Namespace) {
            this.Namespace = Namespace;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionNamespaceContext
    {

    }
}