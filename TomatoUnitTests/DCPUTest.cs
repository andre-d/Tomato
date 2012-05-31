using Tomato;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Organic;
using Tomato.Hardware;
using System.Drawing;

namespace TomatoUnitTests
{
    /// <summary>
    ///This is a test class for DCPUTest and is intended
    ///to contain all DCPUTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DCPUTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private ushort[] GetOutput(List<ListEntry> Output)
        {
            List<ushort> result = new List<ushort>();
            foreach (var entry in Output)
            {
                if (entry.Listed && entry.Output != null)
                    result.AddRange(entry.Output);
            }
            return result.ToArray();
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void TestSet()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble(
                @"
SET B, 0x1001
SET C, 0x1002
SET X, 0x1003
SET Y, 0x1004
SET Z, 0x1005
SET I, 0x1006
SET J, 0x1007
SET [0x1000], 10
SET [0x1001], B
SET [0x1002], [0x1000]
SET PUSH, 10
SET PUSH, 20
SET A, POP
end:
SUB PC, 1"
                );
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(100);
            // Test registers
            Assert.AreEqual(0x1001, target.Memory.B);
            Assert.AreEqual(0x1002, target.Memory.C);
            Assert.AreEqual(0x1003, target.Memory.X);
            Assert.AreEqual(0x1004, target.Memory.Y);
            Assert.AreEqual(0x1005, target.Memory.Z);
            Assert.AreEqual(0x1006, target.Memory.I);
            Assert.AreEqual(0x1007, target.Memory.J);
            // Test Stack
            Assert.AreEqual(0xFFFF, target.Memory.SP);
            Assert.AreEqual(20, target.Memory.A);
            Assert.AreEqual(10, target.Memory[0xFFFF]);
            Assert.AreEqual(20, target.Memory[0xFFFE]);
            // Test memory
            Assert.AreEqual(target.Memory[0x1000], 10);
            Assert.AreEqual(target.Memory[0x1001], target.Memory.B);
            Assert.AreEqual(target.Memory[0x1002], target.Memory[0x1000]);

            Assert.AreEqual(assembler.LabelValues.GetValue("end"), target.Memory.PC);
        }

        [TestMethod()]
        public void TestAdd()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble("SET A, 10\nADD A, 10\nSET B, 0xFFF8\nADD B, 10");
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(100);
            Assert.AreEqual(20, target.Memory.A);
            Assert.AreEqual(2, target.Memory.B);
            Assert.AreEqual(0xFFFF, target.Memory.EX);
        }

        [TestMethod()]
        public void TestSub()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble("SET A, 10\nSUB A, 5");
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(1);
            Assert.AreEqual(10, target.Memory.A);
        }

        [TestMethod]
        public void TestLEM1802()
        {
            DCPU cpu = new DCPU();
            LEM1802 lem = new LEM1802();
            cpu.ConnectDevice(lem);
            cpu.Memory.A = 0;
            cpu.Memory.B = 0x8000;
            lem.HandleInterrupt();
            string test = "Hello, world!";
            for (int i = 0; i < test.Length; i++)
                cpu.Memory[0x8000 + i] = (ushort)(test[i] | 0xF000);
            lem.ScreenImage.Save("screen.bmp");
        }

        [TestMethod]
        public void TestIf()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble(
                @"
.longform
SET X, 0x8000
SET [0x8001], 10
SET [0x8002], 20
SET A, 10
IFE [X+1],[X+2]
    IFN [X+1], [X+2]
        SET A, 20
SUB PC, 1
");
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(100);
            Assert.AreEqual(10, target.Memory.A);
        }

        [TestMethod]
        public void TestJSR()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble(
                @"
JSR subroutine
SET B, 0
loop:
    ADD B, 1
    IFN B, 10
        SET PC, loop
SUB PC, 1

SET A, 10
subroutine:
SET A, 20
SET PC, POP"
                );
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(100);
            Assert.AreEqual(20, target.Memory.A);
            Assert.AreEqual(10, target.Memory.B);
        }

        [TestMethod]
        public void TestInterrupts()
        {
            DCPU target = new DCPU();
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble(
                @"
SET A, data
SET B, [A]
;IAS interruptHandler
;INT 20
;SUB PC, 1
;interruptHandler:
;SET B, A
;RFI 1
data:
    .dw 100"
                );
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(100);
            Assert.AreEqual(target.Memory.B, 100);
        }

        [TestMethod]
        public void TestHardware()
        {
            DCPU target = new DCPU();
            LEM1802 screen = new LEM1802();
            target.ConnectDevice(screen);
            Assembler assembler = new Assembler();
            List<ListEntry> assemblyOutput = assembler.Assemble(
                @"
HWN I
SUB I, 1
hw_init_loop:
    IFE I, 0xFFFF
        SET PC, hw_found
    SET PUSH, Y
    SET PUSH, Z
        HWQ I
    SET Z, POP
    SET Y, POP
    IFE A, 0xf615
        IFE B, 0x7349
            SET Y, I ; Screen
    SUB I, 1
    SET PC, hw_init_loop

hw_found:
    SET A, 0
    SET B, 0x8000
    HWI Y ; Map screen memory

    SET [0x8000], 'a' | 0xF000
    
    SET X, 0
    SET Y, 0
    SET A, msg
    SET B, 0x8000
    JSR printstr
    SUB PC, 1

; A: string
; B: screen memory
; X: location X
; Y: location Y
printstr:
    MUL X, Y
    ADD X, Y
    ADD X, B
str_loop:
    SET B, [A]
    BOR B, 0xF000
    SET [X], B
    ADD A, 1
    ADD X, 1
    IFE [A], 0
        SET PC, POP
    SET PC, str_loop

msg:
    .dw 'H', 'e', 'l', 'l', 'o', ' ', 'w', 'o', 'r', 'l', 'd', '!', 0"
                );
            target.Memory.Flash(GetOutput(assemblyOutput));
            target.Execute(1000);
            Assert.AreEqual(screen.ScreenMap, 0x8000);
            screen.ScreenImage.Save("screen.bmp");
        }
    }
}

