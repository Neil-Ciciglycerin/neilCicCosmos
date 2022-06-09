﻿using System;
using Sys = Cosmos.System;
using Cosmos.System;
using Cosmos.TestRunner;
using Console = System.Console;

namespace ThreadingTests
{
    public class Kernel : Sys.Kernel
    {
        int variable = 0;

        int variableTwo = 0;

        int variableThree = 0;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Let's Test Threading!");
        }

        protected override void Run()
        {
            try
            {
                var threadOne = new Thread(ThreadOne);
                var threadTwo = new Thread(ThreadTwo);

                threadOne.Start();
                threadTwo.Start();

                variableThree = 3;

                Thread.Sleep(1000);

                Assert.AreEqual(variable, 1, "Changing global variable from thread works");

                Assert.AreEqual(variableTwo, 2, "Changing global variable from second thread works");

                Assert.AreEqual(variableThree, 3, "Changing global variable from main context works");

                TestController.Completed();
            }
            catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                mDebugger.Send(e.Message);
                TestController.Failed();
            }
        }

        public void ThreadOne()
        {
            variable = 1;
        }

        public void ThreadTwo()
        {
            variableTwo = 2;
        }
    }
}
