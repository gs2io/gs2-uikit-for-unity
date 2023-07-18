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
 * deny overwrtite
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

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gs2.Unity.UiKit.Gs2Showcase.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Showcase/RandomDisplayItem/Context/Gs2ShowcaseRandomDisplayItemContext")]
    public partial class Gs2ShowcaseOwnRandomDisplayItemContext : MonoBehaviour
    {
        public void Start() {
            if (this.ownRandomDisplayItem == null) {
                Debug.LogError("RandomDisplayItem is not set in Gs2ShowcaseRandomDisplayItemContext.");
            }
        }

        public bool HasError() {
            if (this.ownRandomDisplayItem == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ShowcaseOwnRandomDisplayItemContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseOwnRandomDisplayItemContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseOwnRandomDisplayItemContext
    {
        [FormerlySerializedAs("RandomDisplayItem")] public OwnRandomDisplayItem ownRandomDisplayItem;

        public OwnRandomDisplayItem RandomDisplayItem => this.ownRandomDisplayItem;
        
        public void SetRandomDisplayItem(OwnRandomDisplayItem ownRandomDisplayItem) {
            this.ownRandomDisplayItem = ownRandomDisplayItem;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseOwnRandomDisplayItemContext
    {

    }
}