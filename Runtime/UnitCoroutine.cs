using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonUtility
{
    public class UnitCoroutine
    {
        private MonoBehaviour _monoBehaviour;
        private Coroutine _coroutine;
        private TaskCompletionSource<object> _tcs;
        private CancellationTokenRegistration _cancelation;

        public bool Running { get; private set; } = false;

        public async Task Start(IEnumerator enumerator, MonoBehaviour monoBehaviour, CancellationToken cancellationToken)
        {
            Start(enumerator, monoBehaviour);
            _cancelation = cancellationToken.Register(Stop);
            await _tcs.Task;
        }

        public void Start(IEnumerator enumerator, MonoBehaviour monoBehaviour)
        {
            Stop();
            _monoBehaviour = monoBehaviour;
            _tcs = new TaskCompletionSource<object>();
            _coroutine = _monoBehaviour.StartCoroutine(Process(enumerator));
        }

        public void Stop()
        {
            if (_monoBehaviour != null && _coroutine != null)
            {
                _monoBehaviour.StopCoroutine(_coroutine);
                _monoBehaviour = null;
                _coroutine = null;
            }

            _cancelation.Dispose();
            _tcs?.SetResult(null);
            _tcs = null;

            Running = false;
        }

        private IEnumerator Process(IEnumerator enumerator)
        {
            Running = true;
            yield return enumerator;
            _tcs?.SetResult(null);
            _tcs = null;
            Running = false;
        }
    }
}