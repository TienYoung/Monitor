using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using Desktop.Models;

namespace Desktop.Services
{
    public class MockSensor : ISensorService<SensorModel>
    {
        public SensorModel DataModel => model;

        private SensorModel model = new SensorModel();

        private CancellationTokenSource? _cts;

        public void Start()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                // Simulate some work with a delay
                while (!_cts.Token.IsCancellationRequested)
                {
                    var data = Random.Shared.NextDouble();
                    if (data > 0.1)// Only the value greater than 0.1 is valid
                    {
                        model.CurrentValue = data;
                        model.AddDataRecord(DateTime.Today, data);
                    }
                    else
                    {
                        model.CurrentValue = null;
                    }

                    Thread.Sleep(20); // data capture interval
                }
            }, _cts.Token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }
    }
}
