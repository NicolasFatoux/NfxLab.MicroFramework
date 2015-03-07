using NfxLab.MicroFramework.External;

namespace NfxLab.MicroFramework.Drivers.Grove
{
    /// <summary>
    /// Driver for the Grove Barometer Sensor
    /// http://www.seeedstudio.com/wiki/Grove_-_Barometer_Sensor
    /// </summary>
    public class BarometerSensor
    {
        /// <summary>
        /// I2C Address of BMP085
        /// </summary>
        const byte Address = 0x77;

        /// <summary>
        /// BMP085 mode
        /// </summary>
        const BMP085.DeviceMode Mode = BMP085.DeviceMode.UltraHighResolution;

        
        /// <summary>
        /// The sensor driver
        /// </summary>
        BMP085 sensor;

        
        /// <summary>
        /// The temperature in degree celcius
        /// </summary>
        public float TemperatureCelcius { get; private set; }

        /// <summary>
        /// The pressure in pascal
        /// </summary>
        public float PressurePascal { get; private set; }


        /// <summary>
        /// Initializes a new instance of BarometerSensor
        /// </summary>
        public BarometerSensor()
        {
            sensor = new BMP085(Address, Mode);
        }

        /// <summary>
        /// Read the sensor
        /// </summary>
        public void Update()
        {
            sensor.TakeMeasurements();
            PressurePascal = sensor.Pascal;
            TemperatureCelcius = sensor.Celsius;
        }
    }
}
