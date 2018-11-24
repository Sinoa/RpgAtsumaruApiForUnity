スクリーンショットを撮って送信しよう {#screenshot}
===

# 解説

プレイヤーがゲームの決定的な瞬間や、他のプレイヤーに自慢したいときに  
ゲームの画面を記録したいと思うことでしょう、そのときはRPGアツマールのスクリーンショットを使います。

通常は、RPGアツマールのスクリーンショットボタンを使うため、ゲーム側から特別な操作は必要ありませんが  
ゲーム側からも決定的な瞬間を捉えるためにロジック的に制御する事が出来ます。  
以下に、そのサンプルコードがあります。

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


        // スクリーンショットのクォリティを最高品質に設定（デフォルトは50です）
        // クォリティが高すぎると、ブラウザによってサイズ問題による表示失敗があるので注意して下さい
        RpgAtsumaruApi.GeneralApi.ScreenShotQuality = 100;
    }


    // スクリーンショットを撮る
    public async void ScreenShot()
    {
        // スクリーンショットAPIを呼んで待機する
        //（投稿されるまでの待機ではなく、スクリーンショットを撮った後にRPGアツマールからの応答を待つ待機であることに注意して下さい）
        await RpgAtsumaruApi.GeneralApi.ScreenshotAsync();
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.GeneralApi | 汎用APIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.ScreenShotQuality | スクリーンショットのクォリティを取得設定するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruGeneral.ScreenshotAsync() | スクリーンショットを撮って直ちにプレビューする関数 |
