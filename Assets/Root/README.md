# Readme

## Requirements
[![Unity 2018.3+](https://img.shields.io/badge/unity-2018.3+-brightgreen.svg?style=flat&logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)
[![.Net 2.0 Scripting Runtime](https://img.shields.io/badge/.NET-2.0-blueviolet.svg?style=flat&cacheSeconds=2592000)](https://docs.unity3d.com/2019.1/Documentation/Manual/ScriptingRuntimeUpgrade.html)

## Installation

## Usages

```csharp
 // Start is called before the first frame update
    private void Start()
    {
        var unique = new Id64Generator(10);
        var unique2 = new IdGuidGenerator(10);
        var unique3 = new IdStringGeneratorWrapper(unique);

        Stopwatch wat = new Stopwatch();
//        wat.Start();
//        for (int i = 0; i < 50000000; i++)
//        {
//            Guid.NewGuid().ToString();
//        }
//
//        wat.Stop();
//        Debug.Log(wat.ElapsedMilliseconds); // ~80000ms

//        wat.Restart();
//        for (int i = 0; i < 50000000; i++)
//        {
//            unique.GenerateId();
//            //unique2.GenerateId();// is break it slowly
//        }
//
//        wat.Stop();
//        Debug.Log(wat.ElapsedMilliseconds);// ~20000ms

//        wat.Restart();
//        for (int i = 0; i < 50000000; i++)
//        {
//            unique3.GenerateId();
//        }
//
//        wat.Stop();
//        Debug.Log(wat.ElapsedMilliseconds); //~32000ms

        Main();
    }

    private const string FORMATTER = "{0,22}{1,30}";

    private static void Main()
    {
        Long64BitGenerator();

        GuidGenerator();

        MacAddressGenerator();

        LongToStringIdGenerator();

        LongToUpperHexStringIdGenerator();

        LongToLowerHexStringIdGenerator();

        LongToBase32StringIdGenerator();
    }

    private static void LongToBase32StringIdGenerator()
    {
        Debug.Log(" == String ids (base 32 conversion) ==");

        var idGenerator = new IdStringGeneratorWrapper(new Id64Generator(), IdStringGeneratorWrapper.Base32);

        foreach (var id in idGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }

        Debug.Log(" == String ids (base 32 conversion, leading zero) ==");

        idGenerator = new IdStringGeneratorWrapper(new Id64Generator(), IdStringGeneratorWrapper.Base32LeadingZero);

        foreach (var id in idGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void LongToLowerHexStringIdGenerator()
    {
        Debug.Log(" == String ids (lower hex conversion) with prefix 'low' ==");

        var idGenerator = new IdStringGeneratorWrapper(new Id64Generator(), IdStringGeneratorWrapper.LowerHex, "low");

        foreach (var id in idGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void LongToUpperHexStringIdGenerator()
    {
        Debug.Log(" == String ids (upper hex conversion) with prefix 'upper' ==");

        var idGenerator = new IdStringGeneratorWrapper(new Id64Generator(), IdStringGeneratorWrapper.UpperHex, "upper");

        foreach (var id in idGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void LongToStringIdGenerator()
    {
        Debug.Log(" == String ids with prefix 'o' ==");

        var idGenerator = new IdStringGeneratorWrapper(new Id64Generator(), "o");

        foreach (var id in idGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void Long64BitGenerator()
    {
        Debug.Log(" == Long ids (64 bit) ==");

        var id64Generator = new Id64Generator();

        foreach (var id in id64Generator.Take(5).ToArray())
        {
            GetBytesUInt64((ulong) id);
        }
    }

    private static void GuidGenerator()
    {
        Debug.Log(" == Guid ids ==");

        var idGuidGenerator = new IdGuidGenerator(0x123456789ABCL);

        foreach (var id in idGuidGenerator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void MacAddressGenerator()
    {
        var mac = NetworkInterface
            .GetAllNetworkInterfaces()
            .First(i => i.OperationalStatus == OperationalStatus.Up).GetPhysicalAddress().GetAddressBytes();

        if (BitConverter.IsLittleEndian)
            Array.Reverse(mac);

        var generator = new IdGuidGenerator(mac);

        Debug.Log($" == Guid ids with MAC Address identifier ({BitConverter.ToString(mac)}) ==");

        foreach (var id in generator.Take(5).ToArray())
        {
            Debug.Log(id);
        }

        Debug.Log($" == Guid ids with MAC Address identifier:[{BitConverter.ToString(mac)}] and epoch:[{new DateTime(2012, 10, 1)}] == ");

        generator = new IdGuidGenerator(mac, new DateTime(2012, 10, 1));

        foreach (var id in generator.Take(5).ToArray())
        {
            Debug.Log(id);
        }
    }

    private static void GetBytesUInt64(ulong argument)
    {
        byte[] byteArray = BitConverter.GetBytes(argument);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(byteArray);

        Debug.Log(string.Format(FORMATTER, argument, BitConverter.ToString(byteArray)));
    }
```
