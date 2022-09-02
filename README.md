# iml-unity-data-augmentation
Augments InteractML training datasets through PCGML

## Installation as a submodule of [InteractML](https://github.com/Interactml/iml-unity)
```
# go to your Unity project folder
cd [your_project_folder]

# add InteractML as a submodule (if not done already)
git submodule add -b master --force https://github.com/Interactml/iml-unity.git Assets/iml-unity

# add InteractML_Telemetry as a submodule 
git submodule add https://github.com/carlotes247/InteractML_Telemetry.git Assets/iml-unity-telemetry

# add Data Augmentation as a submodule
git submodule add https://github.com/carlotes247/iml-unity-data-augmentation.git Assets/iml-unity-data-augmentation

```

## Dependencies
**This module also has the following dependencies. It won't work without them**
- [InteractML](https://github.com/Interactml/iml-unity). 
- [InteractML Telemetry](https://github.com/carlotes247/InteractML_Telemetry) **This dependency should be removed in the future**