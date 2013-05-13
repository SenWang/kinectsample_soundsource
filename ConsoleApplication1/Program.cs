using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
namespace ConsoleApplication1
{
    class Program
    {
        static KinectAudioSource kinectaudiosource ; 
        static void Main(string[] args)
        {
            KinectSensor sensor = KinectSensor.KinectSensors[0];
            sensor.Start();

            kinectaudiosource = sensor.AudioSource;
            SoundTracking();

            Console.WriteLine("請按下空白建結束");
            while ( Console.ReadKey().Key != ConsoleKey.Spacebar)
            {
            }

            sensor.Stop();
        }

        static void SoundTracking()
        {
            KinectAudioSource audioSource = AudioSourceSetup();

            audioSource.BeamAngleChanged += audioSource_BeamAngleChanged;
            audioSource.SoundSourceAngleChanged += audioSource_SoundSourceAngleChanged;

            audioSource.Start();
        }


        static KinectAudioSource AudioSourceSetup()
        {
            KinectAudioSource source = kinectaudiosource;
            source.NoiseSuppression = true;
            source.AutomaticGainControlEnabled = true;
            source.BeamAngleMode = BeamAngleMode.Adaptive;
            return source;
        }

        static void audioSource_BeamAngleChanged(object sender, BeamAngleChangedEventArgs e)
        {
            string maxmin = " ,最大Beam Angle :" + KinectAudioSource.MaxBeamAngle
                           + " , 最小Beam Angle :" + KinectAudioSource.MinBeamAngle;
            string output =  "偵測到Beam Angle :" + e.Angle.ToString() + maxmin;
            Console.WriteLine(output);
        }

        static void audioSource_SoundSourceAngleChanged(object sender, SoundSourceAngleChangedEventArgs e)
        {
            string maxmin = " ,最大Source Angle :" + KinectAudioSource.MaxSoundSourceAngle
                                        + " , 最小Sound Angle :" + KinectAudioSource.MinSoundSourceAngle;
            string output = "偵測到Source Angle :" + e.Angle.ToString()
                        + " , Source Confidence: " + e.ConfidenceLevel.ToString()
                        + maxmin;

            Console.WriteLine(output);

            //主動取得
            //string output2 = "SoundSourceAngle :" + audiosource.SoundSourceAngle +
            //                                    " , SoundSourceAngleConfidence: " + audiosource.SoundSourceAngleConfidence;
            //Console.WriteLine(output2);
        }

    }
}
