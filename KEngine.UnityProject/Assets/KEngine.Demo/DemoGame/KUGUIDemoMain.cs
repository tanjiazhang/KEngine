﻿#region Copyright (c) 2015 KEngine / Kelly <http://github.com/mr-kelly>, All rights reserved.

// KEngine - Toolset and framework for Unity3D
// ===================================
// 
// Filename: KUGUIDemoMain.cs
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

using System.Collections;
using System.IO;
using AppSettings;
using KEngine;
using KEngine.CoreModules;
using UnityEngine;

public class KUGUIDemoMain : MonoBehaviour
{
    private void Awake()
    {
    }

    IEnumerator Start()
    {
        KGameSettings.Instance.InitAction += OnGameSettingsInit;

        var engine = KEngine.AppEngine.New(
            gameObject,
            new IModule[]
            {
                //KGameSettings.Instance,
            },
            null,
            null);

        while (!engine.IsInited)
            yield return null;

        KUIModule.Instance.OpenWindow<KUIDemoHome>();

        KUIModule.Instance.CallUI<KUIDemoHome>(ui =>
        {
            // Do some UI stuff
        });

        Debug.Log("[SettingModule]Table: " + ExampleInfo.TabFilePath);
        var exampleInfoTable = SettingModule.Get<ExampleInfo>(ExampleInfo.TabFilePath);
        foreach (var info in exampleInfoTable.GetAll())
        {
            Debug.Log(string.Format("Name: {0}", info.Name));
        }
    }
    private void OnGameSettingsInit()
    {
        KGameSettings _ = KGameSettings.Instance;

        Logger.Log("Begin Load tab file...");

        var tabContent =
            File.ReadAllText(Application.dataPath + "/" + KEngine.AppEngine.GetConfig("ProductRelPath") +
                             "/Setting/test_tab.bytes");
        _.LoadTab<CTestTabInfo>(tabContent);
        Logger.Log("Output the tab file...");
        foreach (CTestTabInfo info in _.GetInfos<CTestTabInfo>())
        {
            Logger.Log("Id:{0}, Name: {1}", info.Id, info.Name);
        }
    }
}

public class CTestTabInfo : CBaseInfo
{
    // Id auto inherit
    public string Name;
}