#!/bin/bash

# Android Screenshot Automation Script
# This script automates the process of taking screenshots of the BootcampMauiApp

set -e

# Configuration
APP_PACKAGE="com.companyname.bootcampmauiapp"
APK_PATH="src/BootcampMauiApp/bin/Debug/net9.0-android/com.companyname.bootcampmauiapp-Signed.apk"
SCREENSHOTS_DIR="docs/images"
ANDROID_HOME="${ANDROID_HOME:-/usr/local/lib/android/sdk}"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

log_info() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

log_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

log_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Check prerequisites
check_prerequisites() {
    log_info "Checking prerequisites..."
    
    # Check if Android SDK is available
    if [[ ! -d "$ANDROID_HOME" ]]; then
        log_error "Android SDK not found at $ANDROID_HOME"
        exit 1
    fi
    
    # Check if ADB is available
    if [[ ! -f "$ANDROID_HOME/platform-tools/adb" ]]; then
        log_error "ADB not found. Make sure platform-tools are installed."
        exit 1
    fi
    
    # Check if emulator is available
    if [[ ! -f "$ANDROID_HOME/emulator/emulator" ]]; then
        log_error "Android emulator not found. Please install emulator package."
        log_info "Run: $ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager \"emulator\""
        exit 1
    fi
    
    # Check if APK exists
    if [[ ! -f "$APK_PATH" ]]; then
        log_error "APK not found at $APK_PATH"
        log_info "Please build the MAUI app first: dotnet build src/BootcampMauiApp/BootcampMauiApp.csproj"
        exit 1
    fi
    
    # Create screenshots directory if it doesn't exist
    mkdir -p "$SCREENSHOTS_DIR"
    
    log_info "Prerequisites check completed successfully!"
}

# Check if emulator is running
check_emulator() {
    log_info "Checking for running Android emulator..."
    
    local devices=$($ANDROID_HOME/platform-tools/adb devices | grep -v "List of devices" | grep -v "^$" | wc -l)
    
    if [[ $devices -eq 0 ]]; then
        log_warning "No Android devices/emulators detected."
        log_info "Please start an Android emulator or connect a device."
        log_info "To list available AVDs: $ANDROID_HOME/emulator/emulator -list-avds"
        log_info "To start an AVD: $ANDROID_HOME/emulator/emulator -avd <AVD_NAME>"
        exit 1
    fi
    
    log_info "Android device/emulator detected!"
}

# Install and launch app
install_and_launch_app() {
    log_info "Installing app on device..."
    
    # Uninstall previous version if exists
    $ANDROID_HOME/platform-tools/adb uninstall $APP_PACKAGE 2>/dev/null || true
    
    # Install APK
    $ANDROID_HOME/platform-tools/adb install "$APK_PATH"
    
    log_info "Launching app..."
    
    # Launch app (MAUI apps typically use MainActivity)
    $ANDROID_HOME/platform-tools/adb shell am start -n "${APP_PACKAGE}/crc64*.MainActivity" || \
    $ANDROID_HOME/platform-tools/adb shell am start -n "${APP_PACKAGE}/.MainActivity" || \
    $ANDROID_HOME/platform-tools/adb shell monkey -p $APP_PACKAGE -c android.intent.category.LAUNCHER 1
    
    # Wait for app to load
    sleep 5
}

# Take screenshot
take_screenshot() {
    local filename="$1"
    local description="$2"
    
    log_info "Taking screenshot: $description"
    
    # Take screenshot using screencap
    $ANDROID_HOME/platform-tools/adb exec-out screencap -p > "$SCREENSHOTS_DIR/$filename"
    
    # Verify screenshot was created and has content
    if [[ -f "$SCREENSHOTS_DIR/$filename" && -s "$SCREENSHOTS_DIR/$filename" ]]; then
        log_info "Screenshot saved: $SCREENSHOTS_DIR/$filename"
    else
        log_error "Failed to capture screenshot: $filename"
        exit 1
    fi
}

# Perform swipe gesture
perform_swipe() {
    local description="$1"
    
    log_info "Performing gesture: $description"
    
    # Swipe left gesture (adjust coordinates based on device screen size)
    # This assumes a typical phone resolution - may need adjustment
    $ANDROID_HOME/platform-tools/adb shell input swipe 800 600 200 600 500
    
    # Wait for animation to complete
    sleep 3
}

# Main screenshot capture process
capture_screenshots() {
    log_info "Starting screenshot capture process..."
    
    # Take initial screenshot
    take_screenshot "android-maui-initial.png" "Initial app state"
    
    # Perform swipe left gesture
    perform_swipe "Swipe left to navigate"
    
    # Take screenshot after swipe
    take_screenshot "android-maui-after-swipe.png" "After swipe navigation"
    
    log_info "Screenshot capture completed successfully!"
    log_info "Screenshots saved in: $SCREENSHOTS_DIR"
}

# Display usage information
show_usage() {
    echo "Usage: $0 [options]"
    echo ""
    echo "Options:"
    echo "  --help, -h     Show this help message"
    echo "  --check-only   Only check prerequisites, don't capture screenshots"
    echo ""
    echo "Environment Variables:"
    echo "  ANDROID_HOME   Path to Android SDK (default: /usr/local/lib/android/sdk)"
    echo ""
    echo "Example:"
    echo "  $0                    # Check prerequisites and capture screenshots"
    echo "  $0 --check-only      # Only check if environment is ready"
}

# Main function
main() {
    case "${1:-}" in
        --help|-h)
            show_usage
            exit 0
            ;;
        --check-only)
            check_prerequisites
            check_emulator
            log_info "Environment check completed. Ready to capture screenshots!"
            exit 0
            ;;
        "")
            check_prerequisites
            check_emulator
            install_and_launch_app
            capture_screenshots
            ;;
        *)
            log_error "Unknown option: $1"
            show_usage
            exit 1
            ;;
    esac
}

# Run main function with all arguments
main "$@"