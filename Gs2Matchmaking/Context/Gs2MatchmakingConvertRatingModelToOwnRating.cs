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
using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Context
{
    [AddComponentMenu("GS2 UIKit/Matchmaking/Rating/Context/Convert/Gs2MatchmakingConvertRatingModelToOwnRating")]
    public class Gs2MatchmakingConvertRatingModelToOwnRating : MonoBehaviour
    {
        private Gs2MatchmakingRatingModelContext _originalContext;
        private Gs2MatchmakingOwnRatingContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2MatchmakingRatingModelContext>() ?? GetComponentInParent<Gs2MatchmakingRatingModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingRatingModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2MatchmakingOwnRatingContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2MatchmakingRatingModelContext>() ?? GetComponentInParent<Gs2MatchmakingRatingModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2MatchmakingOwnRatingContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnRating(
                OwnRating.New(
                    _originalContext.RatingModel.Namespace,
                    _originalContext.RatingModel.ratingName
                )
            );
            enabled = false;
        }
    }
}