using whprepro;

static IntPtr CreateWHPPartition(IntPtr sourceAddress, ulong size)
{
    // IntPtr hPartition;;
    IntPtr guestAddress = (IntPtr)0;
    WindowsHypervisorPlatform.WHvCreatePartition(out IntPtr hPartition);
    WindowsHypervisorPlatform.SetProcessorCount(hPartition, 1);
    WindowsHypervisorPlatform.WHvSetupPartition(hPartition);

    try
    {
        WindowsHypervisorPlatform.WHvMapGpaRange(hPartition, sourceAddress, guestAddress, size, WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagRead | WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagWrite | WindowsHypervisorPlatform.WHV_MAP_GPA_RANGE_FLAGS.WHvMapGpaRangeFlagExecute);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception calling WhVMapGpaRange: {ex}");
    }

    Console.WriteLine($"Created partition  {hPartition}");
    return hPartition;
}
var hPartition1 = CreateWHPPartition((IntPtr)0x200000, 1024 * 1024);
var hPartition2 = CreateWHPPartition((IntPtr)0x200000, 1024 * 1024);
WindowsHypervisorPlatform.WHvDeletePartition(hPartition1);
WindowsHypervisorPlatform.WHvDeletePartition(hPartition2);
