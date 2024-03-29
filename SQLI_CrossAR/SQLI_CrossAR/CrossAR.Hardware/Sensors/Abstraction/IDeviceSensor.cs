﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SQLI_CrossAR.CrossAR.Hardware.Sensors.Abstraction
{
    public delegate void SensorValueChangedEventHandler(object sender, SensorValueChangedEventArgs e);

    public interface IDeviceSensor
    {
        event SensorValueChangedEventHandler SensorValueChanged;
        void Start(DeviceSensorType sensorType, DeviceSensorDelay sensorDelay);
        void Stop(DeviceSensorType sensorType);
        bool IsActive(DeviceSensorType sensorType);
    }

    public class SensorValueChangedEventArgs : EventArgs
    {
        public SensorValueChangedEventArgs(DeviceSensorValues sv, DeviceSensorType st)
        {
            SensorValues = sv;
            SensorType = st;
        }
        public DeviceSensorValues SensorValues { get; }
        public DeviceSensorType SensorType { get; }
    }
}
