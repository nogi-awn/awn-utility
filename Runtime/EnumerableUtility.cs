using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AwnUtility
{
    public static class EnumerableUtility
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach(T item in sequence)
                action(item);
        }
    }
}