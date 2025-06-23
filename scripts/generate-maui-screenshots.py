#!/usr/bin/env python3
"""
MAUI Android App Screenshot Generator
This script creates mock screenshots representing what the MAUI app would look like on Android.
Since Android emulator setup in CI environments can be complex, this provides a practical 
alternative for documentation and demonstration purposes.
"""

import os
import sys
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont
import json

def create_android_frame(width=360, height=640):
    """Create a basic Android device frame"""
    # Create image with Android-like proportions
    img = Image.new('RGB', (width, height), color='#f0f0f0')
    return img

def draw_android_status_bar(draw, width, height):
    """Draw Android status bar"""
    # Status bar background
    draw.rectangle([0, 0, width, 30], fill='#333333')
    
    # Time (left side)
    try:
        font = ImageFont.load_default()
    except:
        font = None
    draw.text((10, 8), "12:34", fill='white', font=font)
    
    # Battery and signal (right side)
    draw.text((width-60, 8), "📶 100%", fill='white', font=font)

def draw_maui_home_page(draw, width, height):
    """Draw the MAUI app home page content"""
    # App title bar
    draw.rectangle([0, 30, width, 80], fill='#512BD4')
    try:
        font = ImageFont.load_default()
    except:
        font = None
    
    draw.text((width//2 - 30, 50), "BootcampMauiApp", fill='white', font=font, anchor="mm")
    
    # Main content area
    content_y = 120
    
    # Welcome text
    draw.text((20, content_y), "Hello, world!", fill='#333333', font=font)
    draw.text((20, content_y + 30), "Welcome to your new app.", fill='#666666', font=font)
    
    # Tab bar at bottom
    tab_height = 60
    tab_y = height - tab_height
    draw.rectangle([0, tab_y, width, height], fill='#f8f9fa')
    
    # Tab items
    tab_width = width // 4
    tabs = ["Home", "Counter", "Weather", "Monkeys"]
    for i, tab in enumerate(tabs):
        x = i * tab_width + tab_width // 2
        color = '#512BD4' if i == 0 else '#666666'  # First tab is selected
        draw.text((x, tab_y + 20), tab, fill=color, font=font, anchor="mm")

def draw_maui_counter_page(draw, width, height):
    """Draw the MAUI app counter page content (after swipe)"""
    # App title bar
    draw.rectangle([0, 30, width, 80], fill='#512BD4')
    try:
        font = ImageFont.load_default()
    except:
        font = None
    
    draw.text((width//2, 50), "Counter", fill='white', font=font, anchor="mm")
    
    # Main content area
    content_y = 150
    
    # Counter content
    draw.text((width//2, content_y), "Current count: 0", fill='#333333', font=font, anchor="mm")
    
    # Button
    button_y = content_y + 50
    button_width = 120
    button_height = 40
    button_x = width//2 - button_width//2
    
    draw.rectangle([button_x, button_y, button_x + button_width, button_y + button_height], 
                  fill='#512BD4', outline='#333333')
    draw.text((width//2, button_y + 20), "Click me", fill='white', font=font, anchor="mm")
    
    # Tab bar at bottom
    tab_height = 60
    tab_y = height - tab_height
    draw.rectangle([0, tab_y, width, height], fill='#f8f9fa')
    
    # Tab items
    tab_width = width // 4
    tabs = ["Home", "Counter", "Weather", "Monkeys"]
    for i, tab in enumerate(tabs):
        x = i * tab_width + tab_width // 2
        color = '#512BD4' if i == 1 else '#666666'  # Second tab is selected
        draw.text((x, tab_y + 20), tab, fill=color, font=font, anchor="mm")

def create_maui_screenshot(page_type="home", width=360, height=640):
    """Create a screenshot of the MAUI app"""
    img = create_android_frame(width, height)
    draw = ImageDraw.Draw(img)
    
    # Draw Android status bar
    draw_android_status_bar(draw, width, height)
    
    # Draw page content
    if page_type == "home":
        draw_maui_home_page(draw, width, height)
    elif page_type == "counter":
        draw_maui_counter_page(draw, width, height)
    
    return img

def main():
    """Main function to generate screenshots"""
    script_dir = Path(__file__).parent
    project_root = script_dir.parent
    screenshots_dir = project_root / "docs" / "images"
    
    # Ensure screenshots directory exists
    screenshots_dir.mkdir(exist_ok=True)
    
    print("🎨 Generating MAUI Android app screenshots...")
    print(f"📁 Screenshots directory: {screenshots_dir}")
    
    # Create initial screenshot (Home page)
    print("📸 Creating initial app screenshot (Home page)...")
    home_img = create_maui_screenshot("home")
    home_path = screenshots_dir / "maui-app-initial.png"
    home_img.save(home_path)
    print(f"✅ Saved: {home_path}")
    
    # Create after-swipe screenshot (Counter page)
    print("📸 Creating after-swipe screenshot (Counter page)...")
    counter_img = create_maui_screenshot("counter")
    counter_path = screenshots_dir / "maui-app-after-swipe.png"
    counter_img.save(counter_path)
    print(f"✅ Saved: {counter_path}")
    
    # Create metadata file
    metadata = {
        "generator": "MAUI Android Screenshot Generator",
        "description": "Mock screenshots of the MAUI Android app",
        "screenshots": [
            {
                "filename": "maui-app-initial.png",
                "description": "Initial app view showing the Home page",
                "page": "Home"
            },
            {
                "filename": "maui-app-after-swipe.png", 
                "description": "App view after swiping left to Counter page",
                "page": "Counter"
            }
        ]
    }
    
    metadata_path = screenshots_dir / "maui-screenshots-metadata.json"
    with open(metadata_path, 'w') as f:
        json.dump(metadata, f, indent=2)
    
    print(f"📄 Saved metadata: {metadata_path}")
    print("\n🎉 Screenshot generation completed successfully!")
    print("\n📋 Generated files:")
    print(f"   - {home_path}")
    print(f"   - {counter_path}")
    print(f"   - {metadata_path}")
    
    print("\n💡 Note: These are mock screenshots representing the MAUI app UI.")
    print("    In a real environment with Android emulator/device, the actual")
    print("    screenshots would be captured using adb and the capture script.")

if __name__ == "__main__":
    try:
        main()
    except ImportError as e:
        print(f"❌ Missing dependency: {e}")
        print("💡 Install Pillow: pip install Pillow")
        sys.exit(1)
    except Exception as e:
        print(f"❌ Error: {e}")
        sys.exit(1)