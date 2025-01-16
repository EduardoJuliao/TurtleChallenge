# Turtle Adventure Challenge

This project solves a challenge where a turtle has to navigate a mine field and reach the exit.

To use this aplication, start the executable, you can either pass down two arguments to the executable:

* Board Setup file path
* Movements file path

If these files are not provided, the executable will ask for a valid path.
In the case of the argumets were informed, but are invalid, the executable will ask the user for valid paths.

## Running the application

To run the application, either run through Visual Studio or the IDE of your choice,
or build the application, when building the applicatino the executable will be under `./TurtleChallenge/bin/Debug/net8.0/TurtleChallange.Console.exe`.

## Project Structure

The solution is divided into two projects

* Console Application
* Testing

### Console Application

#### Data

Testing json files.

##### Models

Classes to translate json files into objects.

#### Entities

Classes required for the program to operate.

#### Enums

All enums used by the system.

#### Helpers

Console Helpers.

#### Interfaces

All interfaces used in the application.

#### Messages

All messages used in the application.
Examples of Messages are validation messages and the result of the game.

#### Validators

All validators used in the system, in this scenarios, it validates the input from the user when creating the board.

#### Root folder

Classes used for the game.

### Testing

In the testing project, you'll find the unit tests for the classes in the console application, with use cases and explanations for the
`InLineData`.

## Json Formats

To use this application, the json files needs to be in a certain format, which follows:

### Board Setup

```json schema
{
  "$schema": "http://json-schema.org/draft-04/schema#",
  "type": "object",
  "properties": {
    "board_size": {
      "type": "object",
      "properties": {
        "x": {
          "type": "integer"
        },
        "y": {
          "type": "integer"
        }
      },
      "required": [
        "x",
        "y"
      ]
    },
    "player_position": {
      "type": "object",
      "properties": {
        "x": {
          "type": "integer"
        },
        "y": {
          "type": "integer"
        },
        "direction": {
          "type": "integer"
        }
      },
      "required": [
        "x",
        "y",
        "direction"
      ]
    },
    "mines": {
      "type": "array",
      "items": [
        {
          "type": "object",
          "properties": {
            "position": {
              "type": "object",
              "properties": {
                "x": {
                  "type": "integer"
                },
                "y": {
                  "type": "integer"
                }
              },
              "required": [
                "x",
                "y"
              ]
            }
          },
          "required": [
            "position"
          ]
        }
      ]
    },
    "exit_location": {
      "type": "object",
      "properties": {
        "x": {
          "type": "integer"
        },
        "y": {
          "type": "integer"
        }
      },
      "required": [
        "x",
        "y"
      ]
    }
  },
  "required": [
    "board_size",
    "player_position",
    "exit_location"
  ]
}
```

### Movement Instructions

```json
{
  "$schema": "http://json-schema.org/draft-04/schema#",
  "type": "array",
  "items": [
    {
      "type": "object",
      "properties": {
        "instructions": {
          "type": "array",
          "items": [
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            }
          ]
        }
      },
      "required": [
        "instructions"
      ]
    },
    {
      "type": "object",
      "properties": {
        "instructions": {
          "type": "array",
          "items": [
            {
              "type": "integer"
            },
            {
              "type": "integer"
            }
          ]
        }
      },
      "required": [
        "instructions"
      ]
    },
    {
      "type": "object",
      "properties": {
        "instructions": {
          "type": "array",
          "items": [
            {
              "type": "integer"
            }
          ]
        }
      },
      "required": [
        "instructions"
      ]
    },
    {
      "type": "object",
      "properties": {
        "instructions": {
          "type": "array",
          "items": [
            {
              "type": "integer"
            },
            {
              "type": "integer"
            },
            {
              "type": "integer"
            }
          ]
        }
      },
      "required": [
        "instructions"
      ]
    }
  ]
}
```
