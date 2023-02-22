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
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enhance/Enhance/View/Gs2EnhanceEnhanceEnhanceActionLabel")]
    public partial class Gs2EnhanceEnhanceEnhanceActionLabel : MonoBehaviour
    {
        public void Update()
        {
            onUpdate?.Invoke(
                format.Replace(
                    "{rateName}", $"{action?.RateName}"
                ).Replace(
                    "{targetItemSetId}", $"{action?.TargetItemSetId}"
                ).Replace(
                    "{materials}", $"{action?.Materials}"
                ).Replace(
                    "{config}", $"{action?.Config}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceEnhanceEnhanceActionLabel
    {
        
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceEnhanceEnhanceActionLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceEnhanceEnhanceActionLabel
    {
        public Gs2EnhanceEnhanceEnhanceAction action;
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceEnhanceEnhanceActionLabel
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