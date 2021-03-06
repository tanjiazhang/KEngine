﻿#region Copyright (c) 2015 KEngine / Kelly <http://github.com/mr-kelly>, All rights reserved.

// KEngine - Toolset and framework for Unity3D
// ===================================
// 
// Filename: KSpriteLoader.cs
// Date:     2015/12/03
// Author:  Kelly
// Email: 23110388@qq.com
// Github: https://github.com/mr-kelly/KEngine
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3.0 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library.

#endregion

using UnityEngine;

namespace KEngine
{

    [CDependencyClass(typeof(KAssetFileLoader))]
    public class KSpriteLoader : KAbstractResourceLoader
    {
        public Sprite Asset
        {
            get { return ResultObject as Sprite; }
        }

        public delegate void CSpriteLoaderDelegate(bool isOk, Sprite tex);

        private KAssetFileLoader AssetFileBridge;

        public override float Progress
        {
            get { return AssetFileBridge.Progress; }
        }

        public string Path { get; private set; }

        public static KSpriteLoader Load(string path, CSpriteLoaderDelegate callback = null)
        {
            CLoaderDelgate newCallback = null;
            if (callback != null)
            {
                newCallback = (isOk, obj) => callback(isOk, obj as Sprite);
            }
            return AutoNew<KSpriteLoader>(path, newCallback);
        }

        protected override void Init(string url, params object[] args)
        {
            base.Init(url, args);
            Path = url;
            AssetFileBridge = KAssetFileLoader.Load(Path, OnAssetLoaded);
        }

        private void OnAssetLoaded(bool isOk, UnityEngine.Object obj)
        {
            OnFinish(obj);
        }

        protected override void DoDispose()
        {
            base.DoDispose();
            AssetFileBridge.Release(); // all, Texture is singleton!
        }
    }

}