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
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObject/View/Gs2DatastoreDataObjectLabel")]
    public partial class Gs2DatastoreDataObjectLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                var createdAt = UnixTime.FromUnixTime(_fetcher.DataObject.CreatedAt).ToLocalTime();
                var updatedAt = UnixTime.FromUnixTime(_fetcher.DataObject.UpdatedAt).ToLocalTime();
                onUpdate.Invoke(
                    format.Replace(
                        "{dataObjectId}", _fetcher.DataObject.DataObjectId.ToString()
                    ).Replace(
                        "{name}", _fetcher.DataObject.Name.ToString()
                    ).Replace(
                        "{userId}", _fetcher.DataObject.UserId.ToString()
                    ).Replace(
                        "{scope}", _fetcher.DataObject.Scope.ToString()
                    ).Replace(
                        "{allowUserIds}", _fetcher.DataObject.AllowUserIds.ToString()
                    ).Replace(
                        "{status}", _fetcher.DataObject.Status.ToString()
                    ).Replace(
                        "{generation}", _fetcher.DataObject.Generation.ToString()
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
                    ).Replace(
                        "{updatedAt:yyyy}", updatedAt.ToString("yyyy")
                    ).Replace(
                        "{updatedAt:yy}", updatedAt.ToString("yy")
                    ).Replace(
                        "{updatedAt:MM}", updatedAt.ToString("MM")
                    ).Replace(
                        "{updatedAt:MMM}", updatedAt.ToString("MMM")
                    ).Replace(
                        "{updatedAt:dd}", updatedAt.ToString("dd")
                    ).Replace(
                        "{updatedAt:hh}", updatedAt.ToString("hh")
                    ).Replace(
                        "{updatedAt:HH}", updatedAt.ToString("HH")
                    ).Replace(
                        "{updatedAt:tt}", updatedAt.ToString("tt")
                    ).Replace(
                        "{updatedAt:mm}", updatedAt.ToString("mm")
                    ).Replace(
                        "{updatedAt:ss}", updatedAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DatastoreDataObjectLabel
    {
        private Gs2DatastoreDataObjectFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2DatastoreDataObjectFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreDataObjectLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectLabel
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