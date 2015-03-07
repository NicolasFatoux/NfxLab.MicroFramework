using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using NfxLab.MicroFramework.Drivers.Grove;

namespace NfxLab.BarometerSensorTest
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("Grove barometer sensor test");

            Debug.Print("initializing sensor...");
            var sensor = new BarometerSensor();

            while (true)
            {
                Debug.Print("sensor update...");
                sensor.Update();
                Debug.Print("temperature : " + sensor.TemperatureCelcius);
                Debug.Print("pressure : " + sensor.PressurePascal);
                Thread.Sleep(500);
            }
        }

    }
}
