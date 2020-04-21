# Utilities

### DiskInfo
If you need to check the disk size on dozens of remote servers, this is the way to do it. Using WMI (Windows Management Instrumentation), the description, system name, name, device id, free space and size are retrieved and run through a series of calculations yielding [](this) in a log file.

An optional email alert can be sent out for a specific threshold.


### DatabaseChecker
This was a way to execute the same query for about two dozen databases on remote servers. The initial idea for this was to get a count of how many rows were in a specific table to do some performance comparison.

Planing to move it to some kind of web hosting in the future.


:warning: Both projects are quick, dirty and do what they need to do. They could both use some improvement around error handling and logging among other things.
