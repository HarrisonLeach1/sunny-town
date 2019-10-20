# Global Warning Studios: Sunny Town

## Team Members

| Name           | GitHub Username  | UoA UPI |
| -------------- | ---------------- | ------- |
| Harrison Leach | HarrisonLeach1   | hlea849 |
| William Li     | TwelveHertz      | zli667  |
| Nidhinesh Nand | nidhineshnand    | nnan773 |
| Allen Nian     | anian100         | ania716 |
| William Shin   | william-shin-387 | wshi593 |
| Casey Wong     | cwon880          | cwon880 |
| James Zhang    | JamesUOA         | jzha414 |

## Gameplay

-   As the mayor your role is to make decisions for the town.
-   Different people will approach after short periods of time.
-   The decisions you make will alter the story's path and influence future decisions.
-   The decisions you make will affect your towns metrics (population happiness, money, environmental health).
-   Periodically an exclamation mark will appear above the towns buildings, interacting with it
    allows you to make 'minor' decisions for the town.

-   The objective of the game is to make appropriate tradeoffs to ensure no metric in the town drops too low.
    This should teach the player the tradeoffs that are necessary in maintaining the environments health.

## Execution Instructions

### Use the WebGL Build

-   Can be found [here](https://global-warning.s3-ap-southeast-2.amazonaws.com/index.html)

### Through Unity (Version 2018.4.9f1)

1. Checkout the "Final" branch from the repository.
2. Navigate to `Assets/Scenes/MenuScene.unity` and open the file with Unity.
3. Press the play button in Unity.
4. You will begin on the start screen where you will be able to select "Play" to begin.

## Setting up

### Setup Unity file merging

1. Add the following text to your `~/.gitconfig` file.

```
[merge] tool = unityyamlmerge
[mergetool "unityyamlmerge"]
    trustExitCode = false
    cmd = 'C:\\Program Files\\Unity\\Editor\\Data\\Tools\\UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```

> **The path above may have to be modified to fit your environment**

For Mac, use the following path instead:

```
/Applications/Unity/Unity.app/Contents/Tools/UnityYAMLMerge
```

### Setup Git LFS

1. Download and install the Git LFS command line tool [here](https://git-lfs.github.com/)
2. Run the following command in the repository:

```
git lfs install
```

### Adding new binaries

If adding new type of binary file, make sure to track it in Git LFS by running the following command:

```
git lfs track "*.<extension name>"
```
