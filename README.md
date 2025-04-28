# Dependencies
Building over dotnet ``net-8.0``

Nuget packages in use:
- Dapper
- OpenTk
- Sixlabors.ImageSharp

Make sure packages is in system (Linux):
- Mesa
- GLFW (for x11, for Wayland is not yet working propertly)

# Solution structure
- ``TestEngine`` - engine itself
- ``DataTescing`` - example project, typo in name :/

# Possible errors
- Please note: while in most *nix-like systems file path separates by slash(``/``) in POSIX systems 
(like Windows and DOS) system utilizes backslash (``\\``). What does it mean? Check examples for right path
variables, for example I'm using path ``"res/draw1.png"`` to load image, but in Windows you must change it
to ``"res\\draw1.png"``
- Very old cards probably will be off-game, since OpenTK built around OpenGL 3.0 and newer. I'm not using 
OpenGL 4.0 for vide-range of cards support, but anyway minimum version is still 3.0
