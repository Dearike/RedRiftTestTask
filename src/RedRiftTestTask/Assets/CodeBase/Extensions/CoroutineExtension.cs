using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;

namespace CodeBase.Extensions
{
    public static class CoroutineExtension
    {
        private static readonly Dictionary<Guid, int> Groups = new Dictionary<Guid, int>();
        
        public static void ParallelCoroutinesGroup(this IEnumerator coroutine, Guid groupId, ICoroutineRunner runner)
        {
            if (!Groups.ContainsKey(groupId))
                Groups.Add(groupId, 0);

            Groups[groupId]++;

            runner.StartCoroutine(
                DoParallel(coroutine, groupId, runner));
        }


        private static IEnumerator DoParallel(IEnumerator coroutine, Guid groupId, ICoroutineRunner runner)
        {
            yield return runner.StartCoroutine(coroutine);
            Groups[groupId]--;
        }
        
        public static bool IsGroupProcessing(Guid groupId)
        {
            return Groups.ContainsKey(groupId) && Groups[groupId] > 0;
        }
    }
}
