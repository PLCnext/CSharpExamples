# Threads
[Threads.cs](Threads.cs)

Minimal required: 
[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2019.0-blue.svg)](http://www.phoenixcontact.com/qr/1046008/softw)
[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2019.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

This is a minimal example how to run threads with PLCnext, outside of the realtime application. Never the less it must be implemented non blocking.
The created theads will run with a priority of -50 to -70 on the controller and can lead to connection issues if it is implemented blocking.