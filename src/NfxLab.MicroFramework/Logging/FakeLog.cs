using System;
using Microsoft.SPOT;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace NfxLab.MicroFramework.Logging
{
    public class FakeLog : Log
    {
        public override void AssemblyInfo()
        {
        }

        public override void Title(string name)
        {
        }

        public override void Line()
        {
        }

        public override void WriteDebug(params object[] data)
        {
        }

        public override void Write(params object[] data)
        {
        }
    }
}
