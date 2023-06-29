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

using System;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Context
{
    [AddComponentMenu("GS2 UIKit/Mission/Counter/Context/Convert/Gs2MissionConvertCounterModelToOwnCounter")]
    public class Gs2MissionConvertCounterModelToOwnCounter : MonoBehaviour
    {
        private Gs2MissionCounterModelContext _originalContext;
        private Gs2MissionOwnCounterContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2MissionCounterModelContext>() ?? GetComponentInParent<Gs2MissionCounterModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionCounterModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2MissionOwnCounterContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2MissionCounterModelContext>() ?? GetComponentInParent<Gs2MissionCounterModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2MissionOwnCounterContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnCounter(
                OwnCounter.New(
                    _originalContext.CounterModel.Namespace,
                    _originalContext.CounterModel.counterName
                )
            );
            enabled = false;
        }
    }
}