スクリーンショットのデフォルト画像を変更しよう {#screenshotdefault}
===

# 解説

"RPGアツマールAPI for Unity"は、極力[RPGアツマール]の機能に準拠し  
プレイヤー体験を損なわないように"設計", "実装"をしていますが、それでも  
Unityの仕様上どうしても、完全再現出来ない場面があります。

その代表として、[RPGアツマール]のスクリーンショットボタンを直接押した場合  
Unity側のレンダリングバッファコピーが間に合わず、直ちに[RPGアツマール]上にプレビューできない問題です。  

その場合は、1度目のスクリーンショットボタンに"もう一度押してほしい"というアピールする"デフォルト画像"を  
設定しておくことで、プレイヤーには手間を増やしてしまいますが、スクリーンショットを撮ることが出来ている  
お知らせとしても使えます。

以下が、その手順及びサンプルコードになります。

# サンプルコード

~~~{.cs}
using RpgAtsumaruApiForUnity;
using UnityEngine;

public class RpgAtsumaruSample : MonoBehaviour
{
    private void Awake()
    {
        // もしプラグインの初期化が終わっていないなら
        if (!RpgAtsumaruApi.Initialized)
        {
            // プラグインの初期化
            RpgAtsumaruApi.Initialize();
        }


        // 作ったデフォルトスクリーンショット画像を設定する
        //（デフォルトスクリーンショットのデータは凄まじく長いですが問題ありません）
        RpgAtsumaruApi.GeneralApi.SetDefaultScreenShotImgeData("data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEBXgFeAA..... 超長いです");
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.GeneralApi | 汎用APIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.SetDefaultScreenShotImgeData() | デフォルトスクリーンショット画像を設定する関数 |
