
# UnityでSerial通信を行うライブラリ

## 動作環境 
- Windows10 / Mac 10.11 ~ 10.13
- Unity 2017


## Usage

#### Install
1. UnityPackageををダブルクリックしてインポートする.


#### Test

*シリアルを受信するテストシーン*

1. testシーンを開く.
2. Arduinoなどのシリアル通信デバイスを接続.
3. シーンを実行する.
4. Consoleに受信したメッセージが表示される.
5. Unityから5秒ごとにテストメッセージが送信される

#### Method

*接続*

`SerialHandler.Connect (string portName, int baud = 9600, int timeout = 1000);`

*受信イベント*

`SerialHandler.OnReceivedLine(string msg);`	

`SerialHandler.OnReceivedByte(byte[] msg);`

*送信*
	
`SerialHandler.Write(string msg);`
	
`SerialHandler.WriteBytes (string msg);`
	
`SerialHandler.WriteLine (string msg);`
