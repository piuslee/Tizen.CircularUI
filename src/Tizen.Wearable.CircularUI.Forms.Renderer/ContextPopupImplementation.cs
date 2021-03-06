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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using EContextPopup = ElmSharp.ContextPopup;
using EContextPopupDirection = ElmSharp.ContextPopupDirection;
using EContextPopupItem = ElmSharp.ContextPopupItem;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using XForms = Xamarin.Forms;
using XFPlatformTizen = Xamarin.Forms.Platform.Tizen;


[assembly: XForms.Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ContextPopupImplementation))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{

    internal class ContextPopupImplementation : IContextPopup, INotifyPropertyChanged, IDisposable
    {
        EContextPopup _popup;
        IDictionary<ContextPopupItem, EContextPopupItem> _items;
        ContextPopupItem _selectedItem = null;
        bool _isDisposed;

        public ContextPopupImplementation()
        {
            _popup = new EContextPopup(TForms.NativeParent)
            {
                Style = "select_mode",
            };

            _popup.BackButtonPressed += (s, e) =>
            {
                _popup.Dismiss();
            };

            _popup.Dismissed += (s, e) =>
            {
                Dismissed?.Invoke(this, EventArgs.Empty);
            };

            _popup.SetDirectionPriorty(
              EContextPopupDirection.Down,
              EContextPopupDirection.Down,
              EContextPopupDirection.Down,
              EContextPopupDirection.Down);

            _items = new Dictionary<ContextPopupItem, EContextPopupItem>();
        }

        ~ContextPopupImplementation()
        {
            Dispose(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public event EventHandler Dismissed;

        public ContextPopupItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                if (_popup != null)
                {
                    _popup.Unrealize();
                    _popup = null;
                }
            }

            _isDisposed = true;
        }



        public void AddItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ContextPopupItemPropertyChanged;
                AddItem(item);
            }
        }

        public void RemoveItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged -= ContextPopupItemPropertyChanged;
                if (_items.ContainsKey(item))
                {
                    var nativeItem = _items[item];
                    nativeItem.Delete();
                    _items.Remove(item);
                }
            }
        }

        public void ClearItems()
        {
            foreach (var item in _items.Keys)
                item.PropertyChanged -= ContextPopupItemPropertyChanged;

            _items.Clear();
            _popup.Clear();
        }

        public void Show(View anchor, int xAnchorOffset, int yAnchorOffset)
        {
            //Console.WriteLine("Show() anchor:" + anchor + ", xAnchorOffset:"+ xAnchorOffset + ", yAnchorOffset:" + yAnchorOffset);
            var geometry = XFPlatformTizen.Platform.GetRenderer(anchor).NativeView.Geometry;
            _popup.Move(geometry.X + xAnchorOffset, geometry.Y + yAnchorOffset);
            _popup.Show();

            if (_items.Count >= 2)
            {
                int index = 0;
                foreach (var item in _items)
                {
                    var nativeItem = item.Value;
                    if(index % 2 ==  0)
                    {
                        nativeItem.Style = "select_mode/top";
                    }
                    else
                    {
                        nativeItem.Style = "select_mode/bottom";
                    }
                    index++;
                }
            }
        }

        public void Dismiss()
        {
            _popup.Dismiss();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void ContextPopupItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as ContextPopupItem;

            if (e.PropertyName == nameof(ContextPopupItem.Label))
            {
                // If the native item already has a label
                EContextPopupItem nativeItem = _items[item];
                nativeItem.SetPartText("default", item.Label);
            }
        }

        void AddItem(ContextPopupItem item)
        {
            if (_items.ContainsKey(item))
                return;

            EContextPopupItem nativeItem;
            nativeItem = _popup.Append(item.Label);
            _items.Add(item, nativeItem);

            nativeItem.Selected += (s, e) =>
            {
                SelectedItem = item; // This will invoke SelectedIndexChanged if the index has changed
                ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem));
            };
        }
    }
}
