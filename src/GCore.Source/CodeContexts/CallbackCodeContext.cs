using System;

namespace GCore.Source.CodeContexts
{
    public class CallbackCodeContext : ACodeContext
    {
        private Action<CallbackCodeContext>? _startCallback = null;
        private Action<CallbackCodeContext>? _endCallback = null;

        public CallbackCodeContext(
            CodeWriter codeWriter,
            Action<CallbackCodeContext>? startCallback,
            Action<CallbackCodeContext>? endCallback)
            : base(codeWriter)
        {
            _startCallback = startCallback;
            _endCallback = endCallback;

            _startCallback?.Invoke(this);
        }

        protected override void OnEndContext()
        {
            _endCallback?.Invoke(this);
        }
    }
}