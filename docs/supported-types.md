# Supported Types
React Native passes data between platforms by mapping specific Objective-C types to valid JavaScript types. This project performs a similar task by mapping specific C# types to appropriate Objective-C types. The table below indicates the C# types that are supported as well as the JavaScript type that they get mapped to. While arrays and dictionaries are supported, they also may only contain the other types indicated in the table. Should you need to pass more complex objects, a good workaround is to serialized the object to JSON and pass it as a string.

| C# Type    | JS Type |
| ---------- | ------- |
| bool       | Boolean |
| double     | Number  |
| float      | Number  |
| int        | Number  |
| uint       | Number  |
| nuint      | Number  |
| long       | Number  |
| ulong      | Number  |
| short      | Number  |
| ushort     | Number  |
| string     | String  |
| T[]        | Array   |
| Dictionary | Object  |
