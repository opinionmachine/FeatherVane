﻿// Copyright 2012-2012 Chris Patterson
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
namespace FeatherVane.Vanes
{
    using System;
    using System.IO;

    public class Logger<T> :
        FeatherVane<T>,
        AgendaItem<T>
    {
        readonly Func<Payload<T>, string> _getLogMessage;
        readonly TextWriter _output;

        public Logger(TextWriter output, Func<Payload<T>, string> getLogMessage)
        {
            _output = output;
            _getLogMessage = getLogMessage;
        }

        public Agenda<T> Plan(Planner<T> planner, Payload<T> payload, Vane<T> next)
        {
            planner.Add(this);

            return next.Plan(planner, payload);
        }

        public bool Execute(Agenda<T> agenda)
        {
            Payload<T> payload = agenda.Payload;

            string message = _getLogMessage(payload);
            _output.WriteLine(message);

            return agenda.Execute();
        }

        public bool Compensate(Agenda<T> agenda)
        {
            return agenda.Compensate();
        }
    }
}