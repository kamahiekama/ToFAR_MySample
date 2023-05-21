# ToFAR_MySample
ToF AR の個人的なサンプル集

ToF AR は git に含めていません。別途公式サイトからダウンロードしてインポートしてください。
https://developer.sony.com/ja/develop/tof-ar/download

# 動作確認ソフトウェア
Unity: 2022.2.1f1<br>
ToF AR: ver 1.3.0 

# 動作確認端末
Windows OS + Xperia 1 II<br>
mac OS + iPhone 13 Pro

# 解像度選択のサンプルシーン
Scene/Select_Resolution シーンを開いてください。<br>

ToFArCameraSelector.cs でやっていることは、以下です。

1. autostart を false にする（ストリーム自動で開始させない）
2. 解像度一覧を取得し、設定したい値に近いものを取得する
3. 設定を反映させる
4. ストリームを開始させる

ToFArCameraSelector.cs を ToFARColorManager と同じ GameObject に追加すれば他のシーンでも動作すると思います。
