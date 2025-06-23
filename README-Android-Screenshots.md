# Android Screenshot Solution

This directory contains the solution for capturing screenshots of the BootcampMauiApp running on Android.

## Status

✅ **MAUI App Built Successfully**  
The Android APK has been generated and is ready for deployment:
- Location: `src/BootcampMauiApp/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk`
- Package: `com.companyname.bootcampmauiapp`
- Target SDK: Android API 35
- Min SDK: Android API 21

❌ **Environment Limitations**  
The current GitHub Actions environment lacks the necessary components for Android emulation:
- Android Emulator package not installed
- No system images available
- No internet connectivity to download missing packages
- Hardware acceleration (KVM) not available

## Solution Files

### 1. Setup Documentation
- **[docs/ANDROID_SCREENSHOT_SETUP.md](docs/ANDROID_SCREENSHOT_SETUP.md)** - Comprehensive guide for setting up Android screenshot environment

### 2. Automation Script
- **[scripts/capture-android-screenshots.sh](scripts/capture-android-screenshots.sh)** - Automated script for capturing screenshots once environment is ready

## Quick Start (When Environment is Ready)

1. Install required Android SDK components:
   ```bash
   # Install emulator and system image
   $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager "emulator"
   $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager "system-images;android-34;google_apis;x86_64"
   
   # Create and start AVD
   $ANDROID_HOME/cmdline-tools/latest/bin/avdmanager create avd -n "TestAVD" -k "system-images;android-34;google_apis;x86_64"
   $ANDROID_HOME/emulator/emulator -avd TestAVD &
   ```

2. Run the automated screenshot script:
   ```bash
   ./scripts/capture-android-screenshots.sh
   ```

3. Screenshots will be saved to `docs/images/`:
   - `android-maui-initial.png` - Initial app state
   - `android-maui-after-swipe.png` - After swipe left navigation

## Manual Process

If you prefer manual control:

```bash
# Deploy APK to emulator/device
$ANDROID_HOME/platform-tools/adb install src/BootcampMauiApp/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk

# Launch app
$ANDROID_HOME/platform-tools/adb shell monkey -p com.companyname.bootcampmauiapp -c android.intent.category.LAUNCHER 1

# Take initial screenshot
$ANDROID_HOME/platform-tools/adb exec-out screencap -p > docs/images/android-maui-initial.png

# Perform swipe left gesture
$ANDROID_HOME/platform-tools/adb shell input swipe 800 600 200 600 500

# Wait and take second screenshot
sleep 3
$ANDROID_HOME/platform-tools/adb exec-out screencap -p > docs/images/android-maui-after-swipe.png
```

## Alternative Solutions

### Physical Android Device
1. Enable Developer Options and USB Debugging on Android device
2. Connect via USB
3. Follow the same ADB commands above

### Cloud Testing Services
- Firebase Test Lab
- AWS Device Farm  
- BrowserStack App Live
- Sauce Labs Real Device Cloud

### Android Studio
Use the built-in emulator and screenshot tools in Android Studio IDE.

## App Features

The BootcampMauiApp includes multiple pages accessible via tab navigation:
- **Home** - Main landing page
- **Counter** - Interactive counter with increment button
- **Weather** - Weather forecast display
- **Monkeys** - Animal information browser

The swipe left gesture navigates between these tabs, demonstrating the app's navigation functionality.

## Verification

To verify the setup works before capturing screenshots:
```bash
./scripts/capture-android-screenshots.sh --check-only
```

This will validate that all prerequisites are met without actually running the screenshot capture process.