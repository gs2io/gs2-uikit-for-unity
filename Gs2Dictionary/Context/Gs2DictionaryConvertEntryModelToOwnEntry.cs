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

using System;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Context
{
    [AddComponentMenu("GS2 UIKit/Dictionary/Entry/Convert/Gs2DictionaryConvertEntryModelToOwnEntry")]
    public class Gs2DictionaryConvertEntryModelToOwnEntry : MonoBehaviour
    {
        private Gs2DictionaryEntryModelContext _context;
        
        public void Awake() {
            _context = GetComponentInParent<Gs2DictionaryEntryModelContext>();
        }
        
        public void Start() {
            this.onConverted.Invoke(
                OwnEntry.New(
                    _context.EntryModel.Namespace,
                    _context.EntryModel.entryName
                )
            );
            enabled = false;
        }
        
        [Serializable]
        private class ConvertEvent : UnityEvent<OwnEntry>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<OwnEntry> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}