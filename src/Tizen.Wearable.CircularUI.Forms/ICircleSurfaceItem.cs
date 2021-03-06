﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ICircleSurfaceItem is an interface that controls the items represented in the CircleSurface.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface ICircleSurfaceItem
    {
        /// <summary>
        /// Gets or sets CircleSurfaceItem's visibility
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        bool IsVisible { get; set; }
    }
}