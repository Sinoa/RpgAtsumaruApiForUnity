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
    // RPGアツマールサーバーストレージからデータを取得します
    // objectName : データの取得が完了した時に、データの通知を受けるゲームオブジェクト名
    // methodName : objectNameに送信されるpublicなメソッド名（引数はjsonデータを１つ受け取る関数であるべきです）
    GetStorageItems: function(objectName, methodName)
    {
        // C#文字列のポインタからJS文字列として取得
        var jsObjectName = Pointer_stringify(objectName);
        var jsMethodName = Pointer_stringify(methodName);


        // RPGアツマールサーバーストレージからデータを拾ってくる
        window.RPGAtsumaru.storage.getItems()
            .then(function(saveData)
            {
                // 無名の配列はUnityのJsonUtilityで処理できないのでSaveDataItemsという名前を付加してJSONデータを生成
                var finalSaveData = {SaveDataItems:saveData};
                var jsonData = JSON.stringify(finalSaveData);


                // Unityに生成したJsonデータを送る
                SendMessage(jsObjectName, jsMethodName, jsonData);
            });
    },


    // RPGアツマールサーバーストレージにデータを設定します
    // key : 設定するデータのキー（仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します）
    // value : 設定するデータの内容（約1KB=1ブロック相当）
    // objectName : データの設定が完了した時に、通知を受けるゲームオブジェクト名
    // methodName : objectNameに送信されるpublicなメソッド名（引数はありません）
    SetStorageItem: function(key, value, objectName, methodName)
    {
        // C#文字列のポインタからJS文字列として取得
        var jsKey = Pointer_stringify(key);
        var jsVal = Pointer_stringify(value);
        var jsObjectName = Pointer_stringify(objectName);
        var jsMethodName = Pointer_stringify(methodName);


        // セーブデータテーブルを生成してRPGアツマールサーバーへ設定する
        var saveData = [{key:jsKey, value:jsVal}];
        window.RPGAtsumaru.storage.setItems(saveData)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(jsObjectName, jsMethodName);
            });
    },


    // RPGアツマールサーバーストレージから指定されたキーのデータを削除します
    // key : 削除するデータのキー（仕様上 "system", "data{N}" といった予約キー名を使うことを推奨します）
    // objectName : データの削除が完了した時に、通知を受けるゲームオブジェクト名
    // methodName : objectNameに送信されるpublicなメソッド名（引数はありません）
    RemoveStorageItem: function(key, objectName, methodName)
    {
        // C#文字列のポインタからJS文字列として取得
        var jsKey = Pointer_stringify(key);
        var jsObjectName = Pointer_stringify(objectName);
        var jsMethodName = Pointer_stringify(methodName);


        // RPGアツマールのストレージアイテム削除APIを叩く
        window.RPGAtsumaru.storage.removeItem(jsKey)
            .then(function()
            {
                // 完了したことを通知する
                SendMessage(jsObjectName, jsMethodName);
            });
    }
};


// Unityライブラリにライブラリオブジェクトを登録
mergeInto(LibraryManager.library, RpgAtsumaruApiForUnity);