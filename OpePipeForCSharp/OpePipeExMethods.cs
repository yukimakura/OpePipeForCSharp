using System;

namespace OpePipeForCSharp
{
    public static class OpePipeExMethods
    {
        /// <summary>
        /// 関数型言語のパイプ演算子のような機能を拡張メソッドにて提供する
        /// </summary>
        /// <typeparam name="T">対象となる値の型</typeparam>
        /// <param name="value"></param>
        /// <param name="action">返り値がVoidとなる任意の処理</param>
        /// <returns>拡張メソッドの引数valueの値が何も処理されずにそのまま返される</returns>
        public static T Pipe<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        /// <summary>
        /// 関数型言語のパイプ演算子のような機能を拡張メソッドにて提供する
        /// </summary>
        /// <typeparam name="FromT">対象となる値の型</typeparam>
        /// <typeparam name="ToT">引数funcによって変形された返り値の型</typeparam>
        /// <param name="value"></param>
        /// <param name="func">任意の値を加工する処理</param>
        /// <returns>引数funcによって変形された返り値</returns>
        public static ToT Pipe<FromT, ToT>(this FromT value, Func<FromT, ToT> func)
           => func(value);


    }
}
