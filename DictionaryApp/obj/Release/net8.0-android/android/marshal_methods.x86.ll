; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [123 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [246 x i32] [
	i32 42639949, ; 0: System.Threading.Thread => 0x28aa24d => 112
	i32 67008169, ; 1: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 2: Microsoft.Maui.Graphics.dll => 0x44bb714 => 48
	i32 117431740, ; 3: System.Runtime.InteropServices => 0x6ffddbc => 104
	i32 165246403, ; 4: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 54
	i32 182336117, ; 5: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 72
	i32 195452805, ; 6: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 7: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 8: System.ComponentModel => 0xc38ff48 => 85
	i32 280992041, ; 9: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 317674968, ; 10: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 11: Xamarin.AndroidX.Activity.dll => 0x13031348 => 50
	i32 336156722, ; 12: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 13: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 61
	i32 356389973, ; 14: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 379916513, ; 15: System.Threading.Thread.dll => 0x16a510e1 => 112
	i32 385762202, ; 16: System.Memory.dll => 0x16fe439a => 94
	i32 395744057, ; 17: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 435591531, ; 18: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 19: System.Collections => 0x1a61054f => 82
	i32 450948140, ; 20: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 60
	i32 456227837, ; 21: System.Web.HttpUtility.dll => 0x1b317bfd => 114
	i32 469710990, ; 22: System.dll => 0x1bff388e => 118
	i32 498788369, ; 23: System.ObjectModel => 0x1dbae811 => 100
	i32 500358224, ; 24: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 25: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 513247710, ; 26: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 43
	i32 539058512, ; 27: Microsoft.Extensions.Logging => 0x20216150 => 40
	i32 592146354, ; 28: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 627609679, ; 29: Xamarin.AndroidX.CustomView => 0x2568904f => 58
	i32 627931235, ; 30: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 31: System.Text.Encodings.Web.dll => 0x27787397 => 109
	i32 672442732, ; 32: System.Collections.Concurrent => 0x2814a96c => 79
	i32 683518922, ; 33: System.Net.Security => 0x28bdabca => 98
	i32 688181140, ; 34: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 690569205, ; 35: System.Xml.Linq.dll => 0x29293ff5 => 115
	i32 706645707, ; 36: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 37: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 38: System.Runtime.Loader.dll => 0x2b15ed29 => 105
	i32 759454413, ; 39: System.Net.Requests => 0x2d445acd => 97
	i32 775507847, ; 40: System.IO.Compression => 0x2e394f87 => 91
	i32 777317022, ; 41: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 42: Microsoft.Extensions.Options => 0x2f0980eb => 42
	i32 823281589, ; 43: System.Private.Uri.dll => 0x311247b5 => 101
	i32 830298997, ; 44: System.IO.Compression.Brotli => 0x317d5b75 => 90
	i32 904024072, ; 45: System.ComponentModel.Primitives.dll => 0x35e25008 => 83
	i32 926902833, ; 46: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 967690846, ; 47: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 61
	i32 992768348, ; 48: System.Collections.dll => 0x3b2c715c => 82
	i32 1012816738, ; 49: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 71
	i32 1028951442, ; 50: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 39
	i32 1029334545, ; 51: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 52: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 51
	i32 1044663988, ; 53: System.Linq.Expressions.dll => 0x3e444eb4 => 92
	i32 1052210849, ; 54: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 63
	i32 1082857460, ; 55: System.ComponentModel.TypeConverter => 0x408b17f4 => 84
	i32 1084122840, ; 56: Xamarin.Kotlin.StdLib => 0x409e66d8 => 76
	i32 1098259244, ; 57: System => 0x41761b2c => 118
	i32 1118262833, ; 58: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1168523401, ; 59: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 60: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 68
	i32 1203215381, ; 61: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1214827643, ; 62: CommunityToolkit.Mvvm => 0x4868cc7b => 35
	i32 1234928153, ; 63: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1260983243, ; 64: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1293217323, ; 65: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 59
	i32 1324164729, ; 66: System.Linq => 0x4eed2679 => 93
	i32 1373134921, ; 67: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 68: Xamarin.AndroidX.SavedState => 0x52114ed3 => 71
	i32 1406073936, ; 69: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 55
	i32 1430672901, ; 70: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1452070440, ; 71: System.Formats.Asn1.dll => 0x568cd628 => 89
	i32 1458022317, ; 72: System.Net.Security.dll => 0x56e7a7ad => 98
	i32 1461004990, ; 73: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1462112819, ; 74: System.IO.Compression.dll => 0x57261233 => 91
	i32 1469204771, ; 75: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 52
	i32 1470490898, ; 76: Microsoft.Extensions.Primitives => 0x57a5e912 => 43
	i32 1480492111, ; 77: System.IO.Compression.Brotli.dll => 0x583e844f => 90
	i32 1493001747, ; 78: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1514721132, ; 79: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1543031311, ; 80: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 111
	i32 1551623176, ; 81: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 82: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 65
	i32 1624863272, ; 83: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 74
	i32 1636350590, ; 84: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 57
	i32 1639515021, ; 85: System.Net.Http.dll => 0x61b9038d => 95
	i32 1639986890, ; 86: System.Text.RegularExpressions => 0x61c036ca => 111
	i32 1657153582, ; 87: System.Runtime => 0x62c6282e => 107
	i32 1658251792, ; 88: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 75
	i32 1677501392, ; 89: System.Net.Primitives.dll => 0x63fca3d0 => 96
	i32 1679769178, ; 90: System.Security.Cryptography => 0x641f3e5a => 108
	i32 1729485958, ; 91: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 53
	i32 1736233607, ; 92: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
	i32 1743415430, ; 93: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1763938596, ; 94: System.Diagnostics.TraceSource.dll => 0x69239124 => 88
	i32 1766324549, ; 95: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 72
	i32 1770582343, ; 96: Microsoft.Extensions.Logging.dll => 0x6988f147 => 40
	i32 1780572499, ; 97: Mono.Android.Runtime.dll => 0x6a216153 => 121
	i32 1782862114, ; 98: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 99: Xamarin.AndroidX.Fragment => 0x6a96652d => 60
	i32 1793755602, ; 100: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 101: Xamarin.AndroidX.Loader => 0x6bcd3296 => 65
	i32 1813058853, ; 102: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 76
	i32 1813201214, ; 103: Xamarin.Google.Android.Material => 0x6c13413e => 75
	i32 1818569960, ; 104: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 69
	i32 1828688058, ; 105: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 41
	i32 1842015223, ; 106: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 107: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 108: System.Linq.Expressions => 0x6ec71a65 => 92
	i32 1875935024, ; 109: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1910275211, ; 110: System.Collections.NonGeneric.dll => 0x71dc7c8b => 80
	i32 1939592360, ; 111: System.Private.Xml.Linq => 0x739bd4a8 => 102
	i32 1968388702, ; 112: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 36
	i32 2003115576, ; 113: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2019465201, ; 114: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 63
	i32 2025202353, ; 115: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
	i32 2045470958, ; 116: System.Private.Xml => 0x79eb68ee => 103
	i32 2055257422, ; 117: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 62
	i32 2066184531, ; 118: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2070888862, ; 119: System.Diagnostics.TraceSource => 0x7b6f419e => 88
	i32 2079903147, ; 120: System.Runtime.dll => 0x7bf8cdab => 107
	i32 2090596640, ; 121: System.Numerics.Vectors => 0x7c9bf920 => 99
	i32 2127167465, ; 122: System.Console => 0x7ec9ffe9 => 86
	i32 2142473426, ; 123: System.Collections.Specialized => 0x7fb38cd2 => 81
	i32 2143790110, ; 124: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 117
	i32 2159891885, ; 125: Microsoft.Maui => 0x80bd55ad => 46
	i32 2169148018, ; 126: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 127: Microsoft.Extensions.Options.dll => 0x820d22b3 => 42
	i32 2192057212, ; 128: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 41
	i32 2193016926, ; 129: System.ObjectModel.dll => 0x82b6c85e => 100
	i32 2201107256, ; 130: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 77
	i32 2201231467, ; 131: System.Net.Http => 0x8334206b => 95
	i32 2207618523, ; 132: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2266799131, ; 133: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 37
	i32 2270573516, ; 134: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 135: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 70
	i32 2303942373, ; 136: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 137: System.Private.CoreLib.dll => 0x896b7878 => 119
	i32 2353062107, ; 138: System.Net.Primitives => 0x8c40e0db => 96
	i32 2368005991, ; 139: System.Xml.ReaderWriter.dll => 0x8d24e767 => 116
	i32 2371007202, ; 140: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 36
	i32 2395872292, ; 141: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2401565422, ; 142: System.Web.HttpUtility => 0x8f24faee => 114
	i32 2427813419, ; 143: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 144: System.Console.dll => 0x912896e5 => 86
	i32 2475788418, ; 145: Java.Interop.dll => 0x93918882 => 120
	i32 2480646305, ; 146: Microsoft.Maui.Controls => 0x93dba8a1 => 44
	i32 2550873716, ; 147: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 148: System.Text.Encodings.Web => 0x9930ee42 => 109
	i32 2593496499, ; 149: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 150: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 77
	i32 2617129537, ; 151: System.Private.Xml.dll => 0x9bfe3a41 => 103
	i32 2620871830, ; 152: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 57
	i32 2626831493, ; 153: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2663698177, ; 154: System.Runtime.Loader => 0x9ec4cf01 => 105
	i32 2724373263, ; 155: System.Runtime.Numerics.dll => 0xa262a30f => 106
	i32 2732626843, ; 156: Xamarin.AndroidX.Activity => 0xa2e0939b => 50
	i32 2737747696, ; 157: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 52
	i32 2752995522, ; 158: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 159: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 45
	i32 2764765095, ; 160: Microsoft.Maui.dll => 0xa4caf7a7 => 46
	i32 2778768386, ; 161: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 73
	i32 2785988530, ; 162: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 163: Microsoft.Maui.Graphics => 0xa7008e0b => 48
	i32 2806116107, ; 164: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 165: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 55
	i32 2831556043, ; 166: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2853208004, ; 167: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 73
	i32 2861189240, ; 168: Microsoft.Maui.Essentials => 0xaa8a4878 => 47
	i32 2909740682, ; 169: System.Private.CoreLib => 0xad6f1e8a => 119
	i32 2916838712, ; 170: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 74
	i32 2919462931, ; 171: System.Numerics.Vectors.dll => 0xae037813 => 99
	i32 2959614098, ; 172: System.ComponentModel.dll => 0xb0682092 => 85
	i32 2978675010, ; 173: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 59
	i32 3038032645, ; 174: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 175: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 66
	i32 3059408633, ; 176: Mono.Android.Runtime => 0xb65adef9 => 121
	i32 3059793426, ; 177: System.ComponentModel.Primitives => 0xb660be12 => 83
	i32 3077302341, ; 178: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3103600923, ; 179: System.Formats.Asn1 => 0xb8fd311b => 89
	i32 3178803400, ; 180: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 67
	i32 3210619471, ; 181: MyLibraryApp.dll => 0xbf5e2a4f => 78
	i32 3220365878, ; 182: System.Threading => 0xbff2e236 => 113
	i32 3249260365, ; 183: RestSharp.dll => 0xc1abc74d => 49
	i32 3258312781, ; 184: Xamarin.AndroidX.CardView => 0xc235e84d => 53
	i32 3271080638, ; 185: MyLibraryApp => 0xc2f8babe => 78
	i32 3305363605, ; 186: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 187: System.Net.Requests.dll => 0xc5b097e4 => 97
	i32 3317135071, ; 188: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 58
	i32 3346324047, ; 189: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 68
	i32 3357674450, ; 190: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 191: System.Text.Json => 0xc82afec1 => 110
	i32 3362522851, ; 192: Xamarin.AndroidX.Core => 0xc86c06e3 => 56
	i32 3366347497, ; 193: Java.Interop => 0xc8a662e9 => 120
	i32 3374999561, ; 194: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 70
	i32 3381016424, ; 195: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 196: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 38
	i32 3463511458, ; 197: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 198: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 84
	i32 3476120550, ; 199: Mono.Android => 0xcf3163e6 => 122
	i32 3479583265, ; 200: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 201: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 202: System.Text.Json.dll => 0xcfbaacae => 110
	i32 3509114376, ; 203: System.Xml.Linq => 0xd128d608 => 115
	i32 3580758918, ; 204: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 205: System.Linq.dll => 0xd715a361 => 93
	i32 3641597786, ; 206: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 62
	i32 3643446276, ; 207: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 208: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 67
	i32 3657292374, ; 209: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 37
	i32 3672681054, ; 210: Mono.Android.dll => 0xdae8aa5e => 122
	i32 3697841164, ; 211: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 212: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 66
	i32 3748608112, ; 213: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 87
	i32 3786282454, ; 214: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 54
	i32 3792276235, ; 215: System.Collections.NonGeneric => 0xe2098b0b => 80
	i32 3802395368, ; 216: System.Collections.Specialized.dll => 0xe2a3f2e8 => 81
	i32 3816437471, ; 217: RestSharp => 0xe37a36df => 49
	i32 3823082795, ; 218: System.Security.Cryptography.dll => 0xe3df9d2b => 108
	i32 3841636137, ; 219: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 39
	i32 3849253459, ; 220: System.Runtime.InteropServices.dll => 0xe56ef253 => 104
	i32 3889960447, ; 221: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 222: System.Collections.Concurrent.dll => 0xe839deed => 79
	i32 3896760992, ; 223: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 56
	i32 3928044579, ; 224: System.Xml.ReaderWriter => 0xea213423 => 116
	i32 3931092270, ; 225: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 69
	i32 3955647286, ; 226: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 51
	i32 3980434154, ; 227: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 228: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4025784931, ; 229: System.Memory => 0xeff49a63 => 94
	i32 4046471985, ; 230: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 45
	i32 4068434129, ; 231: System.Private.Xml.Linq.dll => 0xf27f60d1 => 102
	i32 4073602200, ; 232: System.Threading.dll => 0xf2ce3c98 => 113
	i32 4094352644, ; 233: Microsoft.Maui.Essentials.dll => 0xf40add04 => 47
	i32 4100113165, ; 234: System.Private.Uri => 0xf462c30d => 101
	i32 4102112229, ; 235: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4125707920, ; 236: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 237: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 38
	i32 4150914736, ; 238: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4159265925, ; 239: System.Xml.XmlSerializer => 0xf7e95c85 => 117
	i32 4182413190, ; 240: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 64
	i32 4213026141, ; 241: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 87
	i32 4271975918, ; 242: Microsoft.Maui.Controls.dll => 0xfea12dee => 44
	i32 4274623895, ; 243: CommunityToolkit.Mvvm.dll => 0xfec99597 => 35
	i32 4274976490, ; 244: System.Runtime.Numerics => 0xfecef6ea => 106
	i32 4292120959 ; 245: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 64
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [246 x i32] [
	i32 112, ; 0
	i32 33, ; 1
	i32 48, ; 2
	i32 104, ; 3
	i32 54, ; 4
	i32 72, ; 5
	i32 30, ; 6
	i32 31, ; 7
	i32 85, ; 8
	i32 2, ; 9
	i32 30, ; 10
	i32 50, ; 11
	i32 15, ; 12
	i32 61, ; 13
	i32 14, ; 14
	i32 112, ; 15
	i32 94, ; 16
	i32 34, ; 17
	i32 26, ; 18
	i32 82, ; 19
	i32 60, ; 20
	i32 114, ; 21
	i32 118, ; 22
	i32 100, ; 23
	i32 13, ; 24
	i32 7, ; 25
	i32 43, ; 26
	i32 40, ; 27
	i32 21, ; 28
	i32 58, ; 29
	i32 19, ; 30
	i32 109, ; 31
	i32 79, ; 32
	i32 98, ; 33
	i32 1, ; 34
	i32 115, ; 35
	i32 16, ; 36
	i32 4, ; 37
	i32 105, ; 38
	i32 97, ; 39
	i32 91, ; 40
	i32 25, ; 41
	i32 42, ; 42
	i32 101, ; 43
	i32 90, ; 44
	i32 83, ; 45
	i32 28, ; 46
	i32 61, ; 47
	i32 82, ; 48
	i32 71, ; 49
	i32 39, ; 50
	i32 3, ; 51
	i32 51, ; 52
	i32 92, ; 53
	i32 63, ; 54
	i32 84, ; 55
	i32 76, ; 56
	i32 118, ; 57
	i32 16, ; 58
	i32 22, ; 59
	i32 68, ; 60
	i32 20, ; 61
	i32 35, ; 62
	i32 18, ; 63
	i32 2, ; 64
	i32 59, ; 65
	i32 93, ; 66
	i32 32, ; 67
	i32 71, ; 68
	i32 55, ; 69
	i32 0, ; 70
	i32 89, ; 71
	i32 98, ; 72
	i32 6, ; 73
	i32 91, ; 74
	i32 52, ; 75
	i32 43, ; 76
	i32 90, ; 77
	i32 10, ; 78
	i32 5, ; 79
	i32 111, ; 80
	i32 25, ; 81
	i32 65, ; 82
	i32 74, ; 83
	i32 57, ; 84
	i32 95, ; 85
	i32 111, ; 86
	i32 107, ; 87
	i32 75, ; 88
	i32 96, ; 89
	i32 108, ; 90
	i32 53, ; 91
	i32 23, ; 92
	i32 1, ; 93
	i32 88, ; 94
	i32 72, ; 95
	i32 40, ; 96
	i32 121, ; 97
	i32 17, ; 98
	i32 60, ; 99
	i32 9, ; 100
	i32 65, ; 101
	i32 76, ; 102
	i32 75, ; 103
	i32 69, ; 104
	i32 41, ; 105
	i32 29, ; 106
	i32 26, ; 107
	i32 92, ; 108
	i32 8, ; 109
	i32 80, ; 110
	i32 102, ; 111
	i32 36, ; 112
	i32 5, ; 113
	i32 63, ; 114
	i32 0, ; 115
	i32 103, ; 116
	i32 62, ; 117
	i32 4, ; 118
	i32 88, ; 119
	i32 107, ; 120
	i32 99, ; 121
	i32 86, ; 122
	i32 81, ; 123
	i32 117, ; 124
	i32 46, ; 125
	i32 12, ; 126
	i32 42, ; 127
	i32 41, ; 128
	i32 100, ; 129
	i32 77, ; 130
	i32 95, ; 131
	i32 14, ; 132
	i32 37, ; 133
	i32 8, ; 134
	i32 70, ; 135
	i32 18, ; 136
	i32 119, ; 137
	i32 96, ; 138
	i32 116, ; 139
	i32 36, ; 140
	i32 13, ; 141
	i32 114, ; 142
	i32 10, ; 143
	i32 86, ; 144
	i32 120, ; 145
	i32 44, ; 146
	i32 11, ; 147
	i32 109, ; 148
	i32 20, ; 149
	i32 77, ; 150
	i32 103, ; 151
	i32 57, ; 152
	i32 15, ; 153
	i32 105, ; 154
	i32 106, ; 155
	i32 50, ; 156
	i32 52, ; 157
	i32 21, ; 158
	i32 45, ; 159
	i32 46, ; 160
	i32 73, ; 161
	i32 27, ; 162
	i32 48, ; 163
	i32 6, ; 164
	i32 55, ; 165
	i32 19, ; 166
	i32 73, ; 167
	i32 47, ; 168
	i32 119, ; 169
	i32 74, ; 170
	i32 99, ; 171
	i32 85, ; 172
	i32 59, ; 173
	i32 34, ; 174
	i32 66, ; 175
	i32 121, ; 176
	i32 83, ; 177
	i32 12, ; 178
	i32 89, ; 179
	i32 67, ; 180
	i32 78, ; 181
	i32 113, ; 182
	i32 49, ; 183
	i32 53, ; 184
	i32 78, ; 185
	i32 7, ; 186
	i32 97, ; 187
	i32 58, ; 188
	i32 68, ; 189
	i32 24, ; 190
	i32 110, ; 191
	i32 56, ; 192
	i32 120, ; 193
	i32 70, ; 194
	i32 3, ; 195
	i32 38, ; 196
	i32 11, ; 197
	i32 84, ; 198
	i32 122, ; 199
	i32 24, ; 200
	i32 23, ; 201
	i32 110, ; 202
	i32 115, ; 203
	i32 31, ; 204
	i32 93, ; 205
	i32 62, ; 206
	i32 28, ; 207
	i32 67, ; 208
	i32 37, ; 209
	i32 122, ; 210
	i32 33, ; 211
	i32 66, ; 212
	i32 87, ; 213
	i32 54, ; 214
	i32 80, ; 215
	i32 81, ; 216
	i32 49, ; 217
	i32 108, ; 218
	i32 39, ; 219
	i32 104, ; 220
	i32 32, ; 221
	i32 79, ; 222
	i32 56, ; 223
	i32 116, ; 224
	i32 69, ; 225
	i32 51, ; 226
	i32 27, ; 227
	i32 9, ; 228
	i32 94, ; 229
	i32 45, ; 230
	i32 102, ; 231
	i32 113, ; 232
	i32 47, ; 233
	i32 101, ; 234
	i32 22, ; 235
	i32 17, ; 236
	i32 38, ; 237
	i32 29, ; 238
	i32 117, ; 239
	i32 64, ; 240
	i32 87, ; 241
	i32 44, ; 242
	i32 35, ; 243
	i32 106, ; 244
	i32 64 ; 245
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 0d97e20b84d8e87c3502469ee395805907905fe3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
