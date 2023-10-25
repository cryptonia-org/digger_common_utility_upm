using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class TaskExtensions
    {
        public static IEnumerator WaitFor(this Task task)
        {
            yield return new WaitUntil(() => task.IsCompleted);
        }
    }
}