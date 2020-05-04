using System;

namespace HanuEdmsApi.Converter
{
    public abstract class BaseConverter<S, T>
    {
        public Func<S, T> ForwardConverter { get; set; }
        public Func<T, S> BackwardConverter { get; set; }

        public BaseConverter(Func<S, T> forwardConverter, Func<T, S> backwardConverter)
        {
            ForwardConverter = forwardConverter;
            BackwardConverter = backwardConverter;
        }
    }
}
