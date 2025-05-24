using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    public interface ISensorService<T>
    {
        public T DataModel { get; }
        public void Start();
        public void Stop();
    }
}
