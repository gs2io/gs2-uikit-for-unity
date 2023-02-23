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
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/View/Gs2MissionMissionTaskModelList")]
    public partial class Gs2MissionMissionTaskModelList : MonoBehaviour
    {
        private List<Gs2MissionMissionTaskModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.MissionTaskModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.MissionTaskModels.Count) {
                        _children[i].MissionTaskModel.missionTaskName = this._fetcher.MissionTaskModels[i].Name;
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

    public partial class Gs2MissionMissionTaskModelList
    {
        private Gs2MissionMissionGroupModelContext _context;
        private Gs2MissionMissionTaskModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2MissionMissionGroupModelContext>();
            _fetcher = GetComponentInParent<Gs2MissionMissionTaskModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2MissionMissionTaskModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.MissionTaskModel = MissionTaskModel.New(
                    _context.MissionGroupModel,
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

    public partial class Gs2MissionMissionTaskModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskModelList
    {
        public Gs2MissionMissionTaskModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskModelList
    {

    }
}