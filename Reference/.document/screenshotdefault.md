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
using System.Threading.Tasks;
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
    }


    // 指定されたボードIDにスコアデータを送信します
    // 送信できるスコアボードの数は、RPGアツマールのAPI管理画面にて調整する事が出来ます。
    // 既定の数は10個までとなっています。
    public async void SendScore(int boardId, long score)
    {
        // RPGアツマールにスコアを送信する
        await RpgAtsumaruApi.ScoreboardApi.SendScoreAsync(boardId, score);
    }


    // 指定されたスコアボードIDのスコアボードをRPGアツマール上に表示します
    public async void ShowScoreboard(int boardId)
    {
        // 非同期の表示呼び出しをする（表示されたかどうかの待機ではなく、処理の結果待機であることに注意して下さい）
        await RpgAtsumaruApi.ScoreboardApi.ShowScoreboardAsync(boardId);
    }


    // RPGアツマールのスコアサーバーからスコアボードのデータを取得します
    public async Task<RpgAtsumaruScoreboardData> GetScoreboardData(int boardId)
    {
        // 非同期の取得呼び出しをする（タプル型で返されるため3つ目の結果だけを受け取る場合は以下の通りに実装すると良いでしょう）
        var (_, _, scoreboardData) = await RpgAtsumaruApi.ScoreboardApi.GetScoreboardAsync(boardId);
        return scoreboardData;
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.ScoreboardApi | スコアボードAPIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruScoreboard.SendScoreAsync() | RPGアツマールにスコアを送信する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruScoreboard.ShowScoreboardAsync() | RPGアツマール上にスコアボードを表示する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruScoreboard.GetScoreboardAsync() | RPGアツマールからスコアデータを取得する関数 |
