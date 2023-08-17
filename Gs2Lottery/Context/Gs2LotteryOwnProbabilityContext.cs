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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Lottery/Probability/Context/Gs2LotteryOwnProbabilityContext")]
    public partial class Gs2LotteryOwnProbabilityContext : MonoBehaviour
    {
        public void Start() {
            if (Probability == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Probability is not set in Gs2LotteryOwnProbabilityContext.");
            }
        }
        public virtual bool HasError() {
            if (Probability == null) {
                if (GetComponentInParent<Gs2LotteryOwnProbabilityList>(true) != null) {
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

    public partial class Gs2LotteryOwnProbabilityContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityContext
    {
        public OwnProbability Probability;

        public void SetOwnProbability(OwnProbability probability) {
            this.Probability = probability;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryOwnProbabilityContext
    {

    }
}