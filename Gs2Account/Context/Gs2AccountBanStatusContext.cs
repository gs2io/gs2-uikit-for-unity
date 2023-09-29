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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Account/BanStatus/Context/Gs2AccountBanStatusContext")]
    public partial class Gs2AccountBanStatusContext : MonoBehaviour
    {
        public void Start() {
            if (BanStatus == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: BanStatus is not set in Gs2AccountBanStatusContext.");
            }
        }

        public virtual bool HasError() {
            if (BanStatus == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2AccountBanStatusContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountBanStatusContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2AccountBanStatusContext
    {
        [SerializeField]
        private BanStatus _banStatus;
        public BanStatus BanStatus
        {
            get => _banStatus;
            set => SetBanStatus(value);
        }

        public void SetBanStatus(BanStatus BanStatus) {
            this._banStatus = BanStatus;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountBanStatusContext
    {

    }
}