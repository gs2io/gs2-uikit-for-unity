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

using Gs2.Unity.Gs2Gateway.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Gateway.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Gateway/FirebaseToken/Gs2GatewayFirebaseTokenContext")]
    public partial class Gs2GatewayFirebaseTokenContext : MonoBehaviour
    {
        public void Start() {
            if (FirebaseToken == null) {
                Debug.LogError("FirebaseToken is not set in Gs2GatewayFirebaseTokenContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GatewayFirebaseTokenContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayFirebaseTokenContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GatewayFirebaseTokenContext
    {
        public FirebaseToken FirebaseToken;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayFirebaseTokenContext
    {

    }
}