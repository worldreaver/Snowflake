using System;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Profiling;
using Worldreaver.Snowflake;

public class Brenchmark : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        const int count = 1;
        var unique = new Id64Generator(10);
        var unique2 = new IdGuidGenerator(10);
        var unique3 = new IdStringGeneratorWrapper(unique);
        
        Profiler.BeginSample("track snowflake 1");
        string str1 = "";
        for (int i = 0; i < count; i++)
        {
            str1 = unique.GenerateId().ToString();
        }

        Profiler.EndSample();

        Profiler.BeginSample("track snowflake 2");
        for (int i = 0; i < count; i++)
        {
            unique2.GenerateId();
        }

        Profiler.EndSample();

        Profiler.BeginSample("track snowflake 3");
        string str2 = "";
        for (int i = 0; i < count; i++)
        {
            str2 = unique3.GenerateId();
        }

        Profiler.EndSample();

        Profiler.BeginSample("track guid");
        for (int i = 0; i < count; i++)
        {
            Guid.NewGuid();
        }

        Profiler.EndSample();
        
        Debug.Log(str1);
        Debug.Log(str2);
        
        Main();
    }

    private const string FORMATTER = "{0,22}{1,30}";

    private void Main()
    {
        // Long64BitGenerator();
        //
        // GuidGenerator();
        //
        // MacAddressGenerator();
        //
        // LongToStringIdGenerator();
        //
        // LongToUpperHexStringIdGenerator();
        //
        // LongToLowerHexStringIdGenerator();
        //
        // LongToBase32StringIdGenerator();
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
        var mac = NetworkInterface.GetAllNetworkInterfaces().First(i => i.OperationalStatus == OperationalStatus.Up).GetPhysicalAddress().GetAddressBytes();

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
}