using System;
using System.Runtime.InteropServices;

namespace whprepro
{
    static class WindowsHypervisorPlatform
    {
        public enum WHV_CAPABILITY_CODE
        {
            // Capabilities of the API implementation
            WHvCapabilityCodeHypervisorPresent = 0x00000000,
            WHvCapabilityCodeFeatures = 0x00000001,
            WHvCapabilityCodeExtendedVmExits = 0x00000002,

            // Capabilities of the system's processor
            WHvCapabilityCodeProcessorVendor = 0x00001000,
            WHvCapabilityCodeProcessorFeatures = 0x00001001,
            WHvCapabilityCodeProcessorClFlushSize = 0x00001002,
            WHvCapabilityCodeProcessorXsaveFeatures = 0x00001003,
        }

        public enum WHV_PARTITION_PROPERTY_CODE
        {
            WHvPartitionPropertyCodeExtendedVmExits = 0x00000001,
            WHvPartitionPropertyCodeExceptionExitBitmap = 0x00000002,
            WHvPartitionPropertyCodeSeparateSecurityDomain = 0x00000003,

            WHvPartitionPropertyCodeProcessorFeatures = 0x00001001,
            WHVPartitionPropertyCodeProcessorClFlushSize = 0x00001002,
            WHvPartitionPropertyCodeCpuidExitList = 0x00001003,
            WHvPartitionPropertyCodeCpuidResultList = 0x00001004,
            WHvPartitionPropertyCodeLocalApicEmulationMode = 0x00001005,
            WHvPartitionPropertyCodeProcessorXsaveFeatures = 0x00001006,

            WHvPartitionPropertyCodeProcessorCount = 0x00001fff
        }

        public enum WHV_REGISTER_NAME : uint
        {
            // X64 General purpose registers
            WHvX64RegisterRax = 0x00000000,
            WHvX64RegisterRcx = 0x00000001,
            WHvX64RegisterRdx = 0x00000002,
            WHvX64RegisterRbx = 0x00000003,
            WHvX64RegisterRsp = 0x00000004,
            WHvX64RegisterRbp = 0x00000005,
            WHvX64RegisterRsi = 0x00000006,
            WHvX64RegisterRdi = 0x00000007,
            WHvX64RegisterR8 = 0x00000008,
            WHvX64RegisterR9 = 0x00000009,
            WHvX64RegisterR10 = 0x0000000A,
            WHvX64RegisterR11 = 0x0000000B,
            WHvX64RegisterR12 = 0x0000000C,
            WHvX64RegisterR13 = 0x0000000D,
            WHvX64RegisterR14 = 0x0000000E,
            WHvX64RegisterR15 = 0x0000000F,
            WHvX64RegisterRip = 0x00000010,
            WHvX64RegisterRflags = 0x00000011,

            // X64 Segment registers
            WHvX64RegisterEs = 0x00000012,
            WHvX64RegisterCs = 0x00000013,
            WHvX64RegisterSs = 0x00000014,
            WHvX64RegisterDs = 0x00000015,
            WHvX64RegisterFs = 0x00000016,
            WHvX64RegisterGs = 0x00000017,
            WHvX64RegisterLdtr = 0x00000018,
            WHvX64RegisterTr = 0x00000019,

            // X64 Table registers
            WHvX64RegisterIdtr = 0x0000001A,
            WHvX64RegisterGdtr = 0x0000001B,

            // X64 Control Registers
            WHvX64RegisterCr0 = 0x0000001C,
            WHvX64RegisterCr2 = 0x0000001D,
            WHvX64RegisterCr3 = 0x0000001E,
            WHvX64RegisterCr4 = 0x0000001F,
            WHvX64RegisterCr8 = 0x00000020,

            // X64 Debug Registers
            WHvX64RegisterDr0 = 0x00000021,
            WHvX64RegisterDr1 = 0x00000022,
            WHvX64RegisterDr2 = 0x00000023,
            WHvX64RegisterDr3 = 0x00000024,
            WHvX64RegisterDr6 = 0x00000025,
            WHvX64RegisterDr7 = 0x00000026,

            // X64 Extended Control Registers
            WHvX64RegisterXCr0 = 0x00000027,

            // X64 Floating Point and Vector Registers
            WHvX64RegisterXmm0 = 0x00001000,
            WHvX64RegisterXmm1 = 0x00001001,
            WHvX64RegisterXmm2 = 0x00001002,
            WHvX64RegisterXmm3 = 0x00001003,
            WHvX64RegisterXmm4 = 0x00001004,
            WHvX64RegisterXmm5 = 0x00001005,
            WHvX64RegisterXmm6 = 0x00001006,
            WHvX64RegisterXmm7 = 0x00001007,
            WHvX64RegisterXmm8 = 0x00001008,
            WHvX64RegisterXmm9 = 0x00001009,
            WHvX64RegisterXmm10 = 0x0000100A,
            WHvX64RegisterXmm11 = 0x0000100B,
            WHvX64RegisterXmm12 = 0x0000100C,
            WHvX64RegisterXmm13 = 0x0000100D,
            WHvX64RegisterXmm14 = 0x0000100E,
            WHvX64RegisterXmm15 = 0x0000100F,
            WHvX64RegisterFpMmx0 = 0x00001010,
            WHvX64RegisterFpMmx1 = 0x00001011,
            WHvX64RegisterFpMmx2 = 0x00001012,
            WHvX64RegisterFpMmx3 = 0x00001013,
            WHvX64RegisterFpMmx4 = 0x00001014,
            WHvX64RegisterFpMmx5 = 0x00001015,
            WHvX64RegisterFpMmx6 = 0x00001016,
            WHvX64RegisterFpMmx7 = 0x00001017,
            WHvX64RegisterFpControlStatus = 0x00001018,
            WHvX64RegisterXmmControlStatus = 0x00001019,

            // X64 MSRs
            WHvX64RegisterTsc = 0x00002000,
            WHvX64RegisterEfer = 0x00002001,
            WHvX64RegisterKernelGsBase = 0x00002002,
            WHvX64RegisterApicBase = 0x00002003,
            WHvX64RegisterPat = 0x00002004,
            WHvX64RegisterSysenterCs = 0x00002005,
            WHvX64RegisterSysenterEip = 0x00002006,
            WHvX64RegisterSysenterEsp = 0x00002007,
            WHvX64RegisterStar = 0x00002008,
            WHvX64RegisterLstar = 0x00002009,
            WHvX64RegisterCstar = 0x0000200A,
            WHvX64RegisterSfmask = 0x0000200B,

            WHvX64RegisterMsrMtrrCap = 0x0000200D,
            WHvX64RegisterMsrMtrrDefType = 0x0000200E,

            WHvX64RegisterMsrMtrrPhysBase0 = 0x00002010,
            WHvX64RegisterMsrMtrrPhysBase1 = 0x00002011,
            WHvX64RegisterMsrMtrrPhysBase2 = 0x00002012,
            WHvX64RegisterMsrMtrrPhysBase3 = 0x00002013,
            WHvX64RegisterMsrMtrrPhysBase4 = 0x00002014,
            WHvX64RegisterMsrMtrrPhysBase5 = 0x00002015,
            WHvX64RegisterMsrMtrrPhysBase6 = 0x00002016,
            WHvX64RegisterMsrMtrrPhysBase7 = 0x00002017,
            WHvX64RegisterMsrMtrrPhysBase8 = 0x00002018,
            WHvX64RegisterMsrMtrrPhysBase9 = 0x00002019,
            WHvX64RegisterMsrMtrrPhysBaseA = 0x0000201A,
            WHvX64RegisterMsrMtrrPhysBaseB = 0x0000201B,
            WHvX64RegisterMsrMtrrPhysBaseC = 0x0000201C,
            WHvX64RegisterMsrMtrrPhysBaseD = 0x0000201D,
            WHvX64RegisterMsrMtrrPhysBaseE = 0x0000201E,
            WHvX64RegisterMsrMtrrPhysBaseF = 0x0000201F,

            WHvX64RegisterMsrMtrrPhysMask0 = 0x00002040,
            WHvX64RegisterMsrMtrrPhysMask1 = 0x00002041,
            WHvX64RegisterMsrMtrrPhysMask2 = 0x00002042,
            WHvX64RegisterMsrMtrrPhysMask3 = 0x00002043,
            WHvX64RegisterMsrMtrrPhysMask4 = 0x00002044,
            WHvX64RegisterMsrMtrrPhysMask5 = 0x00002045,
            WHvX64RegisterMsrMtrrPhysMask6 = 0x00002046,
            WHvX64RegisterMsrMtrrPhysMask7 = 0x00002047,
            WHvX64RegisterMsrMtrrPhysMask8 = 0x00002048,
            WHvX64RegisterMsrMtrrPhysMask9 = 0x00002049,
            WHvX64RegisterMsrMtrrPhysMaskA = 0x0000204A,
            WHvX64RegisterMsrMtrrPhysMaskB = 0x0000204B,
            WHvX64RegisterMsrMtrrPhysMaskC = 0x0000204C,
            WHvX64RegisterMsrMtrrPhysMaskD = 0x0000204D,
            WHvX64RegisterMsrMtrrPhysMaskE = 0x0000204E,
            WHvX64RegisterMsrMtrrPhysMaskF = 0x0000204F,

            WHvX64RegisterMsrMtrrFix64k00000 = 0x00002070,
            WHvX64RegisterMsrMtrrFix16k80000 = 0x00002071,
            WHvX64RegisterMsrMtrrFix16kA0000 = 0x00002072,
            WHvX64RegisterMsrMtrrFix4kC0000 = 0x00002073,
            WHvX64RegisterMsrMtrrFix4kC8000 = 0x00002074,
            WHvX64RegisterMsrMtrrFix4kD0000 = 0x00002075,
            WHvX64RegisterMsrMtrrFix4kD8000 = 0x00002076,
            WHvX64RegisterMsrMtrrFix4kE0000 = 0x00002077,
            WHvX64RegisterMsrMtrrFix4kE8000 = 0x00002078,
            WHvX64RegisterMsrMtrrFix4kF0000 = 0x00002079,
            WHvX64RegisterMsrMtrrFix4kF8000 = 0x0000207A,

            WHvX64RegisterTscAux = 0x0000207B,
            WHvX64RegisterSpecCtrl = 0x00002084,
            WHvX64RegisterPredCmd = 0x00002085,
            WHvX64RegisterTscVirtualOffset = 0x00002087,

            // APIC state (also accessible via WHv(Get/Set)VirtualProcessorInterruptControllerState)
            WHvX64RegisterApicId = 0x00003002,
            WHvX64RegisterApicVersion = 0x00003003,

            // Interrupt / Event Registers
            WHvRegisterPendingInterruption = 0x80000000,
            WHvRegisterInterruptState = 0x80000001,
            WHvRegisterPendingEvent = 0x80000002,
            WHvX64RegisterDeliverabilityNotifications = 0x80000004,
            WHvRegisterInternalActivityState = 0x80000005,

        }

        [Flags]
        public enum WHV_MAP_GPA_RANGE_FLAGS
        {
            WHvMapGpaRangeFlagNone = 0x00000000,
            WHvMapGpaRangeFlagRead = 0x00000001,
            WHvMapGpaRangeFlagWrite = 0x00000002,
            WHvMapGpaRangeFlagExecute = 0x00000004,
            WHvMapGpaRangeFlagTrackDirtyPages = 0x00000008,
        }
        public enum WHV_RUN_VP_EXIT_REASON
        {
            WHvRunVpExitReasonNone = 0x00000000,

            // Standard exits caused by operations of the virtual processor
            WHvRunVpExitReasonMemoryAccess = 0x00000001,
            WHvRunVpExitReasonX64IoPortAccess = 0x00000002,
            WHvRunVpExitReasonUnrecoverableException = 0x00000004,
            WHvRunVpExitReasonInvalidVpRegisterValue = 0x00000005,
            WHvRunVpExitReasonUnsupportedFeature = 0x00000006,
            WHvRunVpExitReasonX64InterruptWindow = 0x00000007,
            WHvRunVpExitReasonX64Halt = 0x00000008,
            WHvRunVpExitReasonX64ApicEoi = 0x00000009,

            // Additional exits that can be configured through partition properties
            WHvRunVpExitReasonX64MsrAccess = 0x00001000,
            WHvRunVpExitReasonX64Cpuid = 0x00001001,
            WHvRunVpExitReasonException = 0x00001002,

            // Exits caused by the host
            WHvRunVpExitReasonCanceled = 0x00002001
        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MyUInt128
        {
            public ulong low;
            public ulong high;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WHV_X64_SEGMENT_REGISTER
        {
            public ulong Base;
            public uint Limit;
            public ushort Selector;
            public ushort Attributes;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WHV_VP_EXIT_CONTEXT
        {
            public ushort ExecutionState;
            public ushort InstructionLength_Cr8_Reserved;
            public uint Reserved2;
            public WHV_X64_SEGMENT_REGISTER Cs;
            public ulong Rip;
            public ulong Rflags;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WHV_X64_IO_PORT_ACCESS_CONTEXT
        {
            // Context of the virtual processor
            public byte InstructionByteCount;
            public byte InstructionBytes_00;
            public byte InstructionBytes_01;
            public byte InstructionBytes_02;
            public byte InstructionBytes_03;
            public byte InstructionBytes_04;
            public byte InstructionBytes_05;
            public byte InstructionBytes_06;
            public byte InstructionBytes_07;
            public byte InstructionBytes_08;
            public byte InstructionBytes_09;
            public byte InstructionBytes_0A;
            public byte InstructionBytes_0B;
            public byte InstructionBytes_0C;
            public byte InstructionBytes_0D;
            public byte InstructionBytes_0E;
            public byte InstructionBytes_0F;
            public byte InstructionByte_res1;
            public byte InstructionByte_res2;
            public byte InstructionByte_res3;

            // I/O port access info
            public uint AccessInfo;
            public ushort PortNumber;
            public ushort PortNumber_res1;
            public ushort PortNumber_res2;
            public ushort PortNumber_res3;
            public ulong Rax;
            public ulong Rcx;
            public ulong Rsi;
            public ulong Rdi;
            public WHV_X64_SEGMENT_REGISTER Ds;
            public WHV_X64_SEGMENT_REGISTER Es;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WHV_RUN_VP_EXIT_CONTEXT
        {
            public WHV_RUN_VP_EXIT_REASON ExitReason;
            public uint Reserved;
            public WHV_VP_EXIT_CONTEXT VpContext;
            public WHV_X64_IO_PORT_ACCESS_CONTEXT IoPortAccess;
            public ulong padding00;
            public ulong padding01;
            public ulong padding02;
            public ulong padding03;
            public ulong padding04;
            public ulong padding05;
            public ulong padding06;
            public ulong padding07;
            public ulong padding08;
            public ulong padding09;
            public ulong padding0a;
            public ulong padding0b;
            public ulong padding0c;
            public ulong padding0d;
            public ulong padding0e;
            public ulong padding0f;
        }

        // Overload that returns a uint sized structure.  It is expected that this overload will be called with CapabilityBufferSizeInBytes == 4
        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        static extern void WHvGetCapability(WHV_CAPABILITY_CODE capabilityCode, [Out] out uint capabilityBuffer, uint capabilityBufferSizeInBytes, [Out] out uint writtenSizeInBytes);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvCreatePartition([Out] out IntPtr hPartition);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvDeletePartition(IntPtr hPartition);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        static extern void WHvSetPartitionProperty(IntPtr hPartition, WHV_PARTITION_PROPERTY_CODE propertyCode, [In][Out] ref uint propertyBuffer, uint propertyBufferSizeInBytes);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvSetupPartition(IntPtr hPartition);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvMapGpaRange(IntPtr hPartition, IntPtr sourceAddress, IntPtr guestAddress, ulong sizeInBytes, WHV_MAP_GPA_RANGE_FLAGS flags);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvCreateVirtualProcessor(IntPtr hPartition, uint vpIndex, uint flags);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvRunVirtualProcessor(IntPtr hPartition, uint vpIndex, [Out] out WHV_RUN_VP_EXIT_CONTEXT exitContext, uint exitContextSizeInBytes);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvRunVirtualProcessor(IntPtr hPartition, uint vpIndex, IntPtr exitContext, uint exitContextSizeInBytes);

        [DllImport("WinHvPlatform.dll")]
        public static extern uint WHvDeleteVirtualProcessor(IntPtr hPartition, uint vpIndex);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvSetVirtualProcessorRegisters(IntPtr hPartition, uint vpIndex, WHV_REGISTER_NAME[] registerNames, uint registerCount, MyUInt128[] registerValues);

        [DllImport("WinHvPlatform.dll", PreserveSig = false)]
        public static extern void WHvGetVirtualProcessorRegisters(IntPtr hPartition, uint vpIndex, WHV_REGISTER_NAME[] registerNames, uint registerCount, [Out] MyUInt128[] registerValues);


        public static bool IsHypervisorPresent()
        {
            WindowsHypervisorPlatform.WHvGetCapability(WHV_CAPABILITY_CODE.WHvCapabilityCodeHypervisorPresent, out var hypervisorPresent, sizeof(uint), out _);
            return hypervisorPresent == 1;
        }

        public static void SetProcessorCount(IntPtr hPartition, uint processorCount)
        {
            WHvSetPartitionProperty(hPartition, WHV_PARTITION_PROPERTY_CODE.WHvPartitionPropertyCodeProcessorCount, ref processorCount, sizeof(uint));
        }
    }
}
