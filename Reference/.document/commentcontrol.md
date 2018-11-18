コメントの流れを制御しよう {#commentcontrol}
===

# 解説

RPGアツマールには、ニコニコ動画同様にユーザーコメント機能が存在し、このコメントはゲーム画面上に流れるようになっています。  
しかし、ニコニコ動画と違い現実の時間軸と同じ映像が必ず流れることは無いため、ゲームのような不規則かつ順不同に  
物事が発生するため、コメントの流れるタイミングが決定的に動かすことが出来ません、そのためゲームロジック側から  
RPGアツマールサーバーに、適切なタイミングでコメントを流してもらうためのイベントを通知する必要があります。

ゲームのイベントの時間軸には、「シーン」「イベントのコンテキスト」「イベントステップ」「イベントのサブステップ」が存在し  
RPGアツマールでは、それぞれ「シーン」「コンテキスト」「コンテキストファクター」「マイクロコンテキスト」と定義しており  
それぞれのタイミングをコントロールすることで、コメントの流れを制御出来ます。

詳しい内容は、公式サイトの[コメントを利用する](https://atsumaru.github.io/api-references/comment)を参照して下さい  
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
    }


    // ゲーム内の場面「シーン」を切り替える
    // さらに、その場面が全体の流れの最初に戻ることがあるのなら、リセットする
    public void ChangeScene(string sceneName, bool reset)
    {
        // コメントAPIにシーン名とリセットするかどうかのフラグを渡してシーンの切り替えをする
        RpgAtsumaruApi.CommentApi.ChangeScene(sceneName, reset);
    }


    // ゲーム内のイベントをトリガーします（会話が始まった、宝箱を調べた、何かのアクションが実行された、等）
    public void OnEventRaised(string eventName)
    {
        // ゲーム内で起きたイベントをRPGアツマールに通知します
        RpgAtsumaruApi.CommentApi.SetContext(eventName);
    }


    // ゲーム内で発生したイベントのステップを実行します（会話の選択肢で "はい", "いいえ" を選んだ、アイテムを所持 "している", "していない"、等）
    public void OnEventStep(string stepName)
    {
        // トリガーされたイベントのステップを通知します
        RpgAtsumaruApi.CommentApi.PushContextFactor(stepName);
    }


    // イベントのステップ内で起きた小さなステップを実行します（次の会話に続いた、アクションタイマーが経過した、等）
    public void OnEventSubStep()
    {
        // イベントのステップの進行を通知します
        RpgAtsumaruApi.CommentApi.PushMinorContext();
    }
}
~~~

# 参照ドキュメント

| Link | Help |
| :--- | :--- |
| RpgAtsumaruApiForUnity.RpgAtsumaruApi.CommentApi | コメントAPIを取得するプロパティ |
| RpgAtsumaruApiForUnity.RpgAtsumaruComment.ChangeScene(string, bool) | RPGアツマールにシーンが変わったことを通知する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruComment.SetContext(string) | RPGアツマールにイベントが起きたことを通知する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruComment.PushContextFactor(string) | RPGアツマールにイベント内ステップが起きたことを通知する関数 |
| RpgAtsumaruApiForUnity.RpgAtsumaruComment.PushMinorContext() | RPGアツマールにイベント内ステップのサブステップが進んだことを通知する関数 |
