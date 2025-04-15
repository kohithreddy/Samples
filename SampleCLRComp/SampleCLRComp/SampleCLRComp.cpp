#include "pch.h"  
#include "SampleCLRComp.h"  
#include <iostream>

using namespace WPFNetCore::Editor;

namespace SampleCLRComp {
  String^ SampleUnmanagedClass::CallDotNetCore(String^ name) {
        String^ managedName = gcnew String(name);

       // Create an instance of the Helper class to call the non-static Hello method
       Helper^ helperInstance = gcnew Helper();
       String^ result = helperInstance->Hello(managedName);
       return result;
  }
}