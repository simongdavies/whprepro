using System;
using System.Runtime.InteropServices;

namespace whprepro
{
    static class OS
    {
        //////////////////////////////////////////////////////////////////////
        // Windows memory management functions

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }
        [Flags]
        public enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08,
            GUARD_Modifierflag = 0x100,
            NOCACHE_Modifierflag = 0x200,
            WRITECOMBINE_Modifierflag = 0x400
        }

        // Wimdows Create Process Types
        [StructLayout(LayoutKind.Sequential)]
        public class SecurityAttributes
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor = IntPtr.Zero;
            public bool bInheritHandle = false;
            public SecurityAttributes()
            {
                nLength = Marshal.SizeOf(this);
            }
        }

        [Flags]
        public enum CreateProcessFlags : uint
        {
            CREATE_SUSPENDED = 0x00000004,
        }

        [StructLayout(LayoutKind.Sequential)]
        public class StartupInfo
        {
            public int cb;
            public IntPtr lpReserved = IntPtr.Zero;
            public IntPtr lpDesktop = IntPtr.Zero;
            public IntPtr lpTitle = IntPtr.Zero;
            public int dwX = 0;
            public int dwY = 0;
            public int dwXSize = 0;
            public int dwYSize = 0;
            public int dwXCountChars = 0;
            public int dwYCountChars = 0;
            public int dwFillAttributes = 0;
            public int dwFlags = 0;
            public short wShowWindow = 0;
            public short cbReserved = 0;
            public IntPtr lpReserved2 = IntPtr.Zero;
            public IntPtr hStdInput = IntPtr.Zero;
            public IntPtr hStdOutput = IntPtr.Zero;
            public IntPtr hStdErr = IntPtr.Zero;
            public StartupInfo()
            {
                this.cb = Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessInformation
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CreateProcess(string? lpApplicationName, string lpCommandLine, SecurityAttributes lpProcessAttributes, SecurityAttributes lpThreadAttributes, bool bInheritHandles, CreateProcessFlags dwCreationFlags, IntPtr lpEnvironment, string? lpCurrentDirectory, [In] StartupInfo lpStartupInfo, out ProcessInformation lpProcessInformation);


        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool TerminateProcess(IntPtr hProcess, uint ExitCode);

        // Job Object APIs and Types

        [StructLayout(LayoutKind.Sequential)]
        public struct IoCounters
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct JobBasicLimitInfo
        {
            public long PerProcessUserTimeLimit;
            public long PerJobUserTimeLimit;
            public uint LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public uint ActiveProcessLimit;
            public UIntPtr Affinity;
            public uint PriorityClass;
            public uint SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct JobExtentedLimitInfo
        {
            public JobBasicLimitInfo BasicLimitInformation;
            public IoCounters IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        }

        public enum JobObjectInfoType
        {
            AssociateCompletionPortInformation = 7,
            BasicLimitInformation = 2,
            BasicUIRestrictions = 4,
            EndOfJobTimeInformation = 6,
            ExtendedLimitInformation = 9,
            SecurityLimitInformation = 5,
            GroupInformation = 11
        }


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateJobObject(SecurityAttributes jobSecurityAttributes, string lpName);

        [DllImport("kernel32.dll")]
        public static extern bool SetInformationJobObject(IntPtr hJob, JobObjectInfoType infoType, IntPtr lpJobObjectInfo, UInt32 cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int VirtualFree(IntPtr lpAddress, IntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, OS.MemoryProtection flNewProtect, out OS.MemoryProtection lpflOldProtect);


        //////////////////////////////////////////////////////////////////////
        // Linux memory management functions

        public const int PROT_READ = 0x1; /* page can be read */
        public const int PROT_WRITE = 0x2; /* page can be written */
        public const int PROT_EXEC = 0x4; /* page can be executed */
        public const int PROT_SEM = 0x8; /* page may be used for atomic ops */
        public const int PROT_NONE = 0x0; /* page can not be accessed */

        public const int MAP_SHARED = 0x01; /* Share changes */
        public const int MAP_PRIVATE = 0x02; /* Changes are private */
        public const int MAP_ANONYMOUS = 0x20;/* don't use a file */

        [DllImport("libc", SetLastError = true)]
        public static extern IntPtr mmap(IntPtr addr, ulong length, int prot, int flags, int fd, ulong offset);

        [DllImport("libc", SetLastError = true)]
        public static extern IntPtr munmap(IntPtr addr, ulong length);

        public static IntPtr Allocate(IntPtr addr, ulong size)
        {
            IntPtr memPtr;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                memPtr = OS.mmap(addr, size, OS.PROT_READ | OS.PROT_WRITE | OS.PROT_EXEC, OS.MAP_SHARED | OS.MAP_ANONYMOUS, -1, 0);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                memPtr = OS.VirtualAlloc(addr, (IntPtr)size - 1, OS.AllocationType.Commit | OS.AllocationType.Reserve, OS.MemoryProtection.EXECUTE_READWRITE);
                
            }
            else
            {
                throw new NotSupportedException();
            }

            if (IntPtr.Zero == memPtr)
            {
                var err = Marshal.GetLastWin32Error();
                throw new Exception($"Failed to allocate memory. Error code: {err}");
            }

            return memPtr;
        }

        public static void Free(IntPtr addr, ulong size)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                OS.munmap(addr, size);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // TODO: Handle error
                _ = OS.VirtualFree(addr, IntPtr.Zero, (uint)AllocationType.Release);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
