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

using System.Collections.Generic;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/Entry/View/Gs2DictionaryOwnEntryList")]
    public partial class Gs2DictionaryOwnEntryList : MonoBehaviour
    {
        private List<Gs2DictionaryOwnEntryContext> _children;

        public void Update() {
            if (_fetcher.Fetched && _fetcher.Entries != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Entries.Count) {
                        _children[i].EntryModel.entryName = this._fetcher.Entries[i].Name;
                        _children[i].Entry.entryModelName = this._fetcher.Entries[i].Name;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryOwnEntryList
    {
        private Gs2DictionaryNamespaceContext _context;
        private Gs2DictionaryOwnEntryListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2DictionaryNamespaceContext>();
            _fetcher = GetComponentInParent<Gs2DictionaryOwnEntryListFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryNamespaceContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryOwnEntryListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2DictionaryOwnEntryContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.EntryModel = EntryModel.New(
                    _context.Namespace,
                    ""
                );
                node.Entry = OwnEntry.New(
                    _context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryOwnEntryList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryOwnEntryList
    {
        public Gs2DictionaryOwnEntryContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryOwnEntryList
    {

    }
}