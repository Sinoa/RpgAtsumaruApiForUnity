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

using UnityEditor;
using UnityEngine;

namespace RpgAtsumaruApiForUnity.Editor
{
    /// <summary>
    /// 画像ファイルをDataUrls形式に変換するツールウィンドウクラスです
    /// </summary>
    public class ImageToDataUrlsConvertWindow : EditorWindow
    {
        /// <summary>
        /// エディタウィンドウの初期化をします
        /// </summary>
        private void Awake()
        {
            // タイトルを変えておく
            titleContent = new GUIContent("画像をDataUrlsに変換する");
        }
    }
}