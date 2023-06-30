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
using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Datastore/DataObjectHistory/View/Gs2DatastoreOwnDataObjectHistoryList")]
    public partial class Gs2DatastoreOwnDataObjectHistoryList : MonoBehaviour
    {
        private List<Gs2DatastoreOwnDataObjectHistoryContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.DataObjectHistories != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.DataObjectHistories.Count) {
                        _children[i].DataObjectHistory.generation = this._fetcher.DataObjectHistories[i].Generation;
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

    public partial class Gs2DatastoreOwnDataObjectHistoryList
    {
        private Gs2DatastoreOwnDataObjectHistoryListFetcher _fetcher;
        private Gs2DatastoreOwnDataObjectContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryListFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectHistoryListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2DatastoreOwnDataObjectHistoryContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.DataObjectHistory = OwnDataObjectHistory.New(
                    _fetcher.Context.DataObject,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryListFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreOwnDataObjectHistoryList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreOwnDataObjectHistoryList
    {
        public Gs2DatastoreOwnDataObjectHistoryContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreOwnDataObjectHistoryList
    {

    }
}