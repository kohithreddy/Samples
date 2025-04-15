using System;
using SampleCLRComp;

public class Program
{
    public static void Main(string[] args)
    {
        string name = "World";
        string result = SampleUnmanagedClass.CallDotNetCore(name);
        Console.WriteLine(result);
        Console.ReadLine();
    }
}

//public class NativeMethods
//{
//    [DllImport("C:\\Users\\ankasani\\Desktop\\SampleCLRComp_New\\SampleCLRComp\\BP\\x64\\Debug\\SampleCLRComp.dll")]
//    public static extern int SampleUnmanagedConstint();
//    public static void anyname1()
//    {
//        Console.WriteLine(SampleUnmanagedConstint());
//    }
//}