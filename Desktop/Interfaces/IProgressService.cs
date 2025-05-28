using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    public interface IProgressService
    {
        public event Action<IProgress<int>, CancellationToken>? Starting;
        
        public event Action<bool>? Stopped;
    }
}
