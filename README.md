# RePraxis for Unity

## Table of Contents

- [RePraxis for Unity](#repraxis-for-unity)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Getting Started](#getting-started)
    - [Installation](#installation)
    - [Creating a Database Manager](#creating-a-database-manager)
  - [Samples](#samples)
    - [A Simple Relationship System](#a-simple-relationship-system)
      - [Syncing information with listeners](#syncing-information-with-listeners)
      - [Finding Relationships](#finding-relationships)
      - [Detecting Love Triangles](#detecting-love-triangles)

## Overview

Re:Praxis is an in-memory logic database solution for creating simple databases for games and applications. It is a reconstruction of Praxis, the exclusion logic-based language used by the [Versu social simulation engine](https://versu.com/).

This repository contains the [RePraxis log database](https://github.com/ShiJbey/RePraxis) packaged for distribution as a Unity package. Changes to the database or query language follow the main repository. The version numbers for this package and the original repository should usually match.

For documentation on querying and working with the database, please refer to the [README in the original repo](https://github.com/ShiJbey/RePraxis/blob/main/README.md).

## Getting Started

### Installation

Add the following line to your `manifest.json` file in `YOUR_UNITY_PROJECT/Packages/manifest.json`. `YOUR_UNITY_PROJECT` is the file path to wherever your unity project is stored on your computer. You may need to do this using your computer’s file explorer.

```text
"com.shijbey.repraxis": "https://github.com/ShiJbey/RePraxis-For-Unity.git?path=/Packages/com.shijbey.repraxis#v1.4.0"
```

The final `#v1.4.0` may be changed to any valid version tag found under the [version tag page on GitHub](https://github.com/ShiJbey/RePraxis-For-Unity/tags).

After saving the changes to `manifest.json`, Unity should install the package, and `RePraxis For Unity` should be visible in your Unity Package Manager window.

### Creating a Database Manager

This package contains the code for creating databases, and it comes with default MonoBehaviour implementation to help you get started. The instructions below will get you started with using RePraxis in your game. This tutorial uses the `DatabaseManager` script, a MonoBehaviour that manages a single database instance. Feel free to use that script in your final project or create a custom manager script if necessary.

1. Open your Unity project and create a new scene.
2. Follow the installation instructions above.
3. Create a new empty GameObject.
4. Name it `DatabaseManager`.
5. Add a `Database Manager` component.
6. Create a new script called `DatabaseTest.cs`
7. Copy the code below into `DatabaseTest.cs`. This coed inserts a *sentence* into the database and asserts that the sentence was successfully inserted.

```csharp
      using UnityEngine;
      using RePraxis;

      public class DatabaseTest : MonoBehaviour
      {
         void Start()
         {
            DatabaseManager manager = FindFirstObjectByType<DatabaseManager>();

            manager.Database.Insert("ashley.dislikes.mike");

            Debug.Log(
                  manager.Database.Assert("ashley.dislikes.mike")
            );
         }
      }
```

8. Run the Script and read the output in the Unity console. You should see "True" printed.

## Samples

### A Simple Relationship System

This repository has a sample demonstrating how to create a simple relationship system using RePraxis. In the sample, characters have names and store relationships toward other characters. Each relationship has a relationship type and an opinion score.

#### Syncing information with listeners

Sometimes, you may want to track a value within your game and have it automatically sync with the database- for example, character stats. RePraxis uses *access listeners* to calculate database information whenever a query is made. In the sample scene, we use `RePraxisDatabase.AddBeforeAccessListener()` to listen for when a query accesses data related to a character and update that information before evaluating. The code can be found in the [Sample Character File](./Assets/Scripts/SampleCharacter.cs).

#### Finding Relationships

Clicking the "Search Relationships" button while playing the sample will signal to the "RelationshipFinder" GameObject to find all relationships characters have toward a given target with an opinion score greater than a given threshold and are of a given relationship type. By default, it is configured to find NPC relationships toward the player. Feel free to experiment with the parameters, add more relationships, and see what happens.

#### Detecting Love Triangles

Clicking the "Find Love Triangles" button will signal to the "RelationshipFinder" to find all instances of love triangles in the database. The results are printed to the console. `?a`, `?b`, and `?c` correspond to the three characters in the love triangle. Notice how all permutations are repeated in the results. RePraxis considers each permutation of `?a`, `?b`, and `?c` a separate result.

Love triangles are detected using a query that looks for people who have a crush on someone and that someone does not have a crush on them. Additionally, this pattern must involve three characters (in this case ?a, ?b, and ?c) with each having a crush on the other in a circular fashion.

This example love triangle detector shows how RePraxis makes it easier to query complex relationship patterns like this, which might require many nested loops iterating over characters and their relationships. RePraxis queries are much more concise.
