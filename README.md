# Simple Character Controller (SCC)

A modular, physics-based character controller for Unity featuring smooth movement, ground snapping, and extensible component architecture.

## Features

- **Physics-Based Movement**: Smooth acceleration and deceleration using Unity's physics system
- **Ground Snapping**: Spring-damper system for smooth ground following on uneven terrain
- **Modular Design**: Component-based architecture for easy customization and extension
- **Input Flexibility**: Interface-based input system supporting multiple input methods
- **Ground Interaction**: Callback system for ground-based interactions
- **Ladder Climbing**: Example implementation of special movement mechanics

## Core Components

### Character
The main character class that manages all subsystems:
- Movement management
- Jump mechanics
- Look/camera control
- Ground detection and snapping
- Input handling

### CharacterMovement
Handles horizontal movement with configurable speed and acceleration:
- Ground and air movement with different acceleration values
- Physics-based velocity changes for smooth movement
- Transform-relative movement direction

### FloatingCharacter
Physics-based ground detection and snapping system:
- Sphere-cast ground detection for robust collision
- Spring-damper physics for smooth ground following
- Ground enter/exit callback system
- Configurable hover height and spring properties

### CharacterJump
Simple jump mechanics with ground detection:
- Configurable jump force
- Ground-based jump validation
- Integration with ground snapping system

### CharacterLook
Mouse look camera control:
- Configurable sensitivity for X and Y axes
- Clamped vertical rotation to prevent over-rotation
- Separate camera root for proper first-person view

## Setup

### Basic Character Setup

1. **Create Character GameObject**:
   ```
   Character GameObject
   ├── Character.cs
   ├── CharacterMovement.cs
   ├── CharacterJump.cs
   ├── CharacterLook.cs
   ├── FloatingCharacter.cs
   ├── SimpleInputProvider.cs (or custom input provider)
   ├── Rigidbody
   └── CapsuleCollider (child object)
       └── Camera Root (child)
           └── Main Camera
   ```

2. **Configure Components**:
   - **Rigidbody**: Set appropriate mass, disable rotation constraints on X/Z if needed
   - **FloatingCharacter**: Set desired height, sensor distance, spring/damping values
   - **CharacterMovement**: Configure speed and acceleration values
   - **CharacterJump**: Set jump force
   - **CharacterLook**: Assign camera root transform, set sensitivity values

3. **Input System Setup**:
   - Create Input Actions for movement, look, and jump
   - Assign to SimpleInputProvider component
   - Or implement custom ICharacterInputProvider

### Configuration Parameters

#### FloatingCharacter
- `desiredHeight`: Distance from ground to maintain
- `sensorDistance`: Maximum ground detection range
- `spring`: Spring force strength (higher = stiffer)
- `damping`: Spring damping (higher = less bouncy)
- `applyNegativeForce`: Whether to pull character down to ground

#### CharacterMovement
- `speed`: Maximum movement speed
- `acceleration`: Ground acceleration rate
- `airAcceleration`: Air movement acceleration (usually lower)

#### CharacterJump
- `jumpForce`: Upward force applied when jumping

#### CharacterLook
- `sensitivity`: Look sensitivity for X, Y, Z axes
- `cameraRoot`: Transform containing the camera for pitch rotation

## Extension Examples

### Custom Input Provider
```csharp
public class CustomInputProvider : MonoBehaviour, ICharacterInputProvider
{
    public Vector3 Look { get; private set; }
    public Vector3 Movement { get; private set; }
    public bool IsJumping { get; private set; }
    
    private void Update()
    {
        // Implement custom input logic
    }
}
```

### Ground Interaction
```csharp
public class MovingPlatform : MonoBehaviour, ICharacterGroundCallbacks
{
    public void CharacterEnter(Character character)
    {
        // Character stepped on platform
        character.transform.SetParent(transform);
    }
    
    public void CharacterExit(Character character)
    {
        // Character left platform
        character.transform.SetParent(null);
    }
}
```

### Custom Character Component
```csharp
public class CharacterWallRun : CharacterComponent
{
    private void Update()
    {
        // Access character systems through base Character property
        if (Character.FloatingManager.Grounded) return;
        
        // Custom wall running logic
    }
}
```

## Architecture

### Component Hierarchy
```
CharacterComponent (Abstract Base)
├── CharacterMovement
├── CharacterJump
├── CharacterLook
├── FloatingCharacter
└── Custom Components (CharacterLadderClimber, etc.)
```

### Key Interfaces
- `ICharacterInputProvider`: Input abstraction for different input systems
- `ICharacterGroundCallbacks`: Ground interaction callbacks for environmental objects

## Tips and Best Practices

1. **Physics Timing**: Most character logic runs in `FixedUpdate()` for consistent physics
2. **Ground Detection**: Use sphere casting instead of raycasting for more robust collision detection
3. **Force Application**: Use `ForceMode.VelocityChange` for immediate velocity changes
4. **Modular Design**: Extend functionality by adding new components inheriting from `CharacterComponent`
5. **Input Caching**: Cache input values in Update() and read in FixedUpdate() for better performance

## Common Issues

- **Floating/Bouncing**: Adjust spring and damping values in FloatingCharacter
- **Sluggish Movement**: Increase acceleration values in CharacterMovement
- **Inconsistent Jumping**: Ensure jump logic runs in FixedUpdate()
- **Camera Jitter**: Use separate transforms for character body and camera rotation

## Requirements

- Unity 2021.3 or later
- Unity Input System package (for SimpleInputProvider)
- Physics-based character setup with Rigidbody and Collider

## Author

**HamitEfe** - Check out my other Unity projects and tools on [GitHub](https://github.com/hamitefe)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.
