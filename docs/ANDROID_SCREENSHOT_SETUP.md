# Android Screenshot Setup Guide

## Overview
This document outlines the steps required to capture screenshots of the BootcampMauiApp running on Android, including the setup requirements and process.

## Environment Requirements
To successfully capture Android screenshots as requested, the following components are required:

### Android SDK Components
- Android SDK Platform Tools
- Android Emulator package
- System images for target Android versions
- Hardware acceleration (KVM on Linux, HAXM on Windows)

### Current Environment Status
✅ Android SDK installed at `/usr/local/lib/android/sdk`  
✅ Platform tools available  
✅ Build tools available (versions 34.0.0, 35.0.0, 35.0.1, 36.0.0)  
✅ Android platforms available (API 33-36)  
✅ .NET 9 SDK with MAUI Android workload  
✅ MAUI app successfully built for Android  
✅ APK generated: `src/BootcampMauiApp/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk`  

❌ Android Emulator package not installed  
❌ No system images available  
❌ No internet connectivity to download required packages  
❌ KVM/hardware acceleration not available  

## Steps to Complete Screenshot Task

### 1. Install Required Android Components
```bash
# Install Android Emulator
$ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager "emulator"

# Install system image (example for API 34)
$ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager "system-images;android-34;google_apis;x86_64"

# Install additional tools if needed
$ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager "platform-tools" "build-tools;34.0.0"
```

### 2. Create Android Virtual Device (AVD)
```bash
# Create AVD
$ANDROID_HOME/cmdline-tools/latest/bin/avdmanager create avd \
  -n "BootcampApp_AVD" \
  -k "system-images;android-34;google_apis;x86_64" \
  -d "pixel_4"
```

### 3. Start Android Emulator
```bash
# Start emulator
$ANDROID_HOME/emulator/emulator -avd BootcampApp_AVD -gpu host
```

### 4. Deploy and Run MAUI App
```bash
# Build and deploy to emulator
cd /path/to/BootcampMauiApp
dotnet build -f net9.0-android
$ANDROID_HOME/platform-tools/adb install -r bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk

# Launch app
$ANDROID_HOME/platform-tools/adb shell am start -n com.companyname.bootcampmauiapp/crc64*.MainActivity
```

### 5. Capture Screenshots
```bash
# Take initial screenshot
$ANDROID_HOME/platform-tools/adb exec-out screencap -p > docs/images/android-initial-screenshot.png

# Simulate swipe left gesture
$ANDROID_HOME/platform-tools/adb shell input swipe 800 400 200 400 500

# Wait a few seconds
sleep 3

# Take second screenshot
$ANDROID_HOME/platform-tools/adb exec-out screencap -p > docs/images/android-after-swipe-screenshot.png
```

## Alternative Approaches

### Using Android Studio
1. Open Android Studio
2. Start AVD Manager
3. Create and launch virtual device
4. Deploy app through IDE
5. Use built-in screenshot tools

### Using Cloud Testing Services
- Firebase Test Lab
- AWS Device Farm
- BrowserStack App Live
- Sauce Labs Real Device Cloud

### Using Physical Android Device
1. Enable Developer Options and USB Debugging
2. Connect device via USB
3. Deploy app using ADB
4. Use ADB screencap commands as shown above

## APK Information
The built APK is located at:
```
src/BootcampMauiApp/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk
```

APK Details:
- Package Name: com.companyname.bootcampmauiapp
- Version: 1.0 (Code: 1)
- Target SDK: 35
- Min SDK: 21
- Permissions: INTERNET, ACCESS_NETWORK_STATE

## App Features to Screenshot
The MAUI app includes:
1. Home page with navigation tabs
2. Counter page with increment functionality
3. Weather page showing forecast data
4. Monkeys page displaying animal information

The swipe left gesture should navigate between these tabs, demonstrating the app's navigation functionality.