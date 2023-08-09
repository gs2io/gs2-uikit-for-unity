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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObjectHistory/View/Label/Gs2DatastoreDataObjectHistoryLabel")]
    public partial class Gs2DatastoreDataObjectHistoryLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.DataObjectHistory != null)
            {
                var createdAt = _fetcher.DataObjectHistory.CreatedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.DataObjectHistory.CreatedAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{dataObjectHistoryId}", $"{_fetcher?.DataObjectHistory?.DataObjectHistoryId}"
                    ).Replace(
                        "{generation}", $"{_fetcher?.DataObjectHistory?.Generation}"
                    ).Replace(
                        "{contentLength}", $"{_fetcher?.DataObjectHistory?.ContentLength}"
                    ).Replace(
                        "{createdAt:yyyy}", createdAt.ToString("yyyy")
                    ).Replace(
                        "{createdAt:yy}", createdAt.ToString("yy")
                    ).Replace(
                        "{createdAt:MM}", createdAt.ToString("MM")
                    ).Replace(
                        "{createdAt:MMM}", createdAt.ToString("MMM")
                    ).Replace(
                        "{createdAt:dd}", createdAt.ToString("dd")
                    ).Replace(
                        "{createdAt:hh}", createdAt.ToString("hh")
                    ).Replace(
                        "{createdAt:HH}", createdAt.ToString("HH")
                    ).Replace(
                        "{createdAt:tt}", createdAt.ToString("tt")
                    ).Replace(
                        "{createdAt:mm}", createdAt.ToString("mm")
                    ).Replace(
                        "{createdAt:ss}", createdAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryLabel
    {
        private Gs2DatastoreOwnDataObjectHistoryFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectHistoryFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectHistoryLabel
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