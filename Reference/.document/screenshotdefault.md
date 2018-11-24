スクリーンショットのデフォルト画像を変更しよう {#screenshotdefault}
===

# 解説

"RPGアツマールAPI for Unity"は、極力RPGアツマールの機能に準拠し  
プレイヤー体験を損なわないように"設計", "実装"をしていますが、それでも  
Unityの仕様上どうしても、完全再現出来ない場面があります。

その代表として、RPGアツマールのスクリーンショットボタンを直接押した場合  
Unity側のレンダリングバッファコピーが間に合わず、直ちにRPGアツマール上にプレビューできない問題です。

その場合は、1度目のスクリーンショットボタンに"もう一度押してほしい"というアピールする"デフォルト画像"を  
設定しておくことで、プレイヤーには手間を増やしてしまいますが、スクリーンショットを撮ることが出来ている  
お知らせとしても使えます。

以下が、その手順及びサンプルコードになります。

# デフォルトスクリーンショット画像をDataUrl形式に変換する

## 概要

RPGアツマールでは、スクリーンショットの画像を[DataUrl]という形式を入力として受け付けています。  
そのため、プログラムからデフォルトスクリーンショット画像を設定する場合は、その形式に準拠します。  
以下に、そのデータを用意するまでの手順を解説します。

## Unityエディタのメニューバーからツールを起動

[DataUrl]形式の変換はオンラインツールでも存在しますが、"RPGアツマールAPI for Unity"では、変換ツールを内部で実装しました。  
Ver 1.1からUnityエディタのメニューバーに、新たに"RPGアツマール"が増えていますので、そこからアクセスします。  
![RPGアツマール->ウィンドウ->画像をDataUrls形式に変換をクリック](@ref image007.jpg)  
![変換する画像ファイルを選択するをクリック](@ref image008.jpg)  
![選択した画像を変換するをクリック](@ref image009.jpg)  
![プレビューして問題ないなら、クリップボードにコピーするをクリック](@ref image010.jpg)

この手順にて変換されたデータを、クリップボードにコピーして以下のサンプルコードの通りにデータを貼り付けます。  
また、63KiBより大きいデータでは稀に表示できないブラウザが存在するため、極力63KiB以内に抑えて下さい。

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


[DataUrl]: https://ja.wikipedia.org/wiki/Data_URI_scheme "DataUrl"