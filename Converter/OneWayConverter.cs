using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{

    public class OneWayConverter<S, T> : BaseConverter<S, T> where S : class where T: class
    {
        public OneWayConverter(Func<S, T> forwardConverter) : base(forwardConverter, null) { }

    }
}
