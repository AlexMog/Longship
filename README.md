# Longship
Longship aims to provide a simply-to-configure, optimizations, bugfixes and plugins system for Valheim servers !

# Features
**Currently these features are in dev-phase, please do not use them if they are not marked as *TESTED***  
* Fixed server-lag by upgrading the server bandwith limit through configuration - *TESTED*
* Simple yet powerful plugins system
* Events that plugins developers can listen on to provide more gameplay mechanics
* The possibility to alter complete gameplay parts either through the Events system or directly by overriding game methods using HarmonyLib

# How to install
* Go to the [releases page](https://github.com/AlexMog/Longship/releases) and download the last stable, production version.
* Unzip the downloaded folder directly in your Valheim server directory, `valheim_server.exe` and `Longship.dll` should be in the same directory.
* **IF YOU ARE ON LINUX OR MAC** run the server using `run_longship.sh`
* **IF YOU ARE ON WINDOWS** run the server using `start_headless_server.bat`
* Have fun !

# Configuration
Configuration files are generated when you launch Longship for the first time.  
They should be located in the `VALHEIM_SERVER/Longship/Configs` directory.

`Server.yml`
```yaml
# Name of the server
serverName: Default Server
# Max players that can connect to the server
maxPlayers: 10
# Server password. Note: leave empty if you don't want any password
serverPassword: ""
network:
  # Upload bandwith allowed for the server, it is an easy fix for common lag problems, if you are lagging, you can augment this value.
  # WARNING: This value WILL allow the server to use more bandwith. So be careful.
  # Info: The value is in bytes (in this configuration, that means that the server is limited to ~250 Ko/s)
  dataPerSeconds: 245760
```

# Plugins
Plugins can be added and loaded to Longship.  
Please refer to the [Wiki](https://github.com/AlexMog/Longship/wiki) for guides regarding Plugin installation or development.

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
