#pragma once
#include "pch.h"
#include <msclr/marshal_cppstd.h>
#include <string>

using namespace System;

namespace SampleCLRComp {

    public ref class SampleUnmanagedClass
    {
    public:
        static String^ CallDotNetCore(String^ name);
    };
}
