using System;
using System.Collections.Generic;

namespace FakerLib.Test
{
    public class A
    {
        public B ClassB;

        public C ClassC;

        public List<float> TestList { get; set; }

        public string TestString;

        public DateTime TestDateTime { get; set; }

        public A(string testString)
        {
            TestString = testString;
        }
    }
}
