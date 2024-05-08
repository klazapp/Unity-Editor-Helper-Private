# Editor Helper for Unity

## Introduction

The Editor Helper package, developed under the `com.Klazapp.Editor` namespace, is a comprehensive toolkit designed to streamline and enhance the Unity Editor experience. This utility provides a range of methods for manipulating and displaying properties, drawing custom GUI elements, and handling input within the Unity Editor, making it an essential tool for developers looking to customize and extend the functionality of the Unity Editor.

## Features

- **Property Manipulation:** Simplifies the interaction with serialized properties, allowing for conversions between Unity's `SerializedProperty` and common data types like `Vector3` and `Quaternion`.
- **Custom GUI Elements:** Offers methods to draw custom buttons, lines, boxes, and spaces, enhancing the visual layout and interaction within custom editor scripts.
- **Input Handling:** Includes functions to detect pointer events such as hover, click, and leaving the area of GUI elements, useful for creating interactive editor tools.
- **Texture Loading:** Provides a method to load textures from specified package paths, aiding in the customization of editor visuals.

## Dependencies

- **Unity Editor Scripting:** This package requires Unity Editor classes and attributes, making it specific to editor extensions and not suitable for runtime use.
- **Unity Mathematics Package:** Utilized for mathematical operations within some of the methods.

## Compatibility

This package is designed specifically for use within the Unity Editor and does not depend on Unity's runtime environments, rendering pipelines, or specific Unity versions.

| Compatibility | URP | BRP | HDRP |
|---------------|-----|-----|------|
| Not Applicable| N/A | N/A | N/A  |

## Installation

1. Download the Editor Helper scripts from the [GitHub repository](https://github.com/klazapp/Unity-Editor-Helper-Public.git) or via the Unity Package Manager.
2. Add the scripts to your Unity project, specifically within an Editor folder to ensure they are only compiled for editor use.

## Usage

Include the `EditorHelper` static class in your custom editor scripts to access its functionalities. Here's an example of how to draw a custom button within an editor window:

```csharp
if (EditorHelper.DrawButton(buttonWidth: 100, buttonHeight: 20, buttonColor: Color.green, titleText: "Click Me"))
{
    Debug.Log("Button was clicked!");
}
```

## To-Do List (Future Features)

- [ ] Add support for more complex GUI layouts.
- [ ] Implement additional input handling functionalities for more sophisticated editor interactions.
- [ ] Enhance the visual customization capabilities for editor windows and inspectors.

## License

This utility is released under the MIT License, allowing for free use, modification, and distribution within your projects.
