### WARNING: LONGSHIP IS NOW A BEPINEX PLUGIN !

# Longship
Longship aims to provide a simply-to-configure, optimizations, bugfixes and a high-level API for Valheim servers !
The primary goal is to provide easy ways for developers to develop effective plugins and provide more configuration options and fixes to server admins.

# Features
* Fixed server-lag by upgrading the server bandwith limit through configuration
* Update your server configuration using configuration files
* Simple yet powerful plugins system
* Develop powerful plugins using a simple yet effective EventSystem
* Events that plugins developers can listen on to provide more gameplay mechanics
* The possibility to alter complete gameplay parts either through the Events system or directly by overriding game methods using HarmonyLib

# How to install
## Simple installation
TODO

## Manual installation
* MAKE SHURE YOU HAVE ALREADY INSTALLED [BEPINEX](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/).
* Go to the [releases page](https://github.com/AlexMog/Longship/releases) and download the last stable, production version.
* Unzip the downloaded folder in your `VALHEIM_SERVER/BepInEx/plugins` directory.
* Launch the server.
* Enjoy !

# Configuration
Configuration files are generated when you launch Longship for the first time.  
They should be located in the `VALHEIM_SERVER/BepInEx/config` directory.

`gg.mog.valheim.longship.cfg` contains all the configurations and documentation about them.

# Plugins
Plugins can be added and loaded to Longship.  
Please refer to the [Wiki](https://github.com/AlexMog/Longship/wiki) for guides regarding Plugin installation or development.

# High-level API
Even if you are not using the Plugins system, you can still use the High-Level API (Events system, helpers, etc) provided by Longship.
Please refer to the [Wiki](https://github.com/AlexMog/Longship/wiki) for documentation.

# How to build
You must use VisualStudio or any other IDE compatible with .net framework 4.0 to build this project. **Do not use other versions than 4.0**  

# Roadmap
See the [open issues](https://github.com/AlexMog/Longship/issues) for a list of proposed features (and known issues).

# Wiki
Tutorials and how to can be fond on the [Wiki pages](https://github.com/AlexMog/Longship/wiki)

# Contributing
Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are greatly appreciated.

To contribute, you must:
* Fork the Project
* Create your Feature Branch (git checkout -b feature/AmazingFeature)
* Commit your Changes (git commit -m 'Add some AmazingFeature')
* Push to the Branch (git push origin feature/AmazingFeature)
* Open a [Pull Request](https://github.com/AlexMog/Longship/pulls)

Feel free to contribute on any idea you have for this project, as long as it stays inline with the roadmap and the planned features.

# License
Distributed under the MIT License. See `LICENSE` file for more information.

# Contact
AlexMog
* Twitter: @AlexMog_FR

# Special thanks
* [Hi-ImKyle](https://github.com/Hi-ImKyle) for his port to BepInEx
