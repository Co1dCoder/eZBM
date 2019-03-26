using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.ECObjects.Instance;

namespace eZBMCE.Debug.EcObject
{
    public class PropertyInfoToWrite
    {
        public string m_propertyName { get; set; }
        public string m_type { get; set; }
        public object m_value { get; set; }

        public string GetValueAsString()
        {
            if (null == m_value)
                return "";

            return m_value.ToString();
        }

        public bool GetValueAsBoolean()
        {
            if (null == m_value)
                return false;
            try
            {
                bool boolValue = bool.Parse(m_value.ToString());
                return boolValue;
            }
            catch
            {
                return false;
            }
        }

        public int GetValueAsInt()
        {
            if (null == m_value)
                return 0;
            try
            {
                int intValue = int.Parse(m_value.ToString());
                return intValue;
            }
            catch
            {
                return 0;
            }
        }

        public double GetValueAsDouble()
        {
            if (null == m_value)
                return 0.0;
            try
            {
                double doubleValue = double.Parse(m_value.ToString());
                return doubleValue;
            }
            catch
            {
                return 0.0;
            }
        }
    }


    public class InstanceInfoToWrite
    {
        public string m_schemaName { get; set; }
        public string m_className { get; set; }
        public List<PropertyInfoToWrite> m_properties { get; set; }

        public InstanceInfoToWrite()
        {
            m_properties = new List<PropertyInfoToWrite>();
        }

        private void SetInstancePropertyValue(ref StandaloneECDInstance instance, PropertyInfoToWrite pInfo)
        {
            string type = pInfo.m_type.ToLower();
            switch (type)
            {
                case "string":
                    instance.MemoryBuffer.SetStringValue(pInfo.m_propertyName, -1, pInfo.GetValueAsString());
                    break;
                case "boolean":
                    instance.MemoryBuffer.SetBooleanValue(pInfo.m_propertyName, -1, pInfo.GetValueAsBoolean());
                    break;
                case "int":
                    instance.MemoryBuffer.SetIntegerValue(pInfo.m_propertyName, -1, pInfo.GetValueAsInt());
                    break;
                case "double":
                    instance.MemoryBuffer.SetDoubleValue(pInfo.m_propertyName, -1, pInfo.GetValueAsDouble());
                    break;
            }
        }
    }
}
