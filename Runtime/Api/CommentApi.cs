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

using UnityEngine.SceneManagement;

namespace RpgAtsumaruApiForUnity
{
    /// <summary>
    /// RPGアツマールのコメントを制御するクラスになります
    /// </summary>
    public class RpgAtsumaruComment
    {
        /// <summary>
        /// RpgAtsumaruComment のインスタンスを初期化します
        /// </summary>
        /// <param name="receiver">RPGアツマールネイティブAPIコールバックを拾うレシーバ</param>
        internal RpgAtsumaruComment(RpgAtsumaruApi.RpgAtsumaruApiCallbackReceiver receiver)
        {
        }


        /// <summary>
        /// 現在のUnityのアクティブなシーン名を用いて、コメントシーンを切り替えます。
        /// また、コメントシーンの状態をリセットします。
        /// </summary>
        public void ChangeScene()
        {
            // 現在のシーン名を渡して状態をリセットする
            ChangeScene(SceneManager.GetActiveScene().name, true);
        }


        /// <summary>
        /// 現在のUnityのアクティブなシーン名を用いて、コメントシーンを切り替えます。
        /// </summary>
        /// <param name="reset">コメントシーンの状態をリセットする場合は true</param>
        public void ChangeScene(bool reset)
        {
            // 現在のシーン名を渡して切り替える
            ChangeScene(SceneManager.GetActiveScene().name, reset);
        }


        /// <summary>
        /// 指定されたシーン名を用いて、コメントシーンを切り替えます。
        /// </summary>
        /// <param name="sceneName">切り替えるコメントシーン名。ただし、最大64文字のASCII文字列でなければなりません。さらに文字列の先頭にはアンダースコア2つをつけることは許されていません。</param>
        /// <param name="reset">コメントシーンの状態をリセットする場合は true</param>
        public void ChangeScene(string sceneName, bool reset)
        {
            // リセットする場合は
            if (reset)
            {
                // リセットしながらシーンを切り替えるネイティブプラグイン関数を叩く
                RpgAtsumaruNativeApi.ResetAndChangeScene(sceneName);
            }
            else
            {
                // そのままシーンを切り替えるネイティブプラグイン関数を叩く
                RpgAtsumaruNativeApi.ChangeScene(sceneName);
            }
        }


        /// <summary>
        /// コメントのシーン内で特定のコンテキストを設定します
        /// </summary>
        /// <param name="context">設定するコンテキストの文字列。ただし、最大64文字のASCII文字列でなければなりません。</param>
        public void SetContext(string context)
        {
            // ネイティブプラグイン関数を叩く
            RpgAtsumaruNativeApi.SetContext(context);
        }


        /// <summary>
        /// 現在のコンテキストに対して状態を進めます
        /// </summary>
        /// <param name="factor">現在のコンテキストに対して状態の内容を示す文字列</param>
        public void PushContextFactor(string factor)
        {
            // ネイティブプラグイン関数を叩く
            RpgAtsumaruNativeApi.PushContextFactor(factor);
        }


        /// <summary>
        /// 現在のコンテキストが特定コンテキストファクタの状態におけるマイナーコンテキストを進めます
        /// </summary>
        public void PushMinorContext()
        {
            // ネイティブプラグイン関数を叩く
            RpgAtsumaruNativeApi.PushMinorContext();
        }
    }
}