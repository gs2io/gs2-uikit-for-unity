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

using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/Entry/Context/Gs2DictionaryOwnEntryContext")]
    public partial class Gs2DictionaryOwnEntryContext : Gs2DictionaryEntryModelContext
    {
        public new void Start() {
            if (Entry == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Entry is not set in Gs2DictionaryOwnEntryContext.");
            }
        }
        public override bool HasError() {
            if (!base.HasError()) {
                return false;
            }
            if (Entry == null) {
                if (GetComponentInParent<Gs2DictionaryOwnEntryList>(true) != null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryOwnEntryContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryOwnEntryContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryOwnEntryContext
    {
        public OwnEntry Entry;

        public void SetOwnEntry(OwnEntry entry) {
            this.EntryModel = EntryModel.New(
                Namespace.New(
                    entry.NamespaceName
                ),
                entry.EntryName
            );
            this.Entry = entry;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryOwnEntryContext
    {

    }
}