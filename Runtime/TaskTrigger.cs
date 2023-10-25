using System.Threading;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class TaskTrigger<T>
    {
        private TaskCompletionSource<T> _tcs;
        private CancellationTokenRegistration _cancelation;

        public bool Waiting { get; private set; }

        public bool Setted { get; private set; }

        public T Result { get; private set; }

        public async Task<T> Wait(CancellationToken cancellationToken)
        {
            Clear();

            if (Setted)
                return Result;

            _tcs = new TaskCompletionSource<T>();
            _cancelation = cancellationToken.Register(Clear);
            Waiting = true;
            return await _tcs.Task;
        }

        public void Set(T value)
        {
            Setted = true;
            Result = value;
            Clear(value);
        }

        /// <returns>returning self</returns>
        public TaskTrigger<T> Reset()
        {
            Clear();
            Setted = false;
            Result = default;
            return this;
        }

        private void Clear() => Clear(default);

        private void Clear(T result)
        {
            bool exists = _tcs != null &&
                _tcs.Task != null &&
                _tcs.Task.IsCompleted == false;

            if (exists)
            {
                TaskCompletionSource<T> tcs = _tcs;
                _tcs = null;
                tcs.TrySetResult(result);
            }

            _cancelation.Dispose();

            Waiting = false;
        }
    }

    public class TaskTrigger
    {
        private TaskTrigger<object> _taskTrigger = new TaskTrigger<object>();

        public bool Waiting => _taskTrigger.Waiting;

        public bool Setted => _taskTrigger.Setted;

        public async Task Wait(CancellationToken cancellationToken) => await _taskTrigger.Wait(cancellationToken);

        public void Set() => _taskTrigger.Set(null);

        /// <returns>returning self</returns>
        public TaskTrigger Reset()
        {
            _taskTrigger.Reset();
            return this;
        }
    }
}