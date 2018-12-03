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

using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

namespace RpgAtsumaruApiForUnity.Editor
{
    /// <summary>
    /// WebGLのテンプレートを出力するツールウィンドウクラスです
    /// </summary>
    public class WebGlTemplateGenerateWindow : EditorWindow
    {
        // 定数定義
        private const string WebGlTemplateImageDataUrlPlaceHolder = "%ATSUMARU_LOGO_DATAURL%";
        private const string WebGlTemplateImageWidthPlaceHolder = "%ATSUMARU_LOGO_WIDTH%";
        private const string WebGlTemplateImageHeightPlaceHolder = "%ATSUMARU_LOGO_HEIGHT%";

        // メンバ変数定義
        [NonSerialized]
        private bool initialized;
        private Queue<Action> messageQueue;



        /// <summary>
        /// エディタウィンドウの初期化をします
        /// </summary>
        private void Awake()
        {
            // 初期化を叩く
            Initialize();
        }


        /// <summary>
        /// エディタウィンドウのレンダリングループを実行します
        /// </summary>
        private void OnGUI()
        {
            try
            {
                // 初期化は実行し続ける
                Initialize();


                // メッセージを処理してレンダリングを行う
                ProcessMessage();
                Render();


                // メッセージがあればレンダリングループは継続する
                ContinueIfMessageAvailable();
            }
            catch (Exception exception)
            {
                // エラーが発生したのならウィンドウを閉じる要求をして、例外をキャプチャして再スロー
                Close();
                ExceptionDispatchInfo.Capture(exception).Throw();
            }
        }


        /// <summary>
        /// エディタウィンドウの実際の初期化を行います
        /// </summary>
        private void Initialize()
        {
            // 初期化済みなら
            if (initialized)
            {
                // 何もしない
                return;
            }


            // タイトルとメッセージキューを初期化する
            titleContent = new GUIContent("WebGLテンプレート作成");
            messageQueue = new Queue<Action>();


            // 初期化済みマーク
            initialized = true;
        }


        /// <summary>
        /// 送られてきたメッセージを処理します
        /// </summary>
        private void ProcessMessage()
        {
            // 現時点でのメッセージ数を取得して、その分だけ回る
            var currentMessageCount = messageQueue.Count;
            for (int i = 0; i < currentMessageCount; ++i)
            {
                // メッセージを取り出して処理する
                messageQueue.Dequeue()();
            }
        }


        /// <summary>
        /// エディタウィンドウのレンダリングを行います
        /// </summary>
        private void Render()
        {
            // ボタンを描画するローカル関数を定義
            void RenderButton(string name, Action message, bool enable)
            {
                // 現在のGUIカラーを覚えて有効状態に応じて色を設定する
                var currentColo = GUI.color;
                GUI.color = enable ? GUI.color : Color.gray;


                // ボタンが押されてかつ有効なら
                if (GUILayout.Button(name) && enable)
                {
                    // 該当のメッセージ関数を登録する
                    messageQueue.Enqueue(message);
                }


                // 元の色に戻す
                GUI.color = currentColo;
            }


            // renderの内容を横並びにレイアウトするローカル関数を定義
            void LayoutHorizontal(Action render)
            {
                // 横並びレイアウト制御関数で描画関数を挟む
                GUILayout.BeginHorizontal();
                render();
                GUILayout.EndHorizontal();
            }
        }


        /// <summary>
        /// 未処理のメッセージがあるならループを継続します
        /// </summary>
        private void ContinueIfMessageAvailable()
        {
            // 未処理のメッセージがあるなら
            if (messageQueue.Count > 0)
            {
                // 再描画要求をする
                Repaint();
            }
        }
    }
}