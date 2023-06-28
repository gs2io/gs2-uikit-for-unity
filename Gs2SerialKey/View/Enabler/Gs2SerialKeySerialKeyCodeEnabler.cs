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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2SerialKey.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SerialKey/SerialKey/View/Enabler/Properties/Code/Gs2SerialKeySerialKeyCodeEnabler")]
    public partial class Gs2SerialKeySerialKeyCodeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.SerialKey != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableCodes.Contains(_fetcher.SerialKey.Code));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableCodes.Contains(_fetcher.SerialKey.Code));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableCode.StartsWith(_fetcher.SerialKey.Code));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableCode.EndsWith(_fetcher.SerialKey.Code));
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2SerialKeySerialKeyCodeEnabler
    {
        private Gs2SerialKeySerialKeyFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2SerialKeySerialKeyFetcher>() ?? GetComponentInParent<Gs2SerialKeySerialKeyFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SerialKeySerialKeyFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SerialKeySerialKeyCodeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SerialKeySerialKeyCodeEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableCodes;

        public string enableCode;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SerialKeySerialKeyCodeEnabler
    {
        
    }
}