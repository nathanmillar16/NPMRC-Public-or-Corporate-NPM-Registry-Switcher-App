# NPMRC Public-or-Corporate NPM Registry Switcher App

## Description

This console application allows the user to rename their `.npmrc` file programatically. This should save the user from having to navigate to the directory and perform numerous mouse click actions.
The directory for the npmrc file is configurable and stored in the `NPMRC-App.dll.config` and editable through the console app./.

## Downloads

| OS-ARCH                                                                                                                      |
| ---------------------------------------------------------------------------------------------------------------------------- |
| [LIN-X64](//github.com/nathanmillar16/NPMRC-Public-or-Corporate-NPM-Registry-Switcher-App/blob/master/Downloads/LIN-X64.zip) |
| [OSX-X64](//github.com/nathanmillar16/NPMRC-Public-or-Corporate-NPM-Registry-Switcher-App/blob/master/Downloads/OSX-X64.zip) |
| [WIN-X86](//github.com/nathanmillar16/NPMRC-Public-or-Corporate-NPM-Registry-Switcher-App/blob/master/Downloads/WIN-X86.zip) |
| [WIN-X64](//github.com/nathanmillar16/NPMRC-Public-or-Corporate-NPM-Registry-Switcher-App/blob/master/Downloads/WIN-X64.zip) |

## Publishing

Using Visual Studio 22, publish the project to a directory of your choice. This will produce an `.exe` file for Windows users.

## How to use

Once published, the console app acts as a wizard.

- It will check if you have a directory set. Following that, it will ask if the user is happy with the config.
- If the config needs to be edited, this can be done in the console application but the application will then need to be restarted. Please restart the application when it indicates to do so.

- If no edits are needed to the configs, the user will be shown their set configs.
- Following that it will ask if you are developing corporately (against a private npm artifacts feed) or wanting "personal development" (using the global npmjs feed)
- If the first is chosen, the _existing\*\*_ `.notInUseNpmrcName` will be renamed to `.npmrc`
- If the latter is chosen, the existing `.npmrc` will be renamed to `.notInUseNpmrcName`

\*\* The console application assumes the user is wanting to switch from using `.npmrc`/developing corporately in the first instance the application runs after setting file directory configs.
