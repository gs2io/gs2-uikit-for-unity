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
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    [AddComponentMenu("GS2 UIKit/Formation/Mold/Context/Convert/Gs2FormationConvertMoldModelToOwnMold")]
    public class Gs2FormationConvertMoldModelToOwnMold : MonoBehaviour
    {
        private Gs2FormationMoldModelContext _originalContext;
        private Gs2FormationOwnMoldContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2FormationMoldModelContext>() ?? GetComponentInParent<Gs2FormationMoldModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationMoldModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2FormationOwnMoldContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnMoldContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2FormationMoldModelContext>() ?? GetComponentInParent<Gs2FormationMoldModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2FormationOwnMoldContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnMold(
                OwnMold.New(
                    _originalContext.MoldModel.Namespace,
                    _originalContext.MoldModel.moldModelName
                )
            );
            enabled = false;
        }
    }
}