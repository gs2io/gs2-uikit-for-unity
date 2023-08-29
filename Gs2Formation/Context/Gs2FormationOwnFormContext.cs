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
 * deny overwrite
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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/Form/Context/Gs2FormationOwnFormContext")]
    public partial class Gs2FormationOwnFormContext : Gs2FormationFormModelContext
    {
        public new void Start() {
            if (Form == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Form is not set in Gs2FormationOwnFormContext.");
            }
        }
        public override bool HasError() {
            if (!base.HasError()) {
                return false;
            }
            if (Form == null) {
                if (GetComponentInParent<Gs2FormationOwnFormList>(true) != null) {
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

    public partial class Gs2FormationOwnFormContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnFormContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnFormContext
    {
        public OwnForm Form;

        public void SetOwnForm(OwnForm form) {
            this.FormModel = FormModel.NewByMoldModelName(
                Namespace.New(
                    form.NamespaceName
                ),
                form.MoldName
            );
            this.Form = form;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnFormContext
    {

    }
}