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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/EntryModel/View/SpriteSwitcher/Properties/Metadata/Gs2DictionaryEntryModelMetadataSpriteTableSwitcher")]
    public partial class Gs2DictionaryEntryModelMetadataSpriteTableSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.EntryModel != null)
            {
                if (sprites.Count(v => v.value == _fetcher.EntryModel.Metadata) > 0) {
                    this.onUpdate.Invoke(sprites.Find(v => v.value == _fetcher.EntryModel.Metadata).sprite);
                }
                else {
                    this.onUpdate.Invoke(defaultSprite);
                }
            }
            else {
                this.onUpdate.Invoke(defaultSprite);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryEntryModelMetadataSpriteTableSwitcher
    {
        private Gs2DictionaryEntryModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DictionaryEntryModelFetcher>() ?? GetComponentInParent<Gs2DictionaryEntryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryEntryModelFetcher.");
                enabled = false;
            }
            if (sprites == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2DictionaryEntryModelFetcher>() ?? GetComponentInParent<Gs2DictionaryEntryModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (sprites == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryEntryModelMetadataSpriteTableSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryEntryModelMetadataSpriteTableSwitcher
    {
        [System.Serializable]
        public class SpriteTableEntry
        {
            public string value;
            public Sprite sprite;
        }

        public List<SpriteTableEntry> sprites;
        public Sprite defaultSprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryModelMetadataSpriteTableSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}