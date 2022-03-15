using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using whprepro;

static (IntPtr, IntPtr) CreateWHPPartition(IntPtr sourceAddress, ulong size)
{
    IntPtr hProcess = IntPtr.Zero;
    IntPtr guestAddress = (IntPtr)0;
    WindowsHypervisorPlatform.WHvCreatePartition(out IntPtr hPartition);
    WindowsHypervisorPlatform.SetProcessorCount(hPartition, 1);
    WindowsHypervisorPlatform.WHvSetupPartition(hPartition);

    try
    {
        OS.Allocate(sourceAddress, size);
        var psi = new OS.StartupInfo();
        var si = new OS.SecurityAttributes();
        OS.CreateProcess(null, @"C:\temp\whprepro\HyperlightSurrogate.exe", si, si, false, OS.CreateProcessFlags.CREATE_SUSPENDED, IntPtr.Zero, null, psi, out OS.ProcessInformation pi);
        int error = Marshal.GetLastWin32Error();
        if (error != 0)
        {
            Console.WriteLine("Error: {0}", error);
            return ((IntPtr)0, (IntPtr)0);
        }
        hProcess = pi.hProcess;
        WindowsHypervisorPlatform.WHvMapGpaRange2(hPartition, pi.hProcess, sourceAddress, guestAddress, size, WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagRead | WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagWrite | WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagExecute);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception calling WhVMapGpaRange: {ex}");
    }

    //Console.WriteLine($"Created partition  {hPartition}");
    return (hPartition, hProcess);
}
    var numPartitions = 512;
    IntPtr[] hPartition = new IntPtr[numPartitions];
    IntPtr[] memAddress = new IntPtr[numPartitions];
    SafeProcessHandle[] hProcess = new SafeProcessHandle[numPartitions];
    ulong size = 0x100000;
    IntPtr jobHandle = IntPtr.Zero;

try
{

    var si = new OS.SecurityAttributes();
    jobHandle = OS.CreateJobObject(si, "HyperlightSurrogateJob");
    var jobLimitInfo = new OS.JobBasicLimitInfo
    {
        LimitFlags = 0x2000
    };
    var extendedJobInfo = new OS.JobExtentedLimitInfo
    {
        BasicLimitInformation = jobLimitInfo
    };

    var length = Marshal.SizeOf<OS.JobExtentedLimitInfo>();
    var extendedInfoPtr = Marshal.AllocHGlobal(length);
    Marshal.StructureToPtr<OS.JobExtentedLimitInfo>(extendedJobInfo, extendedInfoPtr, false);
    OS.SetInformationJobObject(jobHandle, OS.JobObjectInfoType.ExtendedLimitInformation, extendedInfoPtr, (uint)length);
    Marshal.FreeHGlobal(extendedInfoPtr);


    var stopWatch = Stopwatch.StartNew();
    for (int i = 0; i < numPartitions; i++)
    {
        var p = IntPtr.Zero;
        memAddress[i] = (IntPtr)(0x200000 + ((ulong)i * size));
        Console.WriteLine($"Creating Partition {i}");
        (hPartition[i], p) = CreateWHPPartition(memAddress[i], size);
        hProcess[i] = new SafeProcessHandle(p, true);
        OS.AssignProcessToJobObject(jobHandle, p);
    }
    stopWatch.Stop();
    var elapsed = stopWatch.Elapsed;
    Console.WriteLine($"Created  {numPartitions} WHP Partitions in {elapsed.TotalSeconds:00}.{elapsed.Milliseconds:000}{elapsed.Ticks / 10 % 1000:000} seconds");

    Thread.Sleep(5000);
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex}");
}
finally
{
    Thread.Sleep(5000);
    for (int i = 0; i < numPartitions; i++)
    {
        WindowsHypervisorPlatform.WHvDeletePartition(hPartition[i]);

        if (memAddress[i] != IntPtr.Zero)
        {
            OS.Free(memAddress[i], size);
        }
        if (hProcess[i] != null)
        {
            var pHandle = hProcess[i].DangerousGetHandle();
            OS.TerminateProcess(pHandle, 0);
            hProcess[i].Dispose();
        }
    }

    OS.CloseHandle(jobHandle);
}