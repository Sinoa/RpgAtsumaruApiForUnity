// zlib/libpng License
//
// Copyright (c) 2018 Sinoa
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

// (確認UnityVersion 2018.3.0b7)
// Unity用RPGアツマールオブジェクトを宣言
var RpgAtsumaruApiForUnity =
{
    // プラグインの状態を保持するコンテキストテーブルです
    $Context:
    {
        initialized: false,       // プラグインが初期化されたかどうか
        unityObjectName: "",      // 非同期操作時にUnityへ通知するためのゲームオブジェクト名
        volumeSubscription: null, // 音量APIのサブスクリプション参照
        unityMethodNames:         // 非同期的にUnityへ通知するための各種メソッド名
        {
            // Storage API
            getItems: "",   // getItems APIの通知名
            setItems: "",   // setItems APIの通知名
            removeItem: "", // removeItem APIの通知名

            // Volume API
            volumeChanged: "", // changed APIの通知名
        },
    },


    // プラグインの初期化を行います
    // initializeParameterJson : 初期化するためのパラメータをJSON化した値
    Initialize: function(initializeParameterJson)
    {
        // 既に初期化済みなら
        if (Context.initialized)
        {
            // 直ちに終了
            return;
        }


        // 受け取ったjsonデータをパースする
        var jsonData = Pointer_stringify(initializeParameterJson);
        var initParam = JSON.parse(jsonData);


        // コンテキストを初期化していく
        Context.unityObjectName = initParam.UnityObjectName;
        Context.unityMethodNames.getItems = initParam.GetItemsCallback;
        Context.unityMethodNames.setItems = initParam.SetItemsCallback;
        Context.unityMethodNames.removeItem = initParam.RemoveItemCallback;
        Context.unityMethodNames.volumeChanged = initParam.VolumeChangedCallback;


        // 初期化済みをマーク
        Context.initialized = true;
    },


    // RPGアツマールサーバーストレージからデータを取得します
    GetStorageItems: function(objectName, methodName)
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return
        }


        // RPGアツマールサーバーストレージからデータを拾ってくる
        window.RPGAtsumaru.storage.getItems()
            .then(function(saveData)
            {
                // 無名の配列はUnityのJsonUtilityで処理できないのでSaveDataItemsという名前を付加してJSONデータを生成
                var finalSaveData = {SaveDataItems:saveData};
                var jsonData = JSON.stringify(finalSaveData);


                // Unityに生成したJsonデータを送る
                SendMessage(Context.unityObjectName, Context.unityMethodNames.getItems, jsonData);
            });
    },


    // RPGアツマールサーバーストレージにデータを設定します
    // key : 設定するデータのキー（仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します）
    // value : 設定するデータの内容（約1KB=1ブロック相当）
    SetStorageItem: function(key, value)
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return
        }


        // C#文字列のポインタからJS文字列として取得
        var jsKey = Pointer_stringify(key);
        var jsVal = Pointer_stringify(value);


        // セーブデータテーブルを生成してRPGアツマールサーバーへ設定する
        var saveData = [{key:jsKey, value:jsVal}];
        window.RPGAtsumaru.storage.setItems(saveData)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(Context.unityObjectName, Context.unityMethodNames.setItems);
            });
    },


    // RPGアツマールサーバーストレージから指定されたキーのデータを削除します
    // key : 削除するデータのキー（仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します）
    RemoveStorageItem: function(key, objectName, methodName)
    {
        // 未初期化なら
        if (!Context.initialized)
        {
            // 直ちに終了する（応答するにも応答先を知らない）
            return
        }


        // C#文字列のポインタからJS文字列として取得
        var jsKey = Pointer_stringify(key);


        // RPGアツマールのストレージアイテム削除APIを叩く
        window.RPGAtsumaru.storage.removeItem(jsKey)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(Context.unityObjectName, Context.unityMethodNames.removeItem);
            });
    }
};


// 内部依存定義の追加
autoAddDeps(RpgAtsumaruApiForUnity, "$Context");


// Unityライブラリにライブラリオブジェクトを登録
mergeInto(LibraryManager.library, RpgAtsumaruApiForUnity);