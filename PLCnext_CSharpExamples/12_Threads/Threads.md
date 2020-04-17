# Threads

[Threads.cs](Threads.cs)

Minimal required:

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2019.0-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2019.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

This is a minimal example how to run threads with PLCnext, outside of the realtime application. Nevertheless it must be implemented cooperative.

>**ATTENTION: Error due to changed priority:**
You can select a priority between 0 and 99. To not change the structure of the real-time Threads, Phoenix Contact recommends priority 0. Otherwise, the stability of the firmware cannot be guaranteed. To perform time-critical tasks programs are provided in ESM tasks.

The implementation of threads in PLCnext is similar to the .NET standard as you can see in the first example. Different to .NET is the priority handling. The standard .NET priorities can be used as usual and used to priortize the C# threads. In C++ Phoenix Contact recommends to use the default priority of 0. All threads with this priority are handled in a round robin scheduling schema. The priorities from 1-99 are all FIFO and a higher priority task interrupts a lower one.

In C# all default priority values of the enum `ThreadPriority` are mapped to explicit priorities of the underlying Linux sub system. The default priority `ThreadPriority.Normal` is mapped to a system priority of 2. Only `ThreadPriority.Lowest` (mapped priority 0) is not in conflict with our realtime system. For priorities higher than 4, it is possible to cast integer like in the example: `StaticCaller.Priority = (ThreadPriority)setPrio;`

