![alt text](Unity3D_Tutorial.png)


# System requirements
Unity3D 2017.4.12f1 or later


# 1. Why Unity3D ?
- User-friendly
- Very good support (Documentation, Community, Forums)
- High quality integrated tools (Physics, Lightning, Rendering, Animation, Network, ...)
- Scripting with CSharp is really easy and fast
- Free version contains most of the features


# 2. Unity3D Interface
- Scene / Game views
- Hierarchy / Project tabs
- Console / Profiler tabs
- The Inspector
- Other windows (Audio, Animation, Animator, Lightning, Navigation, ...)


# 3. Unity3D Configuration
### 1) Set your Keys in Edit > Preferences !
### 2) Take a look at the Project Settings :
- Input
- Tags & Layers
- Audio / Time / Physics
- Quality / Graphics
- Network
- Editor
- Script Execution Order


# 4. GameObject
### 1) Drag an object in the scene
### 2) Give it a behavior (built-in Component or MonoBehaviour)
### 3) Understand MonoBehaviour methods
- Awake() : Init 1
- Start() : Init 2
- Update() : Game loop, animate things here
- LateUpdate() : Finalize animations
- OnEnable() / OnDisable()
- OnDestroy() : Clean up resources if needed
### 4) Understand execution order
https://docs.unity3d.com/Manual/ExecutionOrder.html


# 5. Physics
### 1) Collider: the physical shape of your object
### 2) RigidBody: make your object react to physics
- All the code dealing with RigidBody should be put in FixedUpdate()
- FixedUpdate() is Unity3D's Physics loop, timestep is constant
- Update() & LateUpdate() have a rendering-dependent timestep (non-constant) !


# 6. Animation
### 1) Bonhomme creation
- Add Transforms to create the hierarchy
- Add Cubes for the body parts
### 2) Walk animation creation
### 3) Control animation using speed
### 4) Mecanim : Unity3D animation system
- Blending & mixing animations can be done graphically (animations states, blend trees)
- You should use it when dealing with Humanoids: reuse your animations on different avatars (motion retargeting)


# 7. Game
### 1) Create a controllable Player (Input & Camera)
### 2) Add a terrain (heightmap, textures, details)
### 3) Add trees & wind
### 4) Add physics to the Player (handling of collisions, climbing slopes)
### 5) Add world bounds
### 6) Add bonus objects manually, then procedurally
### 7) Add a game logic
### 8) Add a graphical user interface


# 8. Lighting
### 1) Setup your static environment: all the non-moving objects should be set to static !
### 2) Setup your lights in the scene: add a sun and/or other lights
### 3) STATIC LIGHTS SHOULD BE BAKED !
### 4) GI: Light rays bounce on surfaces to simulate indirect lighting


# 9. Unity3D & Git
### 1) You should use Git with Unity3D !
### 2) Edit > Project Settings > Editor : 
- Version Control : Visible Meta Files contains information that need to be shared between developers.
- Asset Serialization : Mixed mode allows both text & binary files
- Line Endings for New Scripts : choose OS Native to not being bored by file endings while creating new scripts
### 3) Only Commit "Assets" & "ProjectSettings" folders (see .gitignore file)
### 4) REMOVE OPERATIONS SHOULD BE DONE ONLY IN UNITYEDITOR !
### 5) Don’t forget to Add before Commit : Unity3D can generate files (meta files especially)
