﻿// Plato.NET
// Copyright (c) 2016 ReflectSoftware Inc.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Plato.Messaging.Interfaces;

namespace Plato.Messaging.Implementations.AMQ.Interfaces
{
    public interface IAMQSenderBytes: IAMQSender, IMessageSender<byte[]>
    {
    }
}
