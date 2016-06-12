using Android.Speech.Tts;
using Android.OS;
using Xamarin.Forms;
using System.Collections.Generic;
using Java.Lang;
using Todo;

[assembly: Dependency (typeof (TextToSpeech_Android))]

namespace Todo
{
	public class TextToSpeech_Android : Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;
		public TextToSpeech_Android ()
		{
		}

		public void Speak (string text)
		{
			var c = Forms.Context; 
			toSpeak = text;
			if (speaker == null)
				speaker = new TextToSpeech (c, this);
			else
			{
				var p = "";
				var b = new Bundle();
				speaker.Speak (toSpeak, QueueMode.Flush, b, p);
			}
        }

		#region IOnInitListener implementation
		public void OnInit (OperationResult status)
		{
			if (status.Equals (OperationResult.Success)) {
				System.Diagnostics.Debug.WriteLine ("spoke");
				var p = "";
				var b = new Bundle();
				speaker.Speak(toSpeak, QueueMode.Flush, b, p);
			}
			else
				System.Diagnostics.Debug.WriteLine ("was quiet");
		}
		#endregion
	}
}