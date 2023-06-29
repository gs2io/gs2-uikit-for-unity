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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/FormModel/Context/Gs2FormationFormModelContext")]
    public partial class Gs2FormationFormModelContext : MonoBehaviour
    {
        public void Start() {
            if (FormModel == null) {
                Debug.LogError("FormModel is not set in Gs2FormationFormModelContext.");
            }
        }

        public bool HasError() {
            if (FormModel == null) {
                if (GetComponentInParent<Gs2FormationFormModelList>(true) != null) {
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

    public partial class Gs2FormationFormModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationFormModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationFormModelContext
    {
        public FormModel FormModel;

        public void SetFormModel(FormModel FormModel) {
            this.FormModel = FormModel;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormModelContext
    {

    }
}