using FluentAssertions;
using System.Diagnostics;

namespace OpePipeForCSharp.Tests
{
    public class UnitTest
    {
        /// <summary>
        /// プリミティブな形(今回はintを代表として使う)で
        /// ラムダ式直接とFunc型経由でpipeできるかテストする
        /// </summary>
        [Fact]
        public void PrimitiveTest()
        {
            int value = 1;

            value.Pipe(x => x + 100)
                .Pipe(x => x.Should().Be(101));

            Func<int, int> addFunc = x => x + 100;
            value.Pipe(addFunc)
                .Pipe(x => x.Should().Be(101));

        }


        /// <summary>
        /// recordな形で
        /// ラムダ式直接とFunc型経由でpipeできるかテストする
        /// </summary>
        [Fact]
        public void RecordTest()
        {
            var value = new DummyRecord(1, "yukimakura");

            value.Pipe(x => x with { Name = x.Name + "_hoge" })
                .Pipe(x => x.Should().Be(new DummyRecord(1, "yukimakura_hoge")));

            Func<DummyRecord, DummyRecord> changeNameFunc = x => x with { Name = x.Name + "_hoge" };
            value.Pipe(changeNameFunc)
                .Pipe(x => x.Should().Be(new DummyRecord(1, "yukimakura_hoge")));

        }

        /// <summary>
        /// pipeにラムダ式およびFunc型を埋め込むことで型の変形ができるかをテストする
        /// </summary>
        [Fact]
        public void TransformTest()
        {
            var value = new DummyRecord(1, "yukimakura");

            value.Pipe(x => x.ID).Pipe(x => x + 100).Pipe(x => x.Should().Be(101));

            Func<DummyRecord, int> tfFunc = x => x.ID;
            Func<int, int> addFunc = x => x + 100;
            value.Pipe(tfFunc)
                .Pipe(addFunc)
                .Pipe(x => x.Should().Be(101));

        }

        /// <summary>
        /// action型を途中で埋め込んでも、返り値が引数に与えられたものを後続のpipeに渡しているかをテストする
        /// </summary>
        [Fact]
        public void ThruTest()
        {
            var value = new DummyRecord(1, "yukimakura");

            var thruFlag = false;

            Func<DummyRecord, int> tfFunc = x => x.ID;
            Action<int> modThruFlag = x => thruFlag = true;
            Func<int, int> addFunc = x => x + 100;
            value.Pipe(tfFunc)
                .Pipe(modThruFlag)
                .Pipe(addFunc)
                .Pipe(x => x.Should().Be(101));

            thruFlag.Should().BeTrue();

        }

    }
}