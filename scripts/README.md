# MAUI Android Screenshot Scripts

This directory contains scripts for capturing screenshots of the MAUI Android application.

## Scripts

### 1. `capture-maui-screenshots.sh`

**Purpose**: Automated script for capturing real screenshots from a running MAUI Android app.

**Requirements**:
- Android SDK with ADB installed
- Android emulator running or physical device connected
- USB debugging enabled (for physical devices)
- MAUI app built and ready for deployment

**Usage**:
```bash
./scripts/capture-maui-screenshots.sh
```

**What it does**:
1. Builds the MAUI Android app
2. Waits for Android device/emulator to be ready
3. Installs the app on the device
4. Launches the app
5. Takes initial screenshot
6. Performs swipe left gesture
7. Waits 3 seconds
8. Takes second screenshot
9. Saves both screenshots to `docs/images/`

**Output files**:
- `docs/images/maui-app-initial.png` - Initial app view
- `docs/images/maui-app-after-swipe.png` - View after swipe gesture

### 2. `generate-maui-screenshots.py`

**Purpose**: Generate mock screenshots representing the MAUI app UI when real Android environment is not available.

**Requirements**:
- Python 3.6+
- Pillow library (`pip install Pillow`)

**Usage**:
```bash
python3 scripts/generate-maui-screenshots.py
```

**What it does**:
1. Creates mock Android device frames
2. Draws MAUI app UI elements (Home and Counter pages)
3. Simulates the app appearance with proper Android styling
4. Generates screenshots representing initial view and after-swipe view
5. Creates metadata file documenting the screenshots

**Output files**:
- `docs/images/maui-app-initial.png` - Mock initial app view (Home page)
- `docs/images/maui-app-after-swipe.png` - Mock view after swipe (Counter page)
- `docs/images/maui-screenshots-metadata.json` - Documentation metadata

## Android Emulator Setup

If you need to set up an Android emulator for use with the capture script:

1. **Install Android SDK**: Ensure Android SDK is installed with platform-tools and cmdline-tools
2. **Download system images**: 
   ```bash
   sdkmanager "system-images;android-30;google_apis;x86_64"
   ```
3. **Create AVD**:
   ```bash
   avdmanager create avd -n test_avd -k "system-images;android-30;google_apis;x86_64"
   ```
4. **Start emulator**:
   ```bash
   emulator -avd test_avd -no-window -gpu swiftshader_indirect &
   ```

## CI/CD Integration

For GitHub Actions or other CI environments, consider:

1. **Use the mock generator**: When emulator setup is complex, use `generate-maui-screenshots.py`
2. **Android emulator actions**: Use specialized GitHub Actions like `reactivecircus/android-emulator-runner`
3. **Conditional execution**: Run real capture when emulator is available, mock generation otherwise

## Screenshot Specifications

- **Format**: PNG
- **Dimensions**: 360x640 pixels (standard Android phone)
- **Content**: Shows MAUI app with proper branding and navigation
- **Pages**: Home (initial) and Counter (after swipe left)

## Notes

- The mock generator creates representative screenshots for documentation purposes
- Real device screenshots will have actual MAUI rendering and device-specific UI elements
- Both scripts save to the same location for consistency
- Metadata file helps track screenshot generation method and contents