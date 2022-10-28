using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//EMULATE TARGET FUNCTION THAT DOES RC4 decryption and then executes binary.
//Odds of being the output payload are very likely specially since its all over the command handler.


//Emulate all conditions that satisfy flared_54

class FLARE09
{
    public FLARE09(string f)
    {
        FileStream fileStream = new FileStream(f, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        dosHeader = FromBinaryReader<IMAGE_DOS_HEADER>(binaryReader);
        fileStream.Seek((long)((ulong)dosHeader.e_lfanew), SeekOrigin.Begin);
        uint num = binaryReader.ReadUInt32();
        fileHeader = FromBinaryReader<IMAGE_FILE_HEADER>(binaryReader);
        optionalHeader32 = FromBinaryReader<IMAGE_OPTIONAL_HEADER32>(binaryReader);
        imageSectionHeaders = new IMAGE_SECTION_HEADER[(int)fileHeader.NumberOfSections];
        for (int i = 0; i < imageSectionHeaders.Length; i++)
        {
            imageSectionHeaders[i] = FromBinaryReader<IMAGE_SECTION_HEADER>(binaryReader);
        }
    }

    // Token: 0x06000053 RID: 83 RVA: 0x00003C44 File Offset: 0x00001E44
    public static T FromBinaryReader<T>(BinaryReader reader)
    {
        byte[] value = reader.ReadBytes(Marshal.SizeOf(typeof(T)));
        GCHandle gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
        T result = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
        gchandle.Free();
        return result;
    }

    public bool Is32BitHeader
    {
        get
        {
            ushort num = 256;
            return (num & this.FileHeader.Characteristics) == num;
        }
    }

    // Token: 0x17000002 RID: 2
    // (get) Token: 0x06000055 RID: 85 RVA: 0x00003CC4 File Offset: 0x00001EC4
    public IMAGE_FILE_HEADER FileHeader
    {
        get
        {
            return fileHeader;
        }
    }

    // Token: 0x17000003 RID: 3
    // (get) Token: 0x06000056 RID: 86 RVA: 0x00003CDC File Offset: 0x00001EDC
    public IMAGE_OPTIONAL_HEADER32 OptionalHeader32
    {
        get
        {
            return optionalHeader32;
        }
    }

    // Token: 0x17000004 RID: 4
    // (get) Token: 0x06000057 RID: 87 RVA: 0x00003CF4 File Offset: 0x00001EF4
    public IMAGE_OPTIONAL_HEADER64 OptionalHeader64
    {
        get
        {
            return optionalHeader64;
        }
    }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x06000058 RID: 88 RVA: 0x00003D0C File Offset: 0x00001F0C
    public IMAGE_SECTION_HEADER[] ImageSectionHeaders
    {
        get
        {
            return imageSectionHeaders;
        }
    }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x06000059 RID: 89 RVA: 0x00003D24 File Offset: 0x00001F24
    public DateTime TimeStamp
    {
        get
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
            dateTime = dateTime.AddSeconds(fileHeader.TimeDateStamp);
            dateTime += TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
            return dateTime;
        }
    }

    // Token: 0x0400002D RID: 45
    public static IMAGE_DOS_HEADER dosHeader;

    // Token: 0x0400002E RID: 46
    public static IMAGE_FILE_HEADER fileHeader;

    // Token: 0x0400002F RID: 47
    public static IMAGE_OPTIONAL_HEADER32 optionalHeader32;

    // Token: 0x04000030 RID: 48
    public static IMAGE_OPTIONAL_HEADER64 optionalHeader64;

    // Token: 0x04000031 RID: 49
    public static IMAGE_SECTION_HEADER[] imageSectionHeaders;

    // Token: 0x0200001E RID: 30
    public struct IMAGE_DOS_HEADER
    {
        // Token: 0x04000081 RID: 129
        public ushort e_magic;

        // Token: 0x04000082 RID: 130
        public ushort e_cblp;

        // Token: 0x04000083 RID: 131
        public ushort e_cp;

        // Token: 0x04000084 RID: 132
        public ushort e_crlc;

        // Token: 0x04000085 RID: 133
        public ushort e_cparhdr;

        // Token: 0x04000086 RID: 134
        public ushort e_minalloc;

        // Token: 0x04000087 RID: 135
        public ushort e_maxalloc;

        // Token: 0x04000088 RID: 136
        public ushort e_ss;

        // Token: 0x04000089 RID: 137
        public ushort e_sp;

        // Token: 0x0400008A RID: 138
        public ushort e_csum;

        // Token: 0x0400008B RID: 139
        public ushort e_ip;

        // Token: 0x0400008C RID: 140
        public ushort e_cs;

        // Token: 0x0400008D RID: 141
        public ushort e_lfarlc;

        // Token: 0x0400008E RID: 142
        public ushort e_ovno;

        // Token: 0x0400008F RID: 143
        public ushort e_res_0;

        // Token: 0x04000090 RID: 144
        public ushort e_res_1;

        // Token: 0x04000091 RID: 145
        public ushort e_res_2;

        // Token: 0x04000092 RID: 146
        public ushort e_res_3;

        // Token: 0x04000093 RID: 147
        public ushort e_oemid;

        // Token: 0x04000094 RID: 148
        public ushort e_oeminfo;

        // Token: 0x04000095 RID: 149
        public ushort e_res2_0;

        // Token: 0x04000096 RID: 150
        public ushort e_res2_1;

        // Token: 0x04000097 RID: 151
        public ushort e_res2_2;

        // Token: 0x04000098 RID: 152
        public ushort e_res2_3;

        // Token: 0x04000099 RID: 153
        public ushort e_res2_4;

        // Token: 0x0400009A RID: 154
        public ushort e_res2_5;

        // Token: 0x0400009B RID: 155
        public ushort e_res2_6;

        // Token: 0x0400009C RID: 156
        public ushort e_res2_7;

        // Token: 0x0400009D RID: 157
        public ushort e_res2_8;

        // Token: 0x0400009E RID: 158
        public ushort e_res2_9;

        // Token: 0x0400009F RID: 159
        public uint e_lfanew;
    }

    // Token: 0x0200001F RID: 31
    public struct IMAGE_DATA_DIRECTORY
    {
        // Token: 0x040000A0 RID: 160
        public uint VirtualAddress;

        // Token: 0x040000A1 RID: 161
        public uint Size;
    }

    // Token: 0x02000020 RID: 32
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IMAGE_OPTIONAL_HEADER32
    {
        // Token: 0x040000A2 RID: 162
        public ushort Magic;

        // Token: 0x040000A3 RID: 163
        public byte MajorLinkerVersion;

        // Token: 0x040000A4 RID: 164
        public byte MinorLinkerVersion;

        // Token: 0x040000A5 RID: 165
        public uint SizeOfCode;

        // Token: 0x040000A6 RID: 166
        public uint SizeOfInitializedData;

        // Token: 0x040000A7 RID: 167
        public uint SizeOfUninitializedData;

        // Token: 0x040000A8 RID: 168
        public uint AddressOfEntryPoint;

        // Token: 0x040000A9 RID: 169
        public uint BaseOfCode;

        // Token: 0x040000AA RID: 170
        public uint BaseOfData;

        // Token: 0x040000AB RID: 171
        public uint ImageBase;

        // Token: 0x040000AC RID: 172
        public uint SectionAlignment;

        // Token: 0x040000AD RID: 173
        public uint FileAlignment;

        // Token: 0x040000AE RID: 174
        public ushort MajorOperatingSystemVersion;

        // Token: 0x040000AF RID: 175
        public ushort MinorOperatingSystemVersion;

        // Token: 0x040000B0 RID: 176
        public ushort MajorImageVersion;

        // Token: 0x040000B1 RID: 177
        public ushort MinorImageVersion;

        // Token: 0x040000B2 RID: 178
        public ushort MajorSubsystemVersion;

        // Token: 0x040000B3 RID: 179
        public ushort MinorSubsystemVersion;

        // Token: 0x040000B4 RID: 180
        public uint Win32VersionValue;

        // Token: 0x040000B5 RID: 181
        public uint SizeOfImage;

        // Token: 0x040000B6 RID: 182
        public uint SizeOfHeaders;

        // Token: 0x040000B7 RID: 183
        public uint CheckSum;

        // Token: 0x040000B8 RID: 184
        public ushort Subsystem;

        // Token: 0x040000B9 RID: 185
        public ushort DllCharacteristics;

        // Token: 0x040000BA RID: 186
        public uint SizeOfStackReserve;

        // Token: 0x040000BB RID: 187
        public uint SizeOfStackCommit;

        // Token: 0x040000BC RID: 188
        public uint SizeOfHeapReserve;

        // Token: 0x040000BD RID: 189
        public uint SizeOfHeapCommit;

        // Token: 0x040000BE RID: 190
        public uint LoaderFlags;

        // Token: 0x040000BF RID: 191
        public uint NumberOfRvaAndSizes;

        // Token: 0x040000C0 RID: 192
        public IMAGE_DATA_DIRECTORY ExportTable;

        // Token: 0x040000C1 RID: 193
        public IMAGE_DATA_DIRECTORY ImportTable;

        // Token: 0x040000C2 RID: 194
        public IMAGE_DATA_DIRECTORY ResourceTable;

        // Token: 0x040000C3 RID: 195
        public IMAGE_DATA_DIRECTORY ExceptionTable;

        // Token: 0x040000C4 RID: 196
        public IMAGE_DATA_DIRECTORY CertificateTable;

        // Token: 0x040000C5 RID: 197
        public IMAGE_DATA_DIRECTORY BaseRelocationTable;

        // Token: 0x040000C6 RID: 198
        public IMAGE_DATA_DIRECTORY Debug;

        // Token: 0x040000C7 RID: 199
        public IMAGE_DATA_DIRECTORY Architecture;

        // Token: 0x040000C8 RID: 200
        public IMAGE_DATA_DIRECTORY GlobalPtr;

        // Token: 0x040000C9 RID: 201
        public IMAGE_DATA_DIRECTORY TLSTable;

        // Token: 0x040000CA RID: 202
        public IMAGE_DATA_DIRECTORY LoadConfigTable;

        // Token: 0x040000CB RID: 203
        public IMAGE_DATA_DIRECTORY BoundImport;

        // Token: 0x040000CC RID: 204
        public IMAGE_DATA_DIRECTORY IAT;

        // Token: 0x040000CD RID: 205
        public IMAGE_DATA_DIRECTORY DelayImportDescriptor;

        // Token: 0x040000CE RID: 206
        public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;

        // Token: 0x040000CF RID: 207
        public IMAGE_DATA_DIRECTORY Reserved;
    }

    // Token: 0x02000021 RID: 33
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IMAGE_OPTIONAL_HEADER64
    {
        // Token: 0x040000D0 RID: 208
        public ushort Magic;

        // Token: 0x040000D1 RID: 209
        public byte MajorLinkerVersion;

        // Token: 0x040000D2 RID: 210
        public byte MinorLinkerVersion;

        // Token: 0x040000D3 RID: 211
        public uint SizeOfCode;

        // Token: 0x040000D4 RID: 212
        public uint SizeOfInitializedData;

        // Token: 0x040000D5 RID: 213
        public uint SizeOfUninitializedData;

        // Token: 0x040000D6 RID: 214
        public uint AddressOfEntryPoint;

        // Token: 0x040000D7 RID: 215
        public uint BaseOfCode;

        // Token: 0x040000D8 RID: 216
        public ulong ImageBase;

        // Token: 0x040000D9 RID: 217
        public uint SectionAlignment;

        // Token: 0x040000DA RID: 218
        public uint FileAlignment;

        // Token: 0x040000DB RID: 219
        public ushort MajorOperatingSystemVersion;

        // Token: 0x040000DC RID: 220
        public ushort MinorOperatingSystemVersion;

        // Token: 0x040000DD RID: 221
        public ushort MajorImageVersion;

        // Token: 0x040000DE RID: 222
        public ushort MinorImageVersion;

        // Token: 0x040000DF RID: 223
        public ushort MajorSubsystemVersion;

        // Token: 0x040000E0 RID: 224
        public ushort MinorSubsystemVersion;

        // Token: 0x040000E1 RID: 225
        public uint Win32VersionValue;

        // Token: 0x040000E2 RID: 226
        public uint SizeOfImage;

        // Token: 0x040000E3 RID: 227
        public uint SizeOfHeaders;

        // Token: 0x040000E4 RID: 228
        public uint CheckSum;

        // Token: 0x040000E5 RID: 229
        public ushort Subsystem;

        // Token: 0x040000E6 RID: 230
        public ushort DllCharacteristics;

        // Token: 0x040000E7 RID: 231
        public ulong SizeOfStackReserve;

        // Token: 0x040000E8 RID: 232
        public ulong SizeOfStackCommit;

        // Token: 0x040000E9 RID: 233
        public ulong SizeOfHeapReserve;

        // Token: 0x040000EA RID: 234
        public ulong SizeOfHeapCommit;

        // Token: 0x040000EB RID: 235
        public uint LoaderFlags;

        // Token: 0x040000EC RID: 236
        public uint NumberOfRvaAndSizes;

        // Token: 0x040000ED RID: 237
        public IMAGE_DATA_DIRECTORY ExportTable;

        // Token: 0x040000EE RID: 238
        public IMAGE_DATA_DIRECTORY ImportTable;

        // Token: 0x040000EF RID: 239
        public IMAGE_DATA_DIRECTORY ResourceTable;

        // Token: 0x040000F0 RID: 240
        public IMAGE_DATA_DIRECTORY ExceptionTable;

        // Token: 0x040000F1 RID: 241
        public IMAGE_DATA_DIRECTORY CertificateTable;

        // Token: 0x040000F2 RID: 242
        public IMAGE_DATA_DIRECTORY BaseRelocationTable;

        // Token: 0x040000F3 RID: 243
        public IMAGE_DATA_DIRECTORY Debug;

        // Token: 0x040000F4 RID: 244
        public IMAGE_DATA_DIRECTORY Architecture;

        // Token: 0x040000F5 RID: 245
        public IMAGE_DATA_DIRECTORY GlobalPtr;

        // Token: 0x040000F6 RID: 246
        public IMAGE_DATA_DIRECTORY TLSTable;

        // Token: 0x040000F7 RID: 247
        public IMAGE_DATA_DIRECTORY LoadConfigTable;

        // Token: 0x040000F8 RID: 248
        public IMAGE_DATA_DIRECTORY BoundImport;

        // Token: 0x040000F9 RID: 249
        public IMAGE_DATA_DIRECTORY IAT;

        // Token: 0x040000FA RID: 250
        public IMAGE_DATA_DIRECTORY DelayImportDescriptor;

        // Token: 0x040000FB RID: 251
        public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;

        // Token: 0x040000FC RID: 252
        public IMAGE_DATA_DIRECTORY Reserved;
    }

    // Token: 0x02000022 RID: 34
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IMAGE_FILE_HEADER
    {
        // Token: 0x040000FD RID: 253
        public ushort Machine;

        // Token: 0x040000FE RID: 254
        public ushort NumberOfSections;

        // Token: 0x040000FF RID: 255
        public uint TimeDateStamp;

        // Token: 0x04000100 RID: 256
        public uint PointerToSymbolTable;

        // Token: 0x04000101 RID: 257
        public uint NumberOfSymbols;

        // Token: 0x04000102 RID: 258
        public ushort SizeOfOptionalHeader;

        // Token: 0x04000103 RID: 259
        public ushort Characteristics;
    }

    // Token: 0x02000023 RID: 35
    [StructLayout(LayoutKind.Explicit)]
    public struct IMAGE_SECTION_HEADER
    {
        public string Section
        {
            get
            {
                return new string(this.Name);
            }
        }

        // Token: 0x04000104 RID: 260
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;

        // Token: 0x04000105 RID: 261
        [FieldOffset(8)]
        public uint VirtualSize;

        // Token: 0x04000106 RID: 262
        [FieldOffset(12)]
        public uint VirtualAddress;

        // Token: 0x04000107 RID: 263
        [FieldOffset(16)]
        public uint SizeOfRawData;

        // Token: 0x04000108 RID: 264
        [FieldOffset(20)]
        public uint PointerToRawData;

        // Token: 0x04000109 RID: 265
        [FieldOffset(24)]
        public uint PointerToRelocations;

        // Token: 0x0400010A RID: 266
        [FieldOffset(28)]
        public uint PointerToLinenumbers;

        // Token: 0x0400010B RID: 267
        [FieldOffset(32)]
        public ushort NumberOfRelocations;

        // Token: 0x0400010C RID: 268
        [FieldOffset(34)]
        public ushort NumberOfLinenumbers;

        // Token: 0x0400010D RID: 269
        [FieldOffset(36)]
        public DataSectionFlags Characteristics;
    }

    // Token: 0x02000024 RID: 36
    [Flags]
    public enum DataSectionFlags : uint
    {
        // Token: 0x0400010F RID: 271
        TypeReg = 0U,
        // Token: 0x04000110 RID: 272
        TypeDsect = 1U,
        // Token: 0x04000111 RID: 273
        TypeNoLoad = 2U,
        // Token: 0x04000112 RID: 274
        TypeGroup = 4U,
        // Token: 0x04000113 RID: 275
        TypeNoPadded = 8U,
        // Token: 0x04000114 RID: 276
        TypeCopy = 16U,
        // Token: 0x04000115 RID: 277
        ContentCode = 32U,
        // Token: 0x04000116 RID: 278
        ContentInitializedData = 64U,
        // Token: 0x04000117 RID: 279
        ContentUninitializedData = 128U,
        // Token: 0x04000118 RID: 280
        LinkOther = 256U,
        // Token: 0x04000119 RID: 281
        LinkInfo = 512U,
        // Token: 0x0400011A RID: 282
        TypeOver = 1024U,
        // Token: 0x0400011B RID: 283
        LinkRemove = 2048U,
        // Token: 0x0400011C RID: 284
        LinkComDat = 4096U,
        // Token: 0x0400011D RID: 285
        NoDeferSpecExceptions = 16384U,
        // Token: 0x0400011E RID: 286
        RelativeGP = 32768U,
        // Token: 0x0400011F RID: 287
        MemPurgeable = 131072U,
        // Token: 0x04000120 RID: 288
        Memory16Bit = 131072U,
        // Token: 0x04000121 RID: 289
        MemoryLocked = 262144U,
        // Token: 0x04000122 RID: 290
        MemoryPreload = 524288U,
        // Token: 0x04000123 RID: 291
        Align1Bytes = 1048576U,
        // Token: 0x04000124 RID: 292
        Align2Bytes = 2097152U,
        // Token: 0x04000125 RID: 293
        Align4Bytes = 3145728U,
        // Token: 0x04000126 RID: 294
        Align8Bytes = 4194304U,
        // Token: 0x04000127 RID: 295
        Align16Bytes = 5242880U,
        // Token: 0x04000128 RID: 296
        Align32Bytes = 6291456U,
        // Token: 0x04000129 RID: 297
        Align64Bytes = 7340032U,
        // Token: 0x0400012A RID: 298
        Align128Bytes = 8388608U,
        // Token: 0x0400012B RID: 299
        Align256Bytes = 9437184U,
        // Token: 0x0400012C RID: 300
        Align512Bytes = 10485760U,
        // Token: 0x0400012D RID: 301
        Align1024Bytes = 11534336U,
        // Token: 0x0400012E RID: 302
        Align2048Bytes = 12582912U,
        // Token: 0x0400012F RID: 303
        Align4096Bytes = 13631488U,
        // Token: 0x04000130 RID: 304
        Align8192Bytes = 14680064U,
        // Token: 0x04000131 RID: 305
        LinkExtendedRelocationOverflow = 16777216U,
        // Token: 0x04000132 RID: 306
        MemoryDiscardable = 33554432U,
        // Token: 0x04000133 RID: 307
        MemoryNotCached = 67108864U,
        MemoryNotPaged = 134217728U,
        MemoryShared = 268435456U,
        MemoryExecute = 536870912U,
        MemoryRead = 1073741824U,
        MemoryWrite = 2147483648U
    }

}
/*
 	public static void flared_54()
	{
        //RESOLVE THE BUFFER.
		byte[] d = FLARE15.flare_69(ReverseArr(FLARE14.sh));
        //Requires proper string emulation.
		byte[] hashAndReset = FLARE14.h.GetHashAndReset();
		byte[] array = Rc4Decryption(hashAndReset, d);
		string text = Path.GetTempFileName() + Encoding.UTF8.GetString(FLARE12.Rc4Decryption(hashAndReset, new byte[]
		{
			31,
			29,
			40,
			72
		}));
		FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read);
		fileStream.Write(array, 0, array.Length);
		Process.Start(text);
	}
 */
class Generator
{
    public Generator(string path)
    {
        this.filePath = path;
        this.dictFromBruteforce = new Dictionary<string, string> { {"19", "146"}, { "18" , "939" }, { "16" , "e87" },
            { "15" , "197"}, {"14", "3a7"}, {"10", "f38"}, {"17", "2e4"}, { "13", "e38"},
            {  "12", "570"}, {"11", "818"}, {"4", "ea5"}, {"5", "bfb"}, {"3", "113"}, {"1", "c2e"}, {"7", "b"},
            { "8", "2b7" }, { "9" , "9b2" }, {"2", "d7d"}, {"22", "709"}, {"20", "3c9974"}, {"21", "8e6"}
        };
        this.hashIncrement = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        this.ToBruteForceList = new List<string>();
        this.ToBruteforce = new ObservableCollection<int> { 250, 242, 240, 235, 243, 249, 247, 245, 238, 232, 253, 244, 237, 251, 234, 233, 236, 246, 241, 255, 252 };
        this.reflectedModule = Assembly.LoadFrom(path).Modules.First();
        this.reflectedAssembly = Assembly.LoadFrom(path);
        this.arraysToConcat = new Dictionary<string, string>(){
                {"c2e", "RwBlAHQALQBOAGUAdABJAFAAQQBkAGQAcgBlAHMAcwAgAC0AQQBkAGQAcgBlAHMAcwBGAGEAbQBpAGwAeQAgAEkAUAB2ADQAIAB8ACAAUwBlAGwAZQBjAHQALQBPAGIAagBlAGMAdAAgAEkAUABBAGQAZAByAGUAcwBzAA=="},
                {"d7d", "RwBlAHQALQBOAGUAdABOAGUAaQBnAGgAYgBvAHIAIAAtAEEAZABkAHIAZQBzAHMARgBhAG0AaQBsAHkAIABJAFAAdgA0ACAAfAAgAFMAZQBsAGUAYwB0AC0ATwBiAGoAZQBjAHQAIAAiAEkAUABBAEQARAByAGUAcwBzACIA"},
                {"b", "RwBlAHQALQBDAGgAaQBsAGQASQB0AGUAbQAgAC0AUABhAHQAaAAgACIAQwA6AFwAUAByAG8AZwByAGEAbQAgAEYAaQBsAGUAcwAiACAAfAAgAFMAZQBsAGUAYwB0AC0ATwBiAGoAZQBjAHQAIABOAGEAbQBlAA=="},
                {"2b7", "RwBlAHQALQBDAGgAaQBsAGQASQB0AGUAbQAgAC0AUABhAHQAaAAgACcAQwA6AFwAUAByAG8AZwByAGEAbQAgAEYAaQBsAGUAcwAgACgAeAA4ADYAKQAnACAAfAAgAFMAZQBsAGUAYwB0AC0ATwBiAGoAZQBjAHQAIABOAGEAbQBlAA=="},
                {"9b2", "RwBlAHQALQBDAGgAaQBsAGQASQB0AGUAbQAgAC0AUABhAHQAaAAgACcAQwA6ACcAIAB8ACAAUwBlAGwAZQBjAHQALQBPAGIAagBlAGMAdAAgAE4AYQBtAGUA"},
                {"818","RwBlAHQALQBOAGUAdABUAEMAUABDAG8AbgBuAGUAYwB0AGkAbwBuACAAfAAgAFcAaABlAHIAZQAtAE8AYgBqAGUAYwB0ACAAewAkAF8ALgBTAHQAYQB0AGUAIAAtAGUAcQAgACIARQBzAHQAYQBiAGwAaQBzAGgAZQBkACIAfQAgAHwAIABTAGUAbABlAGMAdAAtAE8AYgBqAGUAYwB0ACAAIgBMAG8AYwBhAGwAQQBkAGQAcgBlAHMAcwAiACwAIAAiAEwAbwBjAGEAbABQAG8AcgB0ACIALAAgACIAUgBlAG0AbwB0AGUAQQBkAGQAcgBlAHMAcwAiACwAIAAiAFIAZQBtAG8AdABlAFAAbwByAHQAIgA="},
                {"570","JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANAAuADUAMAAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANAAuADUAMQAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANgA1AC4ANgA1ACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgA2ADUALgA1ADMALgA1ADMAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA7ACQAKABwAGkAbgBnACAALQBuACAAMQAgADEAMAAuADYANQAuADIAMQAuADIAMAAwACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwA"},
                {"e38","bnNsb29rdXAgZmxhcmUtb24uY29tIHwgZmluZHN0ciAvaSBBZGRyZXNzO25zbG9va3VwIHdlYm1haWwuZmxhcmUtb24uY29tIHwgZmluZHN0ciAvaSBBZGRyZXNz" },
                {"3a7","JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4AMgAxAC4AMgAwADEAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA7ACQAKABwAGkAbgBnACAALQBuACAAMQAgADEAMAAuADEAMAAuADEAOQAuADIAMAAxACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgAxADAALgAxADkALgAyADAAMgAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4AMgA0AC4AMgAwADAAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA="},
                {"197","JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4AMQAwAC4ANAAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4ANQAwAC4AMQAwACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgAxADAALgAyADIALgA1ADAAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA7ACQAKABwAGkAbgBnACAALQBuACAAMQAgADEAMAAuADEAMAAuADQANQAuADEAOQAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsAA=="},
                {"e87","JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANQAxAC4AMQAxACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgA2ADUALgA2AC4AMQAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANQAyAC4AMgAwADAAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA7ACQAKABwAGkAbgBnACAALQBuACAAMQAgADEAMAAuADYANQAuADYALgAzACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwA"},
                {"2e4","JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANAA1AC4AMQA4ACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgA2ADUALgAyADgALgA0ADEAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA7ACQAKABwAGkAbgBnACAALQBuACAAMQAgADEAMAAuADYANQAuADMANgAuADEAMwAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4ANgA1AC4ANQAxAC4AMQAwACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwA"},
                {"939", "JAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4AMgAyAC4ANAAyACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgAxADAALgAyADMALgAyADAAMAAgAHwAIABmAGkAbgBkAHMAdAByACAALwBpACAAdAB0AGwAKQAgAC0AZQBxACAAJABuAHUAbABsADsAJAAoAHAAaQBuAGcAIAAtAG4AIAAxACAAMQAwAC4AMQAwAC4ANAA1AC4AMQA5ACAAfAAgAGYAaQBuAGQAcwB0AHIAIAAvAGkAIAB0AHQAbAApACAALQBlAHEAIAAkAG4AdQBsAGwAOwAkACgAcABpAG4AZwAgAC0AbgAgADEAIAAxADAALgAxADAALgAxADkALgA1ADAAIAB8ACAAZgBpAG4AZABzAHQAcgAgAC8AaQAgAHQAdABsACkAIAAtAGUAcQAgACQAbgB1AGwAbAA="},
                {"146", "JChwaW5nIC1uIDEgMTAuNjUuNDUuMyB8IGZpbmRzdHIgL2kgdHRsKSAtZXEgJG51bGw7JChwaW5nIC1uIDEgMTAuNjUuNC41MiB8IGZpbmRzdHIgL2kgdHRsKSAtZXEgJG51bGw7JChwaW5nIC1uIDEgMTAuNjUuMzEuMTU1IHwgZmluZHN0ciAvaSB0dGwpIC1lcSAkbnVsbDskKHBpbmcgLW4gMSBmbGFyZS1vbi5jb20gfCBmaW5kc3RyIC9pIHR0bCkgLWVxICRudWxs" },
                {"3c9974", "RwBlAHQALQBOAGUAdABJAFAAQwBvAG4AZgBpAGcAdQByAGEAdABpAG8AbgAgAHwAIABGAG8AcgBlAGEAYwBoACAASQBQAHYANABEAGUAZgBhAHUAbAB0AEcAYQB0AGUAdwBhAHkAIAB8ACAAUwBlAGwAZQBjAHQALQBPAGIAagBlAGMAdAAgAE4AZQB4AHQASABvAHAA" },
                {"8e6","RwBlAHQALQBEAG4AcwBDAGwAaQBlAG4AdABTAGUAcgB2AGUAcgBBAGQAZAByAGUAcwBzACAALQBBAGQAZAByAGUAcwBzAEYAYQBtAGkAbAB5ACAASQBQAHYANAAgAHwAIABTAGUAbABlAGMAdAAtAE8AYgBqAGUAYwB0ACAAUwBFAFIAVgBFAFIAQQBkAGQAcgBlAHMAcwBlAHMA" },
        };
        this. dictFromBruteforceCommand = new Dictionary<string, string> { { "f38","hostname"}, { "113", "whoami"}, { "bfb","net user"}, { "709", "systeminfo | findstr /i \"Domain\""} };
    }
    public string ReverseArr(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    //Gets for decryption.
    public void GetOutputId() {
        bool isEmpty = false;
        while (true) {
            if (isEmpty == true)
            {
                break;
            }
            foreach (KeyValuePair<string, string> pair in dictFromBruteforce)
            {
                int i = int.Parse(pair.Key);
                string s = pair.Value;
                bool flag = ToBruteforce.Count != 0 && ToBruteforce[0] == (i ^ 248);
                if (flag)
                {
                    outputID += s;
                    ToBruteForceList.Add(s);
                    Console.WriteLine(s);
                    ToBruteforce.Remove(i ^ 248);
                    if (ToBruteforce.Count() == 0) {
                        isEmpty = true;
                    }
                    break;
                }
            }
        }
        Console.WriteLine("The Output dict is: " + outputID);
    }

    public byte[] Rc4Decrytion(byte[] p, byte[] d)
    {
        int[] array = new int[256];
        int[] array2 = new int[256];
        byte[] array3 = new byte[d.Length];
        int i;
        for (i = 0; i < 256; i++)
        {
            array[i] = (int)p[i % p.Length];
            array2[i] = i;
        }
        int num;
        for (i = (num = 0); i < 256; i++)
        {
            num = (num + array2[i] + array[i]) % 256;
            int num2 = array2[i];
            array2[i] = array2[num];
            array2[num] = num2;
        }
        int num3;
        num = (num3 = (i = 0));
        while (i < d.Length)
        {
            num3++;
            num3 %= 256;
            num += array2[num3];
            num %= 256;
            int num2 = array2[num3];
            array2[num3] = array2[num];
            array2[num] = num2;
            int num4 = array2[(array2[num3] + array2[num]) % 256];
            array3[i] = (byte)((int)d[i] ^ num4);
            i++;
        }
        return array3;
    }

    public byte[] LookUpBuffer(string h)
    {
        string location = reflectedAssembly.Location;
        FLARE09 flare = new FLARE09(location);
        byte[] array = null;
        FileStream fileStream = new FileStream(location, FileMode.Open, FileAccess.Read);
        foreach (FLARE09.IMAGE_SECTION_HEADER image_SECTION_HEADER in flare.ImageSectionHeaders)
        {
            bool flag = h.StartsWith(new string(image_SECTION_HEADER.Name));
            if (flag)
            {
                array = new byte[image_SECTION_HEADER.VirtualSize];
                fileStream.Seek((long)((ulong)image_SECTION_HEADER.PointerToRawData), SeekOrigin.Begin);
                fileStream.Read(array, 0, (int)image_SECTION_HEADER.VirtualSize);
                break;
            }
        }
        return array;
    }


    public string ConcatenateString(string c)
    {
        return "powershell -exec bypass -enc \"" + c + "\"";
    }
 
    private string searchId(string id, ref bool isCommand){
        string Value = null;
        foreach (KeyValuePair<string, string> pair in arraysToConcat) {
            if (pair.Key.Equals(id)) {
                isCommand = false;
                Value = pair.Value;
                break;
            }
        }
        Console.WriteLine("Command not found in base64 commands, trying with normal command now");
        foreach (KeyValuePair<string, string> pair in dictFromBruteforceCommand) {
            if (pair.Key.Equals(id)) {
                Value = pair.Value;
                isCommand = true;
                break;
            }
        }

        return Value;
    }

    //String should work: Caller and Caller of the Caller
    //1.-"System.Object InvokeMethod(System.Object, System.Object[], System.Signature, Boolean)"
    //2.-"System.Object Invoke(System.Object, System.Reflection.BindingFlags, System.Reflection.Binder, System.Object[], System.Globalization.CultureInfo)";
    //3.-"System.Object Invoke(System.Object, System.Object[])"
    //4.-"Byte[] GetCodeInfo(Int32 ByRef, Int32 ByRef, Int32 ByRef)"
    //5.-"System.Object flare_71(System.InvalidProgramException, System.Object[], System.Collections.Generic.Dictionary`2[System.UInt32,System.Int32], Byte[])"

    //Combinaciones:
    //1 y 4 (No funciono)
    //1 y 3 (No funciono).
    //2 y 3 (No funciono).


    //Possible errors:
    //1.-Appended data is wrong. (Data is confirmed that is correct, not the issue)
    //2.-Order of being appended is wrong (Cant be the case because 




    private string EmulatedString() {
        string ret = "System.Object InvokeMethod(System.Object, System.Object[], System.Signature, Boolean)" + "System.Object Invoke(System.Object, System.Reflection.BindingFlags, System.Reflection.Binder, System.Object[], System.Globalization.CultureInfo)";
        return ret;
    }

    //Append Data based on the command order.
    public void InitializeHashState()
    {
        int countList = ToBruteForceList.Count();
        for (int i = 0; i < countList; i++)
        {
            if (ToBruteForceList[i] == "ea5"){
                Console.WriteLine("Specific case found, omitting: ea5");
                continue;
            }
            bool isCommand = false;
            string value = searchId(ToBruteForceList[i], ref isCommand);
            string text = null;
            if (isCommand != true)
            {
                text = ConcatenateString(value);
            }
            else {
                text = value;
            }
            string emulated = EmulatedString() + text;
            Console.WriteLine("Emulated line: " + i + ", where state is: " + ToBruteForceList[i] + ", where the value is:" + emulated + "\n");
            hashIncrement.AppendData(Encoding.ASCII.GetBytes(emulated));
        }   
    }

    public byte[] emulate_decryption()
    {
        GetOutputId();
        InitializeHashState();
        byte[] buffer = LookUpBuffer(ReverseArr(outputID));
        //Hash is off, which means data appended is wrong.
        byte[] hashAndReset = hashIncrement.GetHashAndReset();
        byte[] array = Rc4Decrytion(hashAndReset, buffer);
        var Name = Rc4Decrytion(hashAndReset, new byte[] { 31, 29, 40, 72 });
        Console.WriteLine("The name of the file is: " + Encoding.UTF8.GetString(Name));
        string text = Path.GetTempPath() + "\\ImRetarded" + Encoding.UTF8.GetString(Name);
        //Console.WriteLine(text);
        FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read);
        fileStream.Write(array, 0, array.Length);
       

        return buffer;
    }

    string filePath;
    public string outputID;
    Dictionary<string, string> dictFromBruteforce;
    Module reflectedModule;
    Assembly reflectedAssembly;
    IncrementalHash hashIncrement;
    ObservableCollection<int> ToBruteforce;
    List<string> ToBruteForceList;
    Dictionary<string, string> dictFromBruteforceCommand;
    Dictionary<string, string> arraysToConcat;
};


namespace DecryptionEmulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string path = @"C:\Users\User\Downloads\08_backdoor\FlareOn.Backdoor.exe";
            Generator gen = new Generator(path);
            gen.emulate_decryption();
            Console.ReadLine();


        }
    }
}
