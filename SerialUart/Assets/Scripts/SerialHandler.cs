using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour {

	public delegate void OnReceivedLineHandler(string data);
	public event OnReceivedLineHandler OnReceivedLine;

	public delegate void OnReceivedByteHandler(byte[] data);
	public event OnReceivedByteHandler OnReceivedByte;

	SerialPort stream = new SerialPort();
	Thread thread;

	public enum MODE {
		READ_LINE,
		READ_BYTE
	}
	[SerializeField] MODE mode;

	byte[] readByte;
	string readStr;
	bool isNewReceived = false;
	bool isThreadRunning = false;

	void Start(){

	}

	// Update is called once per frame
	void Update () {

		if (isNewReceived) {
			if (mode == MODE.READ_LINE) {
				OnReceivedLine (readStr);
			}
			else if (mode == MODE.READ_BYTE) {
				OnReceivedByte (readByte);
			}
			isNewReceived = false;
		}
	}

	public void Connect(string portName, int baud = 9600, int timeout = 1000){

		stream.PortName = portName;
		stream.BaudRate = baud;
		stream.ReadTimeout = timeout;
		stream.DataBits = 8;
		stream.Parity = Parity.None;
		stream.StopBits = StopBits.One;
		stream.Open();

		thread = new Thread(ReceivetTread);
		thread.IsBackground = true;
		isThreadRunning = true;
		thread.Start();

	}

	public void WriteBytes(byte[] data){
		stream.Write (data, 0, data.Length);
	}
	public void Write(string str){
		stream.Write (str);
	}
	public void WriteLine(string str){
		stream.WriteLine (str);
	}

	public void Disconnect(){
		isNewReceived = false;
		isThreadRunning = false;

		if (thread != null && thread.IsAlive) {
			thread.Abort();
		}

		if (stream != null && stream.IsOpen) {
			stream.Close();
			stream.Dispose();
		}
	}

	void OnApplicationQuit(){
		Disconnect();
	}

	void ReceivetTread(){

		while (isThreadRunning && stream != null && stream.IsOpen) {
			try {
				if (mode == MODE.READ_LINE) {
					readStr = stream.ReadLine();
					isNewReceived = true;
				}
				if (mode == MODE.READ_BYTE) {
					if(stream.BytesToRead <= 0) break;

					int length = stream.BytesToRead;
					readByte = new byte [length];
					stream.Read(readByte, 0, length);
					isNewReceived = true;
				}
			} 
			catch (System.Exception e) {
				Debug.LogWarning(e.Message);
			}
		}
	}

}
