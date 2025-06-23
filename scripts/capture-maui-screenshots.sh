#!/bin/bash

# Script to capture MAUI Android app screenshots
# This script automates the process of launching the MAUI app, taking screenshots, and handling swipe gestures

set -e  # Exit on any error

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(dirname "$SCRIPT_DIR")"
MAUI_PROJECT_DIR="$PROJECT_ROOT/src/BootcampMauiApp"
SCREENSHOTS_DIR="$PROJECT_ROOT/docs/images"
APK_PATH="$MAUI_PROJECT_DIR/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk"

# Android SDK paths
export ANDROID_HOME="${ANDROID_HOME:-/usr/local/lib/android/sdk}"
export PATH="$PATH:$ANDROID_HOME/platform-tools:$ANDROID_HOME/cmdline-tools/latest/bin"

echo "🚀 Starting MAUI Android App Screenshot Capture Process"
echo "Project Directory: $MAUI_PROJECT_DIR"
echo "Screenshots Directory: $SCREENSHOTS_DIR"

# Function to wait for device to be ready
wait_for_device() {
    echo "⏳ Waiting for Android device/emulator to be ready..."
    adb wait-for-device
    
    # Wait for boot to complete
    while [[ "$(adb shell getprop sys.boot_completed 2>/dev/null)" != "1" ]]; do
        echo "⏳ Waiting for device boot to complete..."
        sleep 2
    done
    echo "✅ Device is ready"
}

# Function to build the MAUI app
build_maui_app() {
    echo "🔨 Building MAUI Android app..."
    cd "$MAUI_PROJECT_DIR"
    dotnet build -c Debug -f net9.0-android
    echo "✅ MAUI app built successfully"
}

# Function to install the app
install_app() {
    echo "📱 Installing MAUI app on device..."
    if [[ -f "$APK_PATH" ]]; then
        adb install -r "$APK_PATH"
        echo "✅ App installed successfully"
    else
        echo "❌ APK not found at $APK_PATH"
        # Try to find the APK with a different name pattern
        APK_PATTERN="$MAUI_PROJECT_DIR/bin/Debug/net9.0-android/*.apk"
        APK_FILES=($(ls $APK_PATTERN 2>/dev/null || true))
        if [[ ${#APK_FILES[@]} -gt 0 ]]; then
            APK_PATH="${APK_FILES[0]}"
            echo "📱 Found APK at: $APK_PATH"
            adb install -r "$APK_PATH"
            echo "✅ App installed successfully"
        else
            echo "❌ No APK files found. Please build the project first."
            exit 1
        fi
    fi
}

# Function to launch the app
launch_app() {
    echo "🚀 Launching MAUI app..."
    # Launch the app using the package name and main activity
    adb shell am start -n "com.companyname.bootcampmauiapp/crc64b76f6e8b2d8c8db1.MainActivity"
    
    # Wait for app to fully load
    echo "⏳ Waiting for app to load..."
    sleep 5
    echo "✅ App launched successfully"
}

# Function to take a screenshot
take_screenshot() {
    local filename="$1"
    local filepath="$SCREENSHOTS_DIR/$filename"
    
    echo "📸 Taking screenshot: $filename"
    
    # Create screenshots directory if it doesn't exist
    mkdir -p "$SCREENSHOTS_DIR"
    
    # Take screenshot using adb
    adb shell screencap -p "/sdcard/screenshot.png"
    adb pull "/sdcard/screenshot.png" "$filepath"
    adb shell rm "/sdcard/screenshot.png"
    
    echo "✅ Screenshot saved to: $filepath"
}

# Function to perform swipe gesture
perform_swipe_left() {
    echo "👆 Performing swipe left gesture..."
    
    # Get screen dimensions for swipe coordinates
    SCREEN_SIZE=$(adb shell wm size | grep -o '[0-9]*x[0-9]*' | head -1)
    WIDTH=$(echo $SCREEN_SIZE | cut -d'x' -f1)
    HEIGHT=$(echo $SCREEN_SIZE | cut -d'x' -f2)
    
    # Calculate swipe coordinates (swipe from right to left, middle of screen)
    START_X=$((WIDTH * 3 / 4))
    END_X=$((WIDTH * 1 / 4))
    Y=$((HEIGHT / 2))
    
    # Perform swipe gesture
    adb shell input swipe $START_X $Y $END_X $Y 500
    echo "✅ Swipe left gesture completed"
}

# Function to check if emulator/device is available
check_device_availability() {
    echo "🔍 Checking for available Android devices/emulators..."
    
    # Check if adb is available
    if ! command -v adb &> /dev/null; then
        echo "❌ adb command not found. Please ensure Android SDK is properly installed."
        return 1
    fi
    
    # Start adb server
    adb start-server
    
    # Check for connected devices
    DEVICE_COUNT=$(adb devices | grep -c "device$" || true)
    if [[ $DEVICE_COUNT -eq 0 ]]; then
        echo "❌ No Android devices or emulators found."
        echo "💡 Please start an Android emulator or connect a device before running this script."
        return 1
    fi
    
    echo "✅ Found $DEVICE_COUNT Android device(s)/emulator(s)"
    adb devices
    return 0
}

# Main execution flow
main() {
    echo "🎯 MAUI Android Screenshot Capture Script"
    echo "========================================"
    
    # Check if device is available
    if ! check_device_availability; then
        echo ""
        echo "ℹ️  To run this script, you need:"
        echo "   1. Android SDK with adb installed"
        echo "   2. An Android emulator running or a physical device connected"
        echo "   3. USB debugging enabled (for physical devices)"
        echo ""
        echo "📋 Example commands to set up an emulator:"
        echo "   avdmanager create avd -n test_avd -k \"system-images;android-30;google_apis;x86_64\""
        echo "   emulator -avd test_avd -no-window -gpu swiftshader_indirect &"
        exit 1
    fi
    
    # Build the MAUI app
    build_maui_app
    
    # Wait for device to be ready
    wait_for_device
    
    # Install the app
    install_app
    
    # Launch the app
    launch_app
    
    # Take initial screenshot
    take_screenshot "maui-app-initial.png"
    
    # Perform swipe left gesture
    perform_swipe_left
    
    # Wait a few seconds as requested
    echo "⏳ Waiting 3 seconds after swipe..."
    sleep 3
    
    # Take second screenshot
    take_screenshot "maui-app-after-swipe.png"
    
    echo ""
    echo "🎉 Screenshot capture process completed successfully!"
    echo "📁 Screenshots saved in: $SCREENSHOTS_DIR"
    echo "   - maui-app-initial.png"
    echo "   - maui-app-after-swipe.png"
}

# Handle script termination
cleanup() {
    echo ""
    echo "🧹 Cleaning up..."
    # Add any cleanup commands here if needed
}

trap cleanup EXIT

# Run main function if script is executed directly
if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
    main "$@"
fi