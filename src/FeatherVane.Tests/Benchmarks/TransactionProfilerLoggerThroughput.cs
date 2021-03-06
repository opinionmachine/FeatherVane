// Copyright 2012-2012 Chris Patterson
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
// except in compliance with the License. You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
// ANY KIND, either express or implied. See the License for the specific language governing
// permissions and limitations under the License.
namespace FeatherVane.Tests.Benchmarks
{
    using System;
    using System.IO;
    using Vanes;

    public class TransactionProfilerLoggerThroughput :
        Throughput
    {
        readonly Vane<Subject> _vane;

        public TransactionProfilerLoggerThroughput()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            _vane = Vane.Success(new Logger<Subject>(sw, x => ""),
                new Profiler<Subject>(sw, TimeSpan.FromMilliseconds(2)),
                new Transaction<Subject>());
        }

        public void Execute(Subject subject)
        {
            _vane.Execute(subject);
        }
    }
}