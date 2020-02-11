using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoUI {
    public static class IEnumerableExt {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> doSomeAction) {
            foreach (var item in items) {
                doSomeAction(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<int, T> doSomeAction) {
            for (var i = 0; i < items.Count(); i++) {
                doSomeAction(i, items.ElementAt(i));
            }
        }
    }
}
